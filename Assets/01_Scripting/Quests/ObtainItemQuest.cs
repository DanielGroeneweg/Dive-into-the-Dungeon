using System;
using UnityEngine;
[Serializable]
public class ObtainItemQuest : Quest
{
    [SerializeField] private Item itemNeeded;
    public Item ItemNeeded => itemNeeded;
    public override void ProgressQuest()
    {
        Progress++;
    }
}
