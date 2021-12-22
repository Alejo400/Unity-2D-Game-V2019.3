using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public Text damageNumber;
    public float speedText;
    public int damagePoint;
    // Update is called once per frame
    void Update()
    {

        damageNumber.text = damagePoint.ToString();
        
            transform.position = new Vector3(
            transform.position.x,
            transform.position.y + speedText * Time.deltaTime,
            transform.position.z
            );
    }
}
