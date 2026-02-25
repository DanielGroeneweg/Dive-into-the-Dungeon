using TMPro;
using UnityEngine;
public class QuestDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Presenter progressPresenter;
    [HideInInspector] public Quest quest;
    public void DisplayQuestProgress()
    {
        if (quest == null) return;

        nameText.text = quest.QuestName;
        progressPresenter.SetValue(0, quest.AmountNeeded, quest.Progress);

        if (quest is DefeatEnemyQuest enemyQuest)
        {
            descriptionText.text = $"Kill {enemyQuest.AmountNeeded} {enemyQuest.EnemyType}(s).";
        }

        else if (quest is ObtainItemQuest itemQuest)
        {
            descriptionText.text = $"Obtain {itemQuest.AmountNeeded} {itemQuest.ItemNeeded.Name}(s).";
        }
    }
}