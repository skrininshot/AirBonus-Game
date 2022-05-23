using UnityEngine;

public class CloudBG : MonoBehaviour
{
    public Sprite[] sprites;
    private int rndSprite;
    private float speed;
    private Vector3 rotate;
    void Start()
    {
        rotate = (Random.Range(0, 1) == 0 ? -Vector3.forward : Vector3.forward);
        speed = Random.Range(1,3);
        rndSprite = Random.Range(0,sprites.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[rndSprite];
    }

    void FixedUpdate()
    {
        transform.position -= new Vector3(0,(speed*World.worldSpeed)/10);
        transform.eulerAngles += rotate * ((speed * World.worldSpeed)/10);
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }
}
