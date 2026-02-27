using System.Collections.Generic;
using System;
[Serializable]
public class SpellIDList
{
    public string[] ids = new string[10];
}
[Serializable]
public class SpellSaveData
{
    public List<SpellIDList> spellComponentIDs = new();
    public List<string> spellNames = new();
}