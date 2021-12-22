using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    bool playerCreated;

    void Awake()
    {
        playerCreated = PlayerMovement.playerCreated;

        if (!playerCreated)
            DontDestroyOnLoad(transform.gameObject);
        else
            Destroy(gameObject);
    }

}
