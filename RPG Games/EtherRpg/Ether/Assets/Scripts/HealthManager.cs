using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int exp;
    public string nameCharacter;
    CharacterStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            gameObject.SetActive(false);

            if(gameObject.tag == "Enemy")
            {
                QuestManager.questManager.nameEnemyKilled = nameCharacter;
                playerStats.AddExperiencie(exp);
                Invoke("revive",30);
            }
        }
        fixedHealth();
    }
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
    }
    public void HealthLevelUP(int newHealth, int newMana)
    {
        maxHealth = newHealth;
        currentHealth = maxHealth;
        maxMana = newMana;
        currentMana = maxMana;
    }
    void revive()
    {
        currentHealth = maxHealth;
        gameObject.transform.position = gameObject.GetComponent<EnemyController>().startPosition;
        gameObject.SetActive(true);
    }
    void fixedHealth()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (currentMana > maxMana)
            currentMana = maxMana;
        if (currentMana <= 0)
            currentMana = 0;
    }
}
