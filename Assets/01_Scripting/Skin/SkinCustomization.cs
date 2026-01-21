using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
public class SkinCustomization : MonoBehaviour
{
    [SerializeField] private PlayerSaveData saveData;

    [SerializeField] Material hairMaterial;
    [SerializeField] Material eyesMaterial;
    [SerializeField] Material skinMaterial;

    [SerializeField] FlexibleColorPicker hairPicker;
    [SerializeField] FlexibleColorPicker eyesPicker;
    [SerializeField] FlexibleColorPicker skinPicker;

    [SerializeField] private UnityEvent<Dictionary<SkinParts, Color>> saveColors;

    private static string savePath;
    void Awake()
    {
        savePath = Application.persistentDataPath + "/PlayerSaveData.json";
        if (!File.Exists(savePath)) return;
        string json = File.ReadAllText(savePath);
        SkinSaveData data = JsonUtility.FromJson<SkinSaveData>(json);
        saveData.skinData = data;

        hairMaterial.color = saveData.skinData.hairColor;
        eyesMaterial.color = saveData.skinData.eyeColor;
        skinMaterial.color = saveData.skinData.skinColor;

        hairPicker.SetColor(saveData.skinData.hairColor);
        eyesPicker.SetColor(saveData.skinData.eyeColor);
        skinPicker.SetColor(saveData.skinData.skinColor);
    }

    public void SaveColor()
    {
        Dictionary<SkinParts, Color> dict = new Dictionary<SkinParts, Color>();
        dict.Add(SkinParts.Hair, hairPicker.GetColor());
        dict.Add(SkinParts.Eyes, eyesPicker.GetColor());
        dict.Add(SkinParts.Skin, skinPicker.GetColor());
        saveColors?.Invoke(dict);
    }

    // TODO:
    // MOVE THIS TO ANOTHER SCRIPT/OBJECT
    public void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(saveData.skinData);
        File.WriteAllText(savePath, json);
    }
}