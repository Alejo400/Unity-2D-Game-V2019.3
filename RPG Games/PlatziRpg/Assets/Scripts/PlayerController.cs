using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Acciones de control del personaje
    public float velocity = 4f;
    bool isWalking;
    public Vector2 lastMovement = Vector2.zero;
    public string namePlace;
    bool attacking;
    public float timeToAttack;
    private float timeToAttackCounter;
    //Componentes
    Animator animator;
    Rigidbody2D rb;
    //String de animaciones
    string horizontalState = "Horizontal";
    string verticalState = "Vertical";
    string lastHorizontal = "LastHorizontal";
    string lastVertical = "LastVertical";
    string walking = "Walking";
    string attackingState = "Attacking";
    //Instancias
    public static bool playerHasLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        timeToAttackCounter = timeToAttack;

        if (!playerHasLoaded)
        {
            playerHasLoaded = true;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

        isWalking = false;
        float horizontal = Input.GetAxisRaw(horizontalState);
        float vertical = Input.GetAxisRaw(verticalState);

        if (Input.GetMouseButtonDown(0))
        {
            attacking = true;
            animator.SetBool(attackingState,true);
            rb.velocity = Vector2.zero;
        }

        if (attacking)
        {
            timeToAttackCounter -= Time.deltaTime;

            if(timeToAttackCounter < 0)
            {
                attacking = false;
                animator.SetBool(attackingState,false);
                timeToAttackCounter = timeToAttack;
            }
        }
        else
        {
            if (Mathf.Abs(horizontal) > 0.5f || Mathf.Abs(vertical) > 0.5f)
            {
                isWalking = true;
                lastMovement = new Vector2(horizontal, vertical);
                rb.velocity = lastMovement.normalized * velocity;
            }
        }

        if (!isWalking) rb.velocity = Vector2.zero;

        animator.SetBool(walking, isWalking);
        animator.SetFloat(lastHorizontal, lastMovement.x);
        animator.SetFloat(lastVertical, lastMovement.y);
        animator.SetFloat(horizontalState, horizontal);
        animator.SetFloat(verticalState, vertical);

    }
}
