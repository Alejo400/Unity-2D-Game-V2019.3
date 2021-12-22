using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum StateGame {
    menuGame,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public StateGame currentStateGame = StateGame.menuGame;
    public static GameManager gameManager;
    Player player;
    AudioSource startMusic;
    AudioSource endMusic;
    // Start is called before the first frame update
    private void Awake()
    {
        if (gameManager == null) gameManager = this;
        startMusic = GameObject.Find("SoundBG").GetComponent<AudioSource>();
        endMusic = GameObject.Find("SoundEndBG").GetComponent<AudioSource>();
    }
    void Start()
    {
        GameMenu();
        player = GameObject.Find("PJ").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //StateGameManager();
    }
    /// <summary>
    /// Enumerador StateGame cambia al menu del juego
    /// </summary>
    public void GameMenu() { setCurrentGameState(StateGame.menuGame);  }
    /// <summary>
    /// Enumerador StateGame cambia a jugador en el juego
    /// </summary>
    public void InGame() { setCurrentGameState(StateGame.inGame); }
    /// <summary>
    /// Enumerador StateGame cambia a GameOver o final del juego
    /// </summary>
    public void GameOver() { setCurrentGameState(StateGame.gameOver); }

    /// <summary>
    /// Muestra y oculta menus, crea los bloques de niveles y reproduce sonidos
    /// </summary>
    /// <param name="SG">Identifica si el juego esta en estado Menu, Playing o GameOver</param>
    public void setCurrentGameState(StateGame SG)
    {
        if(SG == StateGame.menuGame) {
            MenuManager.menuManager.ShowMenuGame();
            MenuManager.menuManager.HideStartGame();
            MenuManager.menuManager.HideEndGame();
        }
        else if (SG == StateGame.inGame) {

            startMusic.Play();
            LevelManager.levelManager.RemoveAllLevelBlock();
            LevelManager.levelManager.GenerateLevelBlocks();

            GameView.gameView.resetScore();
            player.PlayerStartGame();

            MenuManager.menuManager.HideMenuGame();
            MenuManager.menuManager.ShowStartGame();
            MenuManager.menuManager.HideEndGame();
        }
        else if (SG == StateGame.gameOver) {
            startMusic.Stop();
            MenuManager.menuManager.HideMenuGame();
            MenuManager.menuManager.HideStartGame();
            MenuManager.menuManager.ShowEndGame();
        }

        this.currentStateGame = SG;
    }

    /*public void StateGameManager()
    {
        if (Input.GetButtonDown("Submit") && currentStateGame != StateGame.inGame) {
            InGame();
            player.PlayerStartGame();
        } 
    }*/
}
