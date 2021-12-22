using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider BarHP;
    public Slider BarMP;
    HealthManager playerHM;
    public Text BarHpText;
    public Text BarMpText;
    public Text HP;
    public Text MP;
    private void Awake()
    {
        playerHM = GameObject.Find("Player").GetComponent<HealthManager>();
    }
    // Update is called once per frame
    void Update()
    {
        setBarHP();
        setPotions();
    }

    void setBarHP()
    {
        BarHP.maxValue = playerHM.maxHealth;
        BarHP.value = playerHM.currentHealth;
        BarMP.maxValue = playerHM.maxMana;
        BarMP.value = playerHM.currentMana;

        BarHpText.text = $"{playerHM.currentHealth}/{playerHM.maxHealth}";
        BarMpText.text = $"{playerHM.currentMana}/{playerHM.maxMana}";
    }

    void setPotions()
    {
        HP.text = ItemsManager.itemsManager.HealthPotion.ToString();
        MP.text = ItemsManager.itemsManager.ManaPotion.ToString();
    }
}
