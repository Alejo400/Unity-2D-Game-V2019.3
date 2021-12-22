using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    Player player;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PJ").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.Die();
        }
    }
}
