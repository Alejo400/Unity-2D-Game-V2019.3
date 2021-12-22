using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed = 1f;
    public bool isMoving;
    Vector2 movingEnemy;

    public bool isFollowtarget;
    PlayerMovement player;
    Transform positionTarget;
    float followSpeed = 3.5f;
    float distanceForFollow = 1.2f;
    public bool playerIsHere;

    public float timeToMove;
    private float timeToMoveCounter;
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    Animator animator;
    Rigidbody2D enemyRB;

    string moving = "Moving";
    string horizontalState = "Horizontal";
    string verticalState = "Vertical";
    public Vector3 startPosition;

    float moveRandomHorizontal, moveRandomVertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        player = FindObjectOfType<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        timeToMoveCounter = timeToMove * Random.Range(0.5f, 1.5f);
        positionTarget = player.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.activeInHierarchy)
        {
            if (!isFollowtarget)
            {
                return;
            }
            else
            {
                isFollowtarget = true;
                //Distancia entre el enemigo y el player. Si es mayor a 3, podemos seguir targeteando
                if (Vector2.Distance(transform.position, positionTarget.position) > distanceForFollow)
                {
                    playerIsHere = false;
                    followTarget();
                }
                else
                {
                    playerIsHere = true;
                    randomMovement();
                }
            }
            animator.SetBool(moving, isMoving);
            animator.SetFloat(horizontalState, movingEnemy.x);
            animator.SetFloat(verticalState, movingEnemy.y);
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }
    public void randomMovement()
    {
        if (!isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            if (timeToMakeStepCounter < 0)
            {
                isMoving = true;
                moveRandomHorizontal = Random.Range(-1, 2);
                moveRandomVertical = Random.Range(-1, 2);
                movingEnemy = new Vector2(moveRandomHorizontal, moveRandomVertical);
                timeToMakeStepCounter = timeToMakeStep;
            }
        }
        else
        {
            enemyRB.velocity = movingEnemy * speed;
            timeToMoveCounter -= Time.deltaTime;

            if (timeToMoveCounter < 0)
            {
                isMoving = false;
                enemyRB.velocity = Vector2.zero;
                timeToMoveCounter = timeToMove;
            }
        }
    }
    void followTarget()
    {
        //Activar movimiento de funcion random movement
        isMoving = true;
        //Seguir en posicion
        transform.position = Vector2.MoveTowards(transform.position, 
            positionTarget.position, 
            followSpeed * Time.deltaTime);
        //Mirar hacia donde el player vaya


    }
}
