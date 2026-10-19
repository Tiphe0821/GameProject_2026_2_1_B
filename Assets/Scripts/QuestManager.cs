using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;

    private Quest currentQuest; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questText.text = "진행중인 퀘스트가 없습니다.";
    }

    private void StartQuest(Quest newQuest)
    {
        currentQuest = newQuest;
        currentQuest.currentAmount = 0;
        currentQuest.state = QuestState.InProgress;

        Debug.Log(currentQuest.questName = "퀘스트 시작");
        UpdateQuestText();
    }

    public void AddQuestItem(string itemName, int amount)
    {
        if (currentQuest == null) return;
        if (currentQuest.state != QuestState.InProgress) return;
        if (currentQuest.targetItemName != itemName) return;

        currentQuest.currentAmount += amount;

        if(currentQuest.currentAmount >= currentQuest.targetAmount)
        {
            currentQuest.currentAmount = currentQuest.targetAmount;
            currentQuest.state = QuestState.ReadyToComplete;
        }

        UpdateQuestText();
    }

    public void TalkToNPC(Quest npcQuest)
    {
        if (npcQuest.state == QuestState.Completed)
        {
            return;
        }
        if (currentQuest == null || currentQuest.state == QuestState.Completed)
        {
            StartQuest(npcQuest);
            return;
        }
        if(currentQuest != npcQuest)
        {
            return;
        }

        if(currentQuest.state == QuestState.InProgress)
        {
            Debug.Log("아직 아이템이 부족해");
        }
    }
    public void CompleteQuest()
    {
        currentQuest.state = QuestState.Completed;

        UpdateQuestText();
    }

    private void UpdateQuestText()
    {
        if (currentQuest.state == QuestState.InProgress) 
        {

            questText.text =
                currentQuest.questName + "\n" +
                currentQuest.targetItemName + "\n" +
                currentQuest.currentAmount + "/" +
                currentQuest.targetAmount;
        }
        else if(currentQuest.state == QuestState.ReadyToComplete)
        {
            questText.text = "ready to complete";
        }
        else if(currentQuest.state == QuestState.Completed)
        {
            questText.text = currentQuest.questName + "\n퀘스트 완료";
        }
    }
}
