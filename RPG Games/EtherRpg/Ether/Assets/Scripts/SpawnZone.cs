using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    PlayerMovement player;
    CameraFollow mainCamera;
    public string PlaceName;
    public Vector2 faceDirection = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        mainCamera = FindObjectOfType<CameraFollow>();

        if (PlayerMovement.nextPlace != PlaceName)
            return;

        player.transform.position = transform.position;
        mainCamera.transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            mainCamera.transform.position.z
            );
        player.lastMovement = faceDirection;
    }

}
