using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject animationBlood;
    public GameObject positionHitPoint;
    public GameObject DamageNumber;

    public CharacterStats playerStats;
    public int damage;

    private void Start()
    {
        playerStats = GetComponentInParent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            int totalDamage = damage;
            totalDamage += playerStats.DamageLevel[playerStats.currentLevel];

            collision.gameObject.GetComponent<HealthManager>().DamageHealth(totalDamage);
            Instantiate(animationBlood,positionHitPoint.transform.position,positionHitPoint.transform.rotation);

            var clone = Instantiate(
                DamageNumber,
                positionHitPoint.transform.position,
                Quaternion.Euler(Vector3.zero)
                );
            clone.GetComponent<DamageNumber>().damagePoint = totalDamage;

        }
    }
}
