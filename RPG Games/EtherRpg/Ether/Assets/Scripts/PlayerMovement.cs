using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D playerRB;
    Animator animator;
    HealthManager healthManager;

    public float speed = 3f;
    bool isMoving;
    bool isAttacking;
    bool isUsingEspecial;
    int  manaCostPerEspecial = 50;

    public bool stopPlayer;

    public float timeToAttack = 2f;
    private float timeToAttackCounter;
    public float timeToEspecial = 1.3f;
    private float timeToEspecialCounter;

    float horizontalMove;
    float verticalMove;
    public Vector2 lastMovement;

    public static bool playerCreated;
    public static string nextPlace;

    string walking = "Walking";
    string attacking = "Attacking";
    string special = "Special";
    string horizontalState = "Horizontal";
    string verticalState = "Vertical";
    string lastHorizontalState = "LastHorizontal";
    string lastVerticalState = "LastVertical";

    int pointHP = 100, pointMP = 100;
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthManager = GetComponent<HealthManager>();
        lastMovement = new Vector2(0,-1);

    }

    private void Start()
    {
        if (!playerCreated)
        {
            playerCreated = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopPlayer)
        {
            Attack();
            usingEspecial();
            MovePlayer();
        }
        else
            stopingPlayer();

        SetAnimation();
        drinkPotions();
    }
    void Attack()
    {
        if (Input.GetMouseButton(0) && QuestManager.questManager.completedQuest[0])
        {
            isAttacking = true;
        }
    }
    void usingEspecial()
    {
        if (Input.GetKeyDown(KeyCode.Q) && healthManager.currentMana >= 50 
            && QuestManager.questManager.completedQuest[1]
            && !isAttacking )
        {
            executeSpecial();
        }
    }
    /*Funcion creada para ejecutarla cuando completamos la quest 
     * y asi evitar el bug de no mostrar ataque a la primera instancia*/
    public void executeSpecial() {
        isUsingEspecial = true;
        healthManager.currentMana -= manaCostPerEspecial;
    }
    void drinkPotions()
    {
        if (Input.GetKeyDown(KeyCode.F1) && ItemsManager.itemsManager.HealthPotion > 0)
        {
            healthManager.currentHealth += pointHP;
            ItemsManager.itemsManager.HealthPotion--;
        }
        if(Input.GetKeyDown(KeyCode.F2) && ItemsManager.itemsManager.ManaPotion > 0)
        {
            healthManager.currentMana += pointMP;
            ItemsManager.itemsManager.ManaPotion--;
        }

    }
    void MovePlayer()
    {
        isMoving = false;
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        if (isAttacking)
        {
            isMoving = false;
            timeToAttackCounter -= Time.deltaTime;

            if(timeToAttackCounter <= 0)
            {
                isAttacking = false;
                timeToAttackCounter = timeToAttack;
            }
        }
        else
        {
            if (Mathf.Abs(horizontalMove) > 0.5f || Mathf.Abs(verticalMove) > 0.5f)
            {
                isMoving = true;
                lastMovement = new Vector2(horizontalMove, verticalMove);
                playerRB.velocity = lastMovement.normalized * speed;
            }
        }
        if(!isMoving) playerRB.velocity = Vector2.zero;

        if (isUsingEspecial)
        {
            stopingPlayer();
            isMoving = false;
            timeToEspecialCounter -= Time.deltaTime;

            if (timeToEspecialCounter <= 0)
            {
                isUsingEspecial = false;
                timeToEspecialCounter = timeToEspecial;
            }
        }
    }

    void SetAnimation()
    {
        animator.SetBool(walking, isMoving);
        animator.SetFloat(horizontalState, horizontalMove);
        animator.SetFloat(verticalState, verticalMove);
        animator.SetFloat(lastHorizontalState, lastMovement.x);
        animator.SetFloat(lastVerticalState, lastMovement.y);
        animator.SetBool(attacking, isAttacking);
        animator.SetBool(special, isUsingEspecial);
    }
    void stopingPlayer()
    {
        playerRB.velocity = Vector2.zero;
        isMoving = false;
    }
}
