using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSecene : MonoBehaviour
{
    public string scene = "";
    public string GoToPlaceName = "";
    public static LoadNewSecene loadNewScene;
    private void Awake()
    {
        // PARA QUE FUNCIONE EL USAR LA VARIABLE STATICA. TENEMOS ESTAS LINEAS COMO RECORDATORIO
        if (loadNewScene == null) 
            loadNewScene = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           PlayerMovement.nextPlace = GoToPlaceName;
           SceneManager.LoadScene(scene);
        }  
    }
}
