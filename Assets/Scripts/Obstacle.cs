using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public static bool FuelQuantity = true;
    void FixedUpdate()
    {
        transform.position += new Vector3(0,-World.worldSpeed);
        if (transform.position.y <= -6){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(FuelQuantity)
        {
            Destroy(gameObject);
        }
    }
}
