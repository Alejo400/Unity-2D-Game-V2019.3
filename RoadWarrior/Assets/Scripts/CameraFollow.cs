using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; //El objetivo que seguiremos
    public Vector3 offset = new Vector3(-3f, 0.0f, -10f); //A que distancia del target lo seguiremos
    public float dampingTime = 0.3f; //Tiempo de amortiguacion
    public Vector3 velocity = Vector3.zero; //Velocidad a la que debe ir la camara

    private void Awake()
    {
        //A cuantos frame rate queremos ir
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        
    }

    void Update()
    {
        MoveCamera(true);   
    }
    
    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }
    void MoveCamera(bool smooth) //smoth es movimiento suave con el dampingTime.
    {
        Vector3 destination = new Vector3(
            target.position.x - offset.x, //Nuestro offset se lo restamos a la posicion del target, para la distancia
            offset.y,
            offset.z
            );
        //Barrido suavizado hasta que reseteemos (morimos y reseteamos)
        if (smooth)
        {
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position,   //posicion actual donde esta la camara
                destination,   //Objetivo donde queremos ir (destino)
                ref velocity,    //Paso por referencia de la velocidad (variables ref) de un vector3
                dampingTime //smooth time
                );
        }
        else
        {
            this.transform.position = destination;
        }
    }
}
