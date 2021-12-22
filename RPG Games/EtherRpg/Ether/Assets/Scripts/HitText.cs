using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitText : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Text hitText;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + moveSpeed * Time.deltaTime,
            transform.position.z
            );
    }
}
