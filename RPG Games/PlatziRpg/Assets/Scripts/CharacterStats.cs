using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int currentLevel;
    public int currentExp;
    public int[] expLevels;

    public int[] HPLevel;
    public int[] DamageLevel;
    public int[] DefenseLevel;

    private HealthManager hpManager;

    private void Start()
    {
        hpManager = GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel >= expLevels.Length)
        {
            return;
        }
        else
        {
            if (currentExp >= expLevels[currentLevel])
            {
                hpManager.AddHealthWhenLevelUp(HPLevel[currentLevel]);
                currentLevel++;
            }       
        }
    }

    public void AddExperience(int exp)
    {
        currentExp += exp;
    }
}
