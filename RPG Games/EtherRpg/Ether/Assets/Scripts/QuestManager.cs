using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] quest;
    public bool[] completedQuest;
    public static QuestManager questManager;
    public string itemColleted;

    public string nameEnemyKilled;
    private void Awake()
    {
        if (questManager == null)
            questManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //El tamaño de completedQuest sera el de las quest que tendremos
        completedQuest = new bool[quest.Length];
    }
    public void StartQuest(string dialog)
    {
        string[] newDialog = new string[]{ dialog };
        DialogManager.dialogManager.ShowDialog(newDialog);
    }
}
