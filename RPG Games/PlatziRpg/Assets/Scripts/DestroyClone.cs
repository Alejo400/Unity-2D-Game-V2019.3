using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClone : MonoBehaviour
{
    public float timeToDestroy;
    private float timeToDestroyCounter;

    private void Start()
    {
        timeToDestroyCounter = timeToDestroy;
    }

    // Update is called once per frame
    void Update()
    {
        timeToDestroyCounter -= Time.deltaTime;
        if(timeToDestroyCounter < 0)
        {
            Destroy(gameObject);
        }
    }
}
