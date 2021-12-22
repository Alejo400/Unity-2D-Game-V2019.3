using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    HealthManager HealthManager;
    public int currentLevel;
    public int currentLevelExp;
    public int [] level, Damage, Health, Defense, Mana;
    public Text lvlText;
    public static CharacterStats characterStats;
    private void Awake()
    {
        if (characterStats == null)
            characterStats = this;

        HealthManager = gameObject.GetComponent<HealthManager>();
    }
    private void Start()
    {
        setLevelStatsText();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLevel > level.Length)
            return;
        else
        {
            LevelUP();
            setLevelStatsText();
        }
            
    }

    void LevelUP()
    {
        if (currentLevelExp >= level[currentLevel] && currentLevel < level.Length - 1)
        {
            HealthManager.HealthLevelUP(Health[currentLevel],Mana[currentLevel]);
            if(currentLevel < level.Length - 1)
                currentLevel++;
        }
    }
    void setLevelStatsText()
    {
        lvlText.text = $"{currentLevel}\n{currentLevelExp}\n{level[currentLevel]}";
    }

    public void AddExperiencie(int exp)
    {
        currentLevelExp += exp;
    }
}
