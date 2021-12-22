using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = new Vector3(
            Player.transform.position.x,
            Player.transform.position.y,
            gameObject.transform.position.z
            );
        transform.position = Vector3.Lerp(this.transform.position, targetPosition, speed);
    }
}
