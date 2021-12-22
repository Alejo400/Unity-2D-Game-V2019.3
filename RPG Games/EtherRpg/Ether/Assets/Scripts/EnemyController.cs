using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1f;
    public bool isMoving;
    Vector2 movingEnemy;

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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f,1.5f);
        timeToMoveCounter = timeToMove * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            if(timeToMakeStepCounter < 0)
            {
                isMoving = true;
                movingEnemy = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
                timeToMakeStepCounter = timeToMakeStep;
            }
        }
        else
        {
            enemyRB.velocity = movingEnemy * speed;
            timeToMoveCounter -= Time.deltaTime;

            if(timeToMoveCounter < 0)
            {
                isMoving = false;
                enemyRB.velocity = Vector2.zero;
                timeToMoveCounter = timeToMove;
            }
        }

        animator.SetBool(moving,isMoving);
        animator.SetFloat(horizontalState, movingEnemy.x);
        animator.SetFloat(verticalState, movingEnemy.y);

    }
}
