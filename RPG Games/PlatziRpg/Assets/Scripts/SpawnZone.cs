using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector2 spawnDirection = Vector2.zero;
    public string spawnZoneName;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();

        if (player.namePlace != spawnZoneName)
            return;

        player.transform.position = new Vector2(
            this.transform.position.x,
            this.transform.position.y
            );
        cameraFollow.transform.position = new Vector3(
            this.transform.position.x,
            this.transform.position.y,
            cameraFollow.transform.position.z
            );

        player.lastMovement = spawnDirection;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
