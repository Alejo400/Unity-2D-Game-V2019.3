using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public static HealthManager hpManager;
    CharacterStats playerStats;
    public int exp;
    //public static HealthManager healthManager;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        DeadPlayer();
    }

    public void DamageHealth(int damage)
    {
        currentHealth -= damage;
    }
    public void DeadPlayer()
    {
        if (currentHealth <= 0)
        {
            if(gameObject.tag == "Enemy")
            {
                playerStats.AddExperience(exp);
            }

            gameObject.SetActive(false);
        }
    }
    public void AddHealthWhenLevelUp(int addHealth)
    {
        currentHealth = addHealth;
        maxHealth = addHealth;
    }
}
