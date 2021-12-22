using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float velocity = 1f;
    public float timeToMakeStep;
    public float timeBetweenStep;
    public bool isMoving;
    Vector2 directionEnemy = Vector2.zero;

    private float timeToMakeStepCounter;
    private float timeBetweenStepCounter;

    Animator animator;
    Rigidbody2D enemyRB;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();

        //Agregar aleatoriedad de movimiento a cada enemigo
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        timeBetweenStepCounter = timeBetweenStep * Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            timeBetweenStepCounter -= Time.deltaTime;
            if(timeBetweenStepCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                directionEnemy = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * velocity;
            }
        }
        else
        {
            timeToMakeStepCounter -= Time.deltaTime;
            enemyRB.velocity = directionEnemy;

            if (timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepCounter = timeBetweenStep;
                enemyRB.velocity = Vector2.zero;
            }
        }

        animator.SetFloat("Horizontal", directionEnemy.x);
        animator.SetFloat("Vertical", directionEnemy.y);
    }
 }
