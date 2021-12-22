using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject BackGroundDialog;
    public Text DialogText;
    public bool isActive;
    public static DialogManager dialogManager;

    public int currentLine;
    public string []lines;
    PlayerMovement playerMove;

    private void Start()
    {
        if (dialogManager == null) 
            dialogManager = this;

        playerMove = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.Backspace))
        {
            currentLine++;
        }
        if(currentLine < lines.Length)
        {
            DialogText.text = lines[currentLine];
        }
        else
        {
            HideDialog();
        }
    }
    public void ShowDialog(string []texto)
    {
        isActive = true;
        BackGroundDialog.SetActive(true);
        currentLine = 0;
        lines = texto;
    }
    public void HideDialog()
    {
        isActive = false;
        BackGroundDialog.SetActive(false);
        currentLine = 0;
        playerMove.stopPlayer = false;
    }

}
