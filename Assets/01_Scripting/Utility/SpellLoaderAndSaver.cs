using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Collections;
public class SpellLoaderAndSaver : MonoBehaviour
{
    [SerializeField] private SpellComponentDataBase dataBase;
    [SerializeField] List<SpellDefinition> spells = new();
    public void SaveSpells(GameOverEventData data)
    {
        List<SpellIDList> spellIDlist = new List<SpellIDList>();
        List<string> spellNames = new List<string>();

        for (int spellIndex = 0; spellIndex < spells.Count; spellIndex++)
        {
            // Save SpellName
            spellNames.Add(spells[spellIndex].spellName);

            // Save Spell Component IDs
            string[] ids = new string[10];

            for (int componentIndex = 0; componentIndex < 10; componentIndex++)
            {
                if (spells[spellIndex].components[componentIndex] != null) ids[componentIndex] = (spells[spellIndex].components[componentIndex].SpellComponentID);

                else ids[componentIndex] = string.Empty;
            }

            spellIDlist.Add(new SpellIDList { ids = ids });
        }

        // Save to JSON
        SpellSaveData savedata = new SpellSaveData
        {
            spellComponentIDs = spellIDlist,
            spellNames = spellNames,
        };

        string path = Application.persistentDataPath + "/Spells.json";
        string json = JsonUtility.ToJson(savedata);
        File.WriteAllText(path, json);
    }
    private IEnumerator LoadSpells()
    {
        yield return new WaitForEndOfFrame();

        // Load from JSON
        string path = Application.persistentDataPath + "/Spells.json";
        if (!File.Exists(path)) yield break;

        string json = File.ReadAllText(path);
        SpellSaveData savedata = JsonUtility.FromJson<SpellSaveData>(json);

        // Loop through spells
        for (int spellIndex = 0; spellIndex < savedata.spellComponentIDs.Count; spellIndex++)
        {
            string[] idList = savedata.spellComponentIDs[spellIndex].ids;

            SpellDefinition spell = spells[spellIndex];

            // Load SpellName
            spell.spellName = savedata.spellNames[spellIndex];

            // Load Spell Components by IDs
            // Spell Components
            for (int componentIndex = 0; componentIndex < 10; componentIndex++)
            {
                if (string.IsNullOrEmpty(idList[componentIndex]))
                {
                    spell.components[componentIndex] = null;
                    continue;
                }

                // Find component
                SpellComponent component = dataBase.Components[idList[componentIndex]];

                // Set Spell Component to found component
                if (component is SpellEffect effect) spell.components[componentIndex] = effect;
                else if (component is SpellModifier modifier) spell.components[componentIndex] = modifier;
                else if (component is SpellForm form) spell.components[componentIndex] = form;
            }
        }
    }
    private void OnEnable()
    {
        StartCoroutine(LoadSpells());
        StartCoroutine(Link());
    }
    private IEnumerator Link()
    {
        yield return new WaitForEndOfFrame();
        GameManager.Instance.LinkGameOverEvent(SaveSpells);
    }
    private void OnDisable()
    {
        GameManager.Instance.UnlinkGameOverEvent(SaveSpells);
    }
    private void OnApplicationQuit()
    {
        SaveSpells(new GameOverEventData(false));
    }
}