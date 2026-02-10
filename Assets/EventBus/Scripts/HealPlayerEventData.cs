using UnityEngine;

public class HealPlayerEventData
{
    public float healing { get; private set; }
    public bool isOverTime { get; private set; }
    public float time { get; private set; }
    public bool hasInitialBurst { get; private set; }
    public float initialBurst { get; private set; }
    public HealPlayerEventData(float healing, bool isOverTime, float time = 0, bool hasInitialBurst = false, float initialBurst = 0)
    {
        this.healing = healing;
        this.isOverTime = isOverTime;
        this.time = time;
        this.hasInitialBurst = hasInitialBurst;
        this.initialBurst = initialBurst;
    }

}
