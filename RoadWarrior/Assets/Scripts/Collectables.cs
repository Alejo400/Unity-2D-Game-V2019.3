using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeCollectable
{
    coin,
    healthPotion,
    manaPotion
}

public class Collectables : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider;
    public TypeCollectable typeCollectable = TypeCollectable.coin;
    int pointCoin = 100, ptoHP = 20, ptoMP = 10;
    Player player;
    AudioSource soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        player = GameObject.Find("PJ").GetComponent<Player>();
        soundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Collect();
        }
    }
    /// <summary>
    /// Ocultar, recoger y reproducir sonido de cada collecionable (monedas y pociones)
    /// </summary>
    public void Collect()
    {
        switch (typeCollectable)
        {
            case TypeCollectable.coin :
                Hide();
                GameView.gameView.setScore(pointCoin);
                soundEffect.Play();
                break;
            case TypeCollectable.healthPotion:
                Hide();
                player.collectHP(ptoHP);
                soundEffect.Play();
                break;
            case TypeCollectable.manaPotion:
                Hide();
                player.collectMP(ptoMP);
                soundEffect.Play();
                break;
        }
    }
    /// <summary>
    /// Desactivar sprite y collider de los coleccionables
    /// </summary>
    public void Hide()
    {
        spriteRenderer.enabled = false;
        circleCollider.enabled = false;
    }
}
