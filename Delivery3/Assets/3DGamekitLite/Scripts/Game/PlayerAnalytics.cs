using Gamekit3D;
using Gamekit3D.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Gamekit3D.Damageable;

public class PlayerAnalytics : MonoBehaviour, IMessageReceiver
{
    protected Damageable m_Damageable;

    private Timer timer;

    private void OnEnable()
    {
        m_Damageable = GetComponent<Damageable>();
        m_Damageable.onDamageMessageReceivers.Add(this);

        timer = FindObjectOfType<Timer>();
    }

    public void OnReceiveMessage(MessageType type, object sender, object data)
    {
        DamageMessage damageMessage = (DamageMessage)data;
        switch (type)
        {
            case MessageType.DAMAGED:
                StartCoroutine(DataAnalytics.AddData(new UserHit(), damageMessage.damageSource.ToString(), timer.GetCurrentTime(), DataAnalytics.GetDamageType(damageMessage).ToString()));
                break;
            case MessageType.DEAD:
                StartCoroutine(DataAnalytics.AddData(new UserDeaths(), damageMessage.damageSource.ToString(), timer.GetCurrentTime(), DataAnalytics.GetDamageType(damageMessage).ToString()));
                break;
            default:
                break;
        }
    }
}
