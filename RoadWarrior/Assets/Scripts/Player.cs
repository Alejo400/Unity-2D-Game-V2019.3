using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //Variables de Accion
    public float jumpForce = 8f, velocity = 4f;
    private float newJumpForce;

    bool move = true;
    bool pushFlip;

    //bool isTouchingTheGround = true;
    //Entorno
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    public Animator animator;
    public LayerMask groundMask;
    //Efectos de sonido
    AudioSource soundEJump, soundEDie;
    //AudioSource soundEAttack;
    //Animaciones
    const string IS_RUNNING = "isRunning";
    //const string ATTACK_ENEMIE = "attackEnemie";
    const string IS_ON_THE_GROUND = "isOnTheGround", IS_ALIVE = "isAlive", SUPER_JUMP = "SuperJump",
                 DAMAGE_ENEMIE = "damageEnemie";
    //Posiciones
    Vector3 position;
    //Variables de vida y maná
    int HP, MP;
    public const int MAX_HP = 100, MAX_MP = 50, INITIAL_HP = 100, INITIAL_MP = 20, COST_MANA_SUPERJUMP = 3;
   //COST_MANA_ATTACK = 5;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        soundEJump = GameObject.Find("soundJump").GetComponent<AudioSource>();
        //soundEAttack = GameObject.Find("soundAttack").GetComponent<AudioSource>();
        soundEDie = GameObject.Find("soundDie").GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if (GameManager.gameManager.currentStateGame == StateGame.inGame && move)
            Running();
    }
    // Start is called before the first frame update
    void Start()
    {
        InitialBarValue();
        isRunning(false);
        //attackEnemie(false);
        isOnTheGround(true);
        position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.gameManager.currentStateGame == StateGame.inGame)
        {
            isOnTheGround(isTouchingGround()); // Juan Gabriel Gomila
            //isOnTheGround(isTouchingTheGround); // Daniela
            //if (isTouchingGround()) Jump(); // Juan Gabriel Gomila
            if (isTouchingGround()) Jump(); // Daniela
            //Attack();
        }
    }
    /// <summary>
    /// Mover al jugador horizontalmente
    /// </summary>
    public void Running()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(MoveHorizontal * velocity,rigidBody.velocity.y);

        if (Math.Abs(MoveHorizontal) != 0)
        {
            isRunning(true);
            if (MoveHorizontal > 0)
                Flip(false);
            else
                Flip(true);
        }
        else
            isRunning(false);

        /*if(MoveHorizontal > 0)
        {
            isRunning(true);
            Flip(false);
        }else if (MoveHorizontal < 0)
        {
            isRunning(true);
            Flip(true);
        }
        else
        {
            isRunning(false);
        }*/
    }
    /// <summary>
    /// Hacer girar al jugador
    /// </summary>
    /// <param name="flip">Booleano. Cambia su valor cuando se presiona left o right</param>
    public void Flip(bool flip)
    {
        pushFlip = flip;
        spriteRenderer.flipX = flip;
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Q))
        {
            newJumpForce = jumpForce;
            if (Input.GetKeyDown(KeyCode.Q) && MP >= COST_MANA_SUPERJUMP)
            {
                SuperJump(true);
                newJumpForce += 2;
                MP -= COST_MANA_SUPERJUMP;
            }
            else
                SuperJump(false);

            //isTouchingTheGround = false; // Daniela
            soundEJump.Play();
            rigidBody.AddForce(Vector2.up * newJumpForce, ForceMode2D.Impulse);
        }
    }
    //Verificar si esta tocando el suelo, con Raycast
    bool isTouchingGround()
    {
        if(Physics2D.Raycast(this.transform.position,
                             Vector2.down,
                             1.5f,
                             groundMask
                             ))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Método de la profesora Daniela Coyotzi. Detectar el suelo
   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            isTouchingTheGround = true;
    }*/

    /*void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q) && MP > 0)
        {
            soundEAttack.Play();
            MP -= COST_MANA_ATTACK;
            attackEnemie(true);
        }
        else
        {
            attackEnemie(false);
        }
    }*/
    /// <summary>
    /// Empujar al jugador mediante un impulso, si es atacado
    /// </summary>
    /// <param name="damage">Bool. Si recibe o no daño</param>
    /// <param name="pushForce">Fuerza de impulso</param>
    public void pushForDamage(bool damage, float pushForce = 20)
    {
        if (damage)
        {
            Vector2 direction;
            animator.SetBool(DAMAGE_ENEMIE, true);
            move = false;

            if (pushFlip)
                direction = Vector2.right;
            else
                direction = Vector2.left;

            rigidBody.AddForce(direction * pushForce, ForceMode2D.Impulse);
        }
        else
        {
            animator.SetBool(DAMAGE_ENEMIE, false);
            move = true;
        }
    }

    public void Die()
    {
        //Paralizamos al personaje si no esta en el suelo
        if (isTouchingGround() || rigidBody.transform.position.y < -3)
            rigidBody.velocity = Vector2.zero; //Cuando morimos, dejamos de accelerar la velocidad para q no lo siga la camara

        isAlive(false);
        GameManager.gameManager.GameOver();

        if(GameView.gameView.newScore > PlayerPrefs.GetFloat("maxScore", 0))
        {
            PlayerPrefs.SetFloat("maxScore", GameView.gameView.newScore);
        }
    }
    /// <summary>
    /// Todas las configuraciones a aplicar, cuando el juego empieza
    /// </summary>
    public void PlayerStartGame()
    {
        InitialBarValue();
        isAlive(true);
        isOnTheGround(true);
        Flip(false);
        //Invoke("PlayerStartPosition",0f); SI LO USAMOS ASI, SE NOS PEGA EL RESTART GAME Y LA ANIMACION
        PlayerStartPosition();
        GameView.gameView.setMaxScore();
    }
    /// <summary>
    /// Posición inicial del jugador. Usado cuando se reinicia la partida o el jugador muere
    /// </summary>
    public void PlayerStartPosition()
    {
            rigidBody.position = position;
            rigidBody.velocity = Vector2.zero;
    }
    //Setear animaciones
    /// <summary>
    /// Modificador de la animación de correr
    /// </summary>
    /// <param name="isRun">si esta o no corriendo</param>
    void isRunning(bool isRun) { animator.SetBool(IS_RUNNING, isRun); }
    //void attackEnemie(bool attack) { animator.SetBool(ATTACK_ENEMIE,attack); }
    /// <summary>
    /// Modificador de la animación de salto
    /// </summary>
    /// <param name="onGround">si esta o no en el suelo</param>
    void isOnTheGround(bool onGround) { animator.SetBool(IS_ON_THE_GROUND, onGround); }
    /// <summary>
    /// Modificador de la animación de muerte
    /// </summary>
    /// <param name="isAlive">Si esta o no vivo</param>
    void isAlive(bool isAlive) { animator.SetBool(IS_ALIVE, isAlive); }
    /// <summary>
    /// Modificar de la animación de Super Salto
    /// </summary>
    /// <param name="superJump">Si hara o no un super alto</param>
    void SuperJump(bool superJump) { animator.SetBool(SUPER_JUMP, superJump); }

    /// <summary>
    /// Barra de vida
    /// </summary>
    /// <param name="ptoHP">Puntos de vida</param>
    public void collectHP(int ptoHP)
    {
        HP += ptoHP;
        if (HP >= MAX_HP)
        {
            HP = MAX_HP;
        }
    }
    /// <summary>
    /// Barra de mana
    /// </summary>
    /// <param name="ptoMP">Puntos de mana</param>
    public void collectMP(int ptoMP)
    {
        MP += ptoMP;
        if (MP >= MAX_MP)
        {
            MP = MAX_MP;
        }
    }
    /// <summary>
    /// Obtener puntos de vida
    /// </summary>
    /// <returns></returns>
    public int getHealth()
    {
        return HP;
    }
    /// <summary>
    /// Obtener puntos de mana
    /// </summary>
    /// <returns></returns>
    public int getMana()
    {
        return MP;
    }
    /// <summary>
    /// Valores iniciales de vida y mana, cuando el juego inicia
    /// </summary>
    public void InitialBarValue()
    {
        HP = INITIAL_HP;
        MP = INITIAL_MP;
    }
}
