using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class QuestTrigger : MonoBehaviour
{
    public int QuestID;
    public bool pointStart, pointEnd;
    PlayerMovement playerMove;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!QuestManager.questManager.completedQuest[QuestID])
            {
                //active in hierarchy define si el objeto esta activo en la escena
                if (pointStart && !QuestManager.questManager.quest[QuestID].gameObject.activeInHierarchy)
                {
                    if (QuestManager.questManager.quest[QuestID].dependOtherQuest)
                    {
                        int idOtherQuest = QuestManager.questManager.quest[QuestID].idDependQuest;

                        if (QuestManager.questManager.completedQuest[idOtherQuest])
                        {
                            initQuest();
                        }          
                    }
                    else
                    {
                        initQuest();
                    }
                }
                if (pointEnd && QuestManager.questManager.quest[QuestID].gameObject.activeInHierarchy)
                {
                    QuestManager.questManager.quest[QuestID].CompletedQuest();
                }
            }
        }
    }
    void initQuest()
    {
        playerMove.stopPlayer = true;
        QuestManager.questManager.quest[QuestID].gameObject.SetActive(true);
        QuestManager.questManager.quest[QuestID].StartQuest();

        if(QuestManager.questManager.quest[QuestID].dependObjectsWhenInit.Length > 0)
            QuestManager.questManager.quest[QuestID].activeDependObjects();
    }
}
