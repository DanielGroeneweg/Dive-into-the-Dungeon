using UnityEngine;
public class GameOverEventData : GameEventData
{
    public bool killed {  get; private set; }
    public GameOverEventData(bool killed)
    {
        this.killed = killed;
    }
}