using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string NewScene = "Default Scene";
    public string GoToNamePlace;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<PlayerController>().namePlace = GoToNamePlace;
            SceneManager.LoadScene(NewScene);
        }
    }
}
