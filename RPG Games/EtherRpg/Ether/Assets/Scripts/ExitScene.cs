using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScene : MonoBehaviour
{
    public GameObject[] activeObjects;
    // Update is called once per frame
    void Update()
    {
        if (QuestManager.questManager.completedQuest[3])
        {
            foreach (GameObject thoseObjects in activeObjects)
            {
                thoseObjects.SetActive(true);
            }
        }
    }
}
