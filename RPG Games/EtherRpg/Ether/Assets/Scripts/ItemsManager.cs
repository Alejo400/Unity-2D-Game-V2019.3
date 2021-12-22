using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager itemsManager;
    public int HealthPotion, ManaPotion;
    private void Awake()
    {
        if (itemsManager == null) itemsManager = this;
        HealthPotion = 0;
        ManaPotion = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
