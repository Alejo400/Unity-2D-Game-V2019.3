using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBlock : MonoBehaviour
{
    public BoxCollider2D wallEndLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            wallEndLevel.isTrigger = false;
            FindObjectOfType<CameraFollow>().enabled = false;
        }
    }
}
