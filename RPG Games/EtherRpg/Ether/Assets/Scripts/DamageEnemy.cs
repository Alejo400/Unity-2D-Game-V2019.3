using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    HealthManager healthManager;
    public int damage;
    public GameObject hitText;
    public GameObject blood;
    ParticleSystem bloodClone;
    CharacterStats player;

    int playerDefense;

    private void Start()
    {
        bloodClone = blood.GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        healthManager = collision.gameObject.GetComponent<HealthManager>();
        if(collision.gameObject.tag == "Player")
        {
            var main = bloodClone.main;
            main.startColor = Color.red;
            Instantiate(blood, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

            player = collision.gameObject.GetComponent<CharacterStats>();
            playerDefense = player.Defense[player.currentLevel];
            int totalDamage = damage - playerDefense;
            if (totalDamage <= 0)
                totalDamage = 1;

            healthManager.GetDamage(totalDamage);

            var clone = Instantiate(hitText,
                        collision.gameObject.transform.position,
                        Quaternion.Euler(Vector3.zero)
                        );
            clone.GetComponent<HitText>().hitText.text = totalDamage.ToString();
        }
    }
}
