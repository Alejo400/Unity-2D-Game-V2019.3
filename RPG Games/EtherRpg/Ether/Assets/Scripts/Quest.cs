using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int QuestID;
    public string StartText, EndText;
    public static Quest questStatic;
    public int experiencie;

    public bool requireItem;
    public bool dependOtherQuest;
    public int idDependQuest;

    public string item;
    public bool requirePrizes;
    public GameObject[] prizes;
    public GameObject[] dependObjectsWhenInit;
    public GameObject[] dependObjectsWhenEnd;
    public int[] consumables;

    public bool requireEnemies;
    public string nameEnemies;
    public int numberOfEnemies;
    private int enemiesKilled;
    private void Awake()
    {
        if (questStatic == null)
            questStatic = this;
    }

    private void Update()
    {
        ItemCollected();
        KillEnemyToQuest();
    }

    public void StartQuest()
    {
        QuestManager.questManager.StartQuest(StartText);
    }

    public void CompletedQuest()
    {
        CharacterStats.characterStats.AddExperiencie(experiencie);
        GetPrizes();
        QuestManager.questManager.StartQuest(EndText);
        QuestManager.questManager.completedQuest[QuestID] = true;
        gameObject.SetActive(false);

        if(dependObjectsWhenEnd.Length > 0)
            activeDependObjectsForComplete();
        if (consumables.Length > 0)
            giveConsumables();
        //corregir bug de cuando el player hace por primera vez el especial
        if (QuestID == 1)
            GameObject.Find("Player").GetComponent<PlayerMovement>().executeSpecial();
    }

    public void ItemCollected()
    {
        if (requireItem && QuestManager.questManager.itemColleted == item)
        {
            CompletedQuest();
            QuestManager.questManager.itemColleted = "";
        }
    }

    public void GetPrizes()
    {
        //Si hemos marcado que esta quest requiere premio
        if (requirePrizes)
            prizes[0].SetActive(true);
    }

    public void KillEnemyToQuest()
    {
        if (requireEnemies && QuestManager.questManager.nameEnemyKilled.Equals(nameEnemies))
        {
            enemiesKilled++;
            QuestManager.questManager.nameEnemyKilled = "";
            if(enemiesKilled >= numberOfEnemies)
            {
                gameObject.SetActive(false);
                CompletedQuest();
            }
        }
    }
    //Activar los objetos que dependan de que esta quest cuando se inicie 
    public void activeDependObjects() 
    {
        foreach(GameObject thoseObjects in dependObjectsWhenInit)
        {
            thoseObjects.SetActive(true);
        }
    }
    //Activar los objetos que dependan de que esta quest cuando se complete
    public void activeDependObjectsForComplete()
    {
        foreach (GameObject thoseObjects in dependObjectsWhenEnd)
        {
            thoseObjects.SetActive(true);
        }
    }
    //Dar items consumibles como el caso de las pociones
    public void giveConsumables()
    {
        ItemsManager.itemsManager.HealthPotion += consumables[0];
        ItemsManager.itemsManager.ManaPotion += consumables[1];
    }
}
