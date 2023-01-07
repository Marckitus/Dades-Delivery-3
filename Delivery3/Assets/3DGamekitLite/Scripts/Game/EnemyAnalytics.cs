using Gamekit3D;
using Gamekit3D.Message;
using UnityEngine;
using static Gamekit3D.Damageable;

public class EnemyAnalytics : MonoBehaviour, IMessageReceiver
{
    private Damageable m_Damageable;

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
            case MessageType.DEAD:
                StartCoroutine(DataAnalytics.AddData(new UserKills(), damageMessage.damageSource.ToString(), timer.GetCurrentTime(), DataAnalytics.GetEnemyType(name).ToString()));
                break;
            default:
                break;
        }
    }
}
