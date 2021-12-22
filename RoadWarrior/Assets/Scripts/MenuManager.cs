using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Canvas MenuCanvas, StartCanvas, EndCanvas;
    public static MenuManager menuManager;
    // Start is called before the first frame update
    private void Awake()
    {
        if (menuManager == null) menuManager = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowMenuGame() { MenuCanvas.enabled = true; }
    public void HideMenuGame() { MenuCanvas.enabled = false; }
    public void ShowStartGame() { StartCanvas.enabled = true; }
    public void HideStartGame() { StartCanvas.enabled = false; }
    public void ShowEndGame() { EndCanvas.enabled = true; }
    public void HideEndGame() { EndCanvas.enabled = false; }
    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
