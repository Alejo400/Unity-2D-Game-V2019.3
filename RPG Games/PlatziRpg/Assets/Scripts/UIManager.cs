using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //GameObject Player;
    public HealthManager PlayerHPManager;
    int maxHP;
    int currentHP;
    public Text HPText;
    public Text LevelStats;
    public Slider HPBar;
    public CharacterStats playerStats;

    void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        SetHpUI();
        SetLevelStats();
    }

    public void SetHpUI()
    {
        currentHP = PlayerHPManager.currentHealth;
        maxHP = PlayerHPManager.maxHealth;

        HPBar.maxValue = maxHP;
        HPBar.minValue = 0;
        HPBar.value = currentHP;

        HPText.text = currentHP.ToString() + " / " + maxHP;
    }
    public void SetLevelStats()
    {
        LevelStats.text = $"{playerStats.currentLevel} " + "\n" +
            $"{playerStats.currentExp} " + "\n" +
            $"{playerStats.expLevels[playerStats.currentLevel]}";
    }
}
