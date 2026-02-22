using UnityEngine;
public class EventBusManager : MonoBehaviour
{
    public static EventBusManager instance;
    [SerializeField] private HealPlayerEvent healPlayerEvent;
    [SerializeField] private DamagePlayerEvent damagePlayerEvent;
    [SerializeField] private EnemyDeathEvent enemyDeathEvent;
    [SerializeField] private UpdateStatsEvent updateStatsEvent;
    [SerializeField] private LoseManaEvent loseManaEvent;
    [SerializeField] private GainManaEvent gainManaEvent;
    public HealPlayerEvent HealPlayerEvent => healPlayerEvent;
    public DamagePlayerEvent DamagePlayerEvent => damagePlayerEvent;
    public EnemyDeathEvent EnemyDeathEvent => enemyDeathEvent;
    public UpdateStatsEvent UpdateStatsEvent => updateStatsEvent;
    public LoseManaEvent LoseManaEvent => loseManaEvent;
    public GainManaEvent GainManaEvent => gainManaEvent;
    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}