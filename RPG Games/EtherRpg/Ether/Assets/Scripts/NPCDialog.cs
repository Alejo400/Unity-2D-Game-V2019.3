using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public string []dialog;
    public bool playerIsHere;

    private void Update()
    {
        if (playerIsHere && Input.GetKeyDown(KeyCode.Space) && !DialogManager.dialogManager.isActive)
        {
            DialogManager.dialogManager.ShowDialog(dialog);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerIsHere = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsHere = false;
            DialogManager.dialogManager.HideDialog();
        }
    }
}
