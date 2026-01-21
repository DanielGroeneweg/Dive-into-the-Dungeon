using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class SkinSaveData
{
    public Color skinColor;
    public Color hairColor;
    public Color eyeColor;
}

[CreateAssetMenu(fileName = "PlayerSaveData", menuName = "Scriptable Objects/PlayerSaveData")]
public class PlayerSaveData : ScriptableObject
{
    public SkinSaveData skinData;
    public void SaveSkinColors(Dictionary<SkinParts, Color> dict)
    {
        foreach (SkinParts skinPart in dict.Keys)
        {
            switch (skinPart)
            {
                case SkinParts.Hair:
                    skinData.hairColor = dict[skinPart];
                    break;
                case SkinParts.Eyes:
                    skinData.eyeColor = dict[skinPart];
                    break;
                case SkinParts.Skin:
                    skinData.skinColor = dict[skinPart];
                    break;
            }
        }
    }
}