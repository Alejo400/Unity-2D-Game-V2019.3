using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    /**** Variables. ****/

    public float npcSpeed;
    public bool isWalking;
    public float walkTime;
    public float waitTime;

    private float waitTimeCounter;
    private float walkCounter;
    private int currentDirection;

    /**** Components. ****/

    private Vector2[] walkingDirections =
    {
        //Definimos las diferentes direcciones que tomará nuestro npc.
        new Vector2(1,0),
        //new Vector2(0,1),
        new Vector2(-1,0),
        //new Vector2(0, -1)
    };

    private Rigidbody2D npcRigidbody;


    /**** Metudus. ****/

    // Start is called before the first frame update
    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody2D>();
        waitTimeCounter = waitTime;
        walkCounter = walkTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogManager.dialogManager.isActive)
        {
            npcRigidbody.velocity = Vector2.zero;
            return;
        }

        if (isWalking)
        {
            npcRigidbody.velocity = walkingDirections[currentDirection] * npcSpeed;
            walkCounter -= Time.deltaTime;
            if (walkCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            npcRigidbody.velocity = Vector2.zero;
            waitTimeCounter -= Time.deltaTime;

            if (waitTimeCounter < 0)
            {
                StartWalking();
            }
        }
    }

    private void StartWalking()
    {
        isWalking = true;
        currentDirection = Random.Range(0, 2);
        walkCounter = walkTime;
    }

    private void StopWalking()
    {
        isWalking = false;
        waitTimeCounter = waitTime;
        npcRigidbody.velocity = Vector2.zero;
    }
}
