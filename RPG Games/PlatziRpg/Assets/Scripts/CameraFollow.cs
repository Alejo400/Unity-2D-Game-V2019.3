using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private float speed = 4.0f; //misma velocidad del pj
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(target.transform.position.x,
                                    target.transform.position.y,
                                    this.transform.position.z);
        /*suavizado con Interpolacion lineal: dados dos puntos, movernos
         de uno a otro de forma fluida
         */
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, speed * Time.deltaTime);
        /* este objeto se movera en 3 dimenciones:
         1) partiendo de su punto original, 
         2) a donde debe moverse la camara la cual es calculada por targetPosition, 
         3) bajo que parametro se movera pues es la velocidad por los frame spor segundo */
    }
}
