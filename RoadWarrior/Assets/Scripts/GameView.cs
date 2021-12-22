using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text score, maxscore;
    public static GameView gameView;
    public int newScore = 0;
    private void Awake()
    {
        if (gameView == null) gameView = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Agrega puntos al score
    /// </summary>
    /// <param name="newPoint"></param>
    public void setScore(int newPoint)
    {
        if(GameManager.gameManager.currentStateGame == StateGame.inGame)
        {
            newScore += newPoint;
            score.text = newScore.ToString();
        }
    }
    /// <summary>
    /// Guarda la puntuacion final
    /// </summary>
    public void setMaxScore()
    {
        maxscore.text = PlayerPrefs.GetFloat("maxScore", 0).ToString();
    }
    /// <summary>
    /// Resetear el score al morir
    /// </summary>
    public void resetScore()
    {
        score.text = "0";
        newScore = 0;
    }
}
