using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    BossController boss;
    private void Start()
    {
        boss = FindObjectOfType<BossController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            boss.isFollowtarget = true;
        }
    }
}
