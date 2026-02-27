using System;
using System.Collections.Generic;
[Serializable]
public class InventorySaveData
{
    public string headID;
    public string chestID;
    public string legID;
    public string footID;
    public string weaponID;
    public int potionAmount;
    public List<string> items = new();
}