using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFuel : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += new Vector3(0,-World.worldSpeed);
        if (transform.position.y <= -6){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
