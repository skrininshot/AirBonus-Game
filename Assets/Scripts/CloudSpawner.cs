using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject CloudObj;
    public int interval;
    private float counter = 0;

    void FixedUpdate()
    {
        if (counter >= interval)
        {
            Instantiate(CloudObj, new Vector3(Random.Range(-3f, 3f), 8,transform.position.z), Quaternion.identity);
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime*(World.worldSpeed * 6);
        }
    }
}
