using NUnit.Framework;
using System;
using UnityEngine;
[Serializable]
public class ItemDrop
{
    public Item item;
    [UnityEngine.Range(0f,1f)] public float dropChance;
}