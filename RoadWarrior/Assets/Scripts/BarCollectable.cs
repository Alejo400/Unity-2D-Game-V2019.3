using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    BarHP,
    BarMP
}

public class BarCollectable : MonoBehaviour
{
    public BarType barType = BarType.BarHP;
    Slider slider;
    Player player;
    public static BarCollectable barCollectable;
    // Start is called before the first frame update
    private void Awake()
    {
        if (barCollectable == null) barCollectable = this;
        slider = GetComponent<Slider>();
    }
    void Start()
    {
        player = GameObject.Find("PJ").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        setBarValue();
        KillPlayer();
    }
    /// <summary>
    /// Modifica el valor de las barras de vida y mana en el jugador
    /// </summary>
    public void setBarValue()
    {
        switch (barType)
        {
            case BarType.BarHP:
                slider.maxValue = Player.MAX_HP;
                slider.value = player.getHealth();
                break;
            case BarType.BarMP:
                slider.maxValue = Player.MAX_MP;
                slider.value = player.getMana();
                break;
        }
    }
    /// <summary>
    /// Ejecutar una funcion que mata al jugador, si la vida llega a 0
    /// </summary>
    public void KillPlayer()
    {
        switch (barType)
        {
            case BarType.BarHP:
                if (slider.value <= 0)
                    player.Die();     
                break;
        }
    }
}
