using Gamekit3D;
using Gamekit3D.Message;
using UnityEngine;
using static Gamekit3D.Damageable;

public class PlayerAnalytics : MonoBehaviour, IMessageReceiver
{
    [SerializeField] private int positionInterval;
    private int currentInterval;

    private CharacterController player;
    private Damageable m_Damageable;
    private System.Guid sessionID;

    private Timer timer;

    private void OnEnable()
    {
        sessionID = System.Guid.NewGuid();
        m_Damageable = GetComponent<Damageable>();
        player = GetComponent<CharacterController>();
        m_Damageable.onDamageMessageReceivers.Add(this);

        timer = FindObjectOfType<Timer>();
        currentInterval = timer.currentFrames + positionInterval;
    }

    private void Update()
    {
        if (timer.currentFrames >= currentInterval)
        {
            currentInterval = timer.currentFrames + positionInterval;
            StartCoroutine(DataAnalytics.AddData(DataType.POSITION, transform.position.ToString(), timer.GetCurrentTime(), player.velocity.ToString(), sessionID.ToString()));
        }
    }

    public void OnReceiveMessage(MessageType type, object sender, object data)
    {
        DamageMessage damageMessage = (DamageMessage)data;
        switch (type)
        {
            case MessageType.DAMAGED:
                StartCoroutine(DataAnalytics.AddData(DataType.HIT, damageMessage.damageSource.ToString(), timer.GetCurrentTime(), DataAnalytics.GetDamageType(damageMessage.damager.name).ToString()));
                break;
            case MessageType.DEAD:
                StartCoroutine(DataAnalytics.AddData(DataType.DEATH, damageMessage.damageSource.ToString(), timer.GetCurrentTime(), DataAnalytics.GetDamageType(damageMessage.damager.name).ToString()));
                break;
            default:
                break;
        }
    }
}
