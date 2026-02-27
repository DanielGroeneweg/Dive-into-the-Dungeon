using UnityEngine;
[CreateAssetMenu(menuName = "EventBus/EventBus")]
public class EventBusManager : ScriptableObject
{
    static EventBusManager instance;
    [SerializeField] private HealPlayerEvent healPlayerEvent;
    [SerializeField] private DamagePlayerEvent damagePlayerEvent;
    [SerializeField] private EnemyDeathEvent enemyDeathEvent;
    [SerializeField] private UpdateStatsEvent updateStatsEvent;
    [SerializeField] private LoseManaEvent loseManaEvent;
    [SerializeField] private GainManaEvent gainManaEvent;
    [SerializeField] private GetItemEvent getItemEvent;
    [SerializeField] private EquipWeaponEvent equipWeaponEvent;
    [SerializeField] private GameOverEvent gameOverEvent;
    public HealPlayerEvent HealPlayerEvent => healPlayerEvent;
    public DamagePlayerEvent DamagePlayerEvent => damagePlayerEvent;
    public EnemyDeathEvent EnemyDeathEvent => enemyDeathEvent;
    public UpdateStatsEvent UpdateStatsEvent => updateStatsEvent;
    public LoseManaEvent LoseManaEvent => loseManaEvent;
    public GainManaEvent GainManaEvent => gainManaEvent;
    public GetItemEvent GetItemEvent => getItemEvent;
    public EquipWeaponEvent EquipWeaponEvent => equipWeaponEvent;
    public GameOverEvent GameOverEvent => gameOverEvent;
    public static EventBusManager Instance
    {
        get
        {
            if (instance == null) instance = Resources.Load<EventBusManager>("EventBus");
            return instance;
        }
    }
}