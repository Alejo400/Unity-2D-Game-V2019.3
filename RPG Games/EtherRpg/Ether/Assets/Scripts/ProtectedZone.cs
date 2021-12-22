using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<HealthManager>().currentHealth = 0;
        }
    }
}
