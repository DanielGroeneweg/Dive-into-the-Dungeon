using System.Collections.Generic;
using UnityEngine;
public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<ObtainItemQuest> itemQuests = new();
    [SerializeField] private List<DefeatEnemyQuest> enemyQuests = new();
    [SerializeField] private QuestDisplayer displayerPrefab;
    [SerializeField] private GameObject displayerParent;
    private List<QuestDisplayer> displayers = new();
    private void OnEnable()
    {
        EventBusManager.Instance.GetItemEvent.Register(ItemQuestProgress);
        EventBusManager.Instance.EnemyDeathEvent.Register(EnemyQuestProgress);
    }
    private void OnDisable()
    {
        EventBusManager.Instance.GetItemEvent.Unregister(ItemQuestProgress);
        EventBusManager.Instance.EnemyDeathEvent.Unregister(EnemyQuestProgress);
    }
    private void Start()
    {
        foreach (ObtainItemQuest quest in itemQuests)
        {
            QuestDisplayer displayer = Instantiate(displayerPrefab, Vector3.zero, Quaternion.identity, displayerParent.transform);
            displayer.quest = quest;
            displayer.DisplayQuestProgress();
            displayers.Add(displayer);
        }

        foreach (DefeatEnemyQuest quest in enemyQuests)
        {
            QuestDisplayer displayer = Instantiate(displayerPrefab, Vector3.zero, Quaternion.identity, displayerParent.transform);
            displayer.quest = quest;
            displayer.DisplayQuestProgress();
            displayers.Add(displayer);
        }
    }
    private void ItemQuestProgress(GetItemEventData data)
    {
        foreach(ObtainItemQuest quest in itemQuests)
        {
            if (quest.ItemNeeded == data.Item) quest.ProgressQuest();
        }

        DisplayQuests();
    }
    private void EnemyQuestProgress(EnemyDeathEventData data)
    {
        foreach (DefeatEnemyQuest quest in enemyQuests)
        {
            if (quest.EnemyType == data.EnemyType) quest.ProgressQuest();
        }

        DisplayQuests();
    }
    private void DisplayQuests()
    {
        for (int i = displayers.Count - 1; i >= 0; i--)
        {
            QuestDisplayer displayer = displayers[i];
            displayer.DisplayQuestProgress();
            if (displayer.quest.Progress >= displayer.quest.AmountNeeded)
            {
                displayers.Remove(displayer);
                Destroy(displayer.gameObject);
            }
        }
    }
}