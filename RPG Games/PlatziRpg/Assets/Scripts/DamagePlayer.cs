using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;
    public GameObject damageNumber;
    GameObject damageZone;
    public GameObject blood;
    

    private void Start()
    {
        damageZone = GameObject.Find("DamageZone");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<HealthManager>().DamageHealth(damage);

            Instantiate(blood,
                       damageZone.transform.position,
                       damageZone.transform.rotation);

            var clone = Instantiate(damageNumber,
                                    damageZone.transform.position,
                                    damageZone.transform.rotation);
            clone.GetComponent<DamageNumber>().damagePoint = damage;
            clone.GetComponent<DamageNumber>().damageNumber.color = Color.red;
        }
    }
}
