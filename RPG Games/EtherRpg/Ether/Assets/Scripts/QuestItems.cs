using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItems : MonoBehaviour
{
    public int QuestID;
    public string ItemName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (QuestManager.questManager.quest[QuestID].gameObject.activeInHierarchy
                && !QuestManager.questManager.completedQuest[QuestID])
            {
                /*Le damos el nombre de nuestro item a QuestManager para que luego Quest.cs lo compare con el nombre de su item
                y ejecute completeQuest*/
                gameObject.SetActive(false);
                QuestManager.questManager.itemColleted = ItemName;
            }
        }
    }
}
