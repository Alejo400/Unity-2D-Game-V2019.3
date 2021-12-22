using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    Player player;
    public float velocity;
    SpriteRenderer spriteRend;
    Rigidbody2D enemieRB;
    bool flip = false;
    AudioSource soundEffect;
    ParticleSystem enemieParticle;
    Animator animator;

    public float pushForce, durationDamage, countDurationDamage;
    public bool collisionPlayer = false;

    const string ENEMIE_LIVE = "enemieLive";
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PJ").GetComponent<Player>();
        enemieRB = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        soundEffect = GetComponent<AudioSource>();
        enemieParticle = GameObject.Find("EnemieParticle").GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        countDurationDamage = durationDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.currentStateGame == StateGame.inGame)
        {
            enemieRB.velocity = Vector2.right * velocity;
            spriteRend.flipX = flip;
        }
        //Si collisionamos con el player, haremos la animacion de hit
        if (collisionPlayer)
        {
            countDurationDamage -= Time.deltaTime;

            if (countDurationDamage < 0)
            {
                player.pushForDamage(false);
                collisionPlayer = false;
                countDurationDamage = durationDamage;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            velocity *= -1;
            flip = !flip;
        }

        if (collision.tag == "Player")
        {
            collisionPlayer = true;
            if (enemieRB.transform.position.y > player.transform.position.y)
            {
                player.pushForDamage(true, pushForce);
                player.collectHP(-20);
                soundEffect.Play();
            }
            else
            {
                enemieRB.velocity = Vector2.zero;
                animator.SetBool(ENEMIE_LIVE, false);
                Invoke("enemieDie", 0.5f);
            }
        }
    }
    /// <summary>
    /// Destruir al enemigo
    /// </summary>
    void enemieDie()
    {
        enemieParticle.transform.position = enemieRB.transform.position;
        enemieParticle.Play();
        Destroy(enemieRB.gameObject);
    }
}
