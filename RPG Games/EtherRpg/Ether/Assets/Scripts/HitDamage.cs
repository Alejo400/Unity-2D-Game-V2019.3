using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
    public int damage;
    HealthManager HealthManager;
    public GameObject hitText;
    public GameObject blood;
    ParticleSystem cloneBlood;
    CharacterStats player;
    private void Start()
    {
        cloneBlood = blood.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthManager = collision.gameObject.GetComponent<HealthManager>();
        if(collision.gameObject.tag == "Enemy")
        {
            player = gameObject.GetComponentInParent<CharacterStats>();
            //Cambiando color a particle system
            var main = cloneBlood.main;
            main.startColor = Color.blue;
            Instantiate(blood, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

            int totalDamage = damage + player.Damage[player.currentLevel];

            HealthManager.GetDamage(totalDamage);

            var clone = Instantiate(hitText,
            collision.gameObject.transform.position,
            Quaternion.Euler(Vector3.zero)
            );
            clone.GetComponent<HitText>().hitText.text = totalDamage.ToString();
            clone.GetComponent<HitText>().hitText.color = Color.red;
        }
    }
}
