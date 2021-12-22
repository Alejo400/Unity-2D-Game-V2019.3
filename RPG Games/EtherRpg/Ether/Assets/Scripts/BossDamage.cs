using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    HealthManager healthManager;
    public int damage;
    public GameObject hitText;
    public GameObject blood;
    ParticleSystem bloodClone;
    CharacterStats player;
    BossController boss;
    PlayerMovement pj;

    int playerDefense;

    public float TimeToMakeDamage = 3f;
    private float TimeToDamageCounter;

    private void Start()
    {
        boss = FindObjectOfType<BossController>();
        bloodClone = blood.GetComponent<ParticleSystem>();
        TimeToDamageCounter = TimeToMakeDamage;
        pj = FindObjectOfType<PlayerMovement>();
    }
    private void Update()
    {
        if (boss.playerIsHere && pj.gameObject.activeInHierarchy)
        {
            TimeToDamageCounter -= Time.deltaTime;
            if (TimeToDamageCounter <= 0)
            {
                healthManager = pj.GetComponent<HealthManager>();
                var main = bloodClone.main;
                main.startColor = Color.red;
                Instantiate(blood, pj.transform.position, pj.transform.rotation);

                player = pj.GetComponent<CharacterStats>();
                playerDefense = player.Defense[player.currentLevel];
                int totalDamage = damage - playerDefense;
                if (totalDamage <= 0)
                    totalDamage = 1;

                healthManager.GetDamage(totalDamage);

                var clone = Instantiate(hitText,
                            pj.transform.position,
                            Quaternion.Euler(Vector3.zero)
                            );
                clone.GetComponent<HitText>().hitText.text = totalDamage.ToString();
                //Reiniciar contador
                TimeToDamageCounter = TimeToMakeDamage;
            }
        }
    }
}
