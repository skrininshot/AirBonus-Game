using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject[] obstacle;
    public Transform[] spawnPoints;
    public static float worldSpeed = 0.1f;
    public static float defaultSpeed;
    private float intervalBtwSpawn = 0.5f;
    private int SelectObject;
    

    private float counter;
    private int rndPosition;
    private int rndObstacle;
    private int rndNumberObject;


    public static int state = 0;

    void Start()
    {
        defaultSpeed = worldSpeed;
        counter = intervalBtwSpawn;
    }

    void RandomObject()
    {
        rndNumberObject = Random.Range(0, 10000);
        if(rndNumberObject >= 0 && rndNumberObject <= 500)
        {
            SelectObject = 1;
        }
        else if(rndNumberObject > 500 && rndNumberObject < 600) //Three percent bonus
        {
            if (Time.time > 5)
            {
                SelectObject = 2;
            }  
        }
        else if(rndNumberObject > 600 && rndNumberObject < 700) //Five percent bonus
        {
            if (Time.time > 5)
            {
                SelectObject = 3;
            }
        }
        else if(rndNumberObject > 700 && rndNumberObject < 750) //Super bonus
        {
            if (Time.time > 5)
            {
                SelectObject = 4;
            }
        }
        else
        {
            SelectObject = 0;
        }
    }

    void Update()
    {
        switch (state)
        {
            case 1:
                if (counter <= 0)
                {
                    RandomObject();
                    rndPosition = Random.Range(0, spawnPoints.Length);
                    if (Airplane.Fuel < 0.25f && GameObject.FindGameObjectsWithTag("Fuel").Length  == 1)
                    {
                        SelectObject = 1;
                    }
                    Instantiate(obstacle[SelectObject], spawnPoints[rndPosition].transform.position, Quaternion.identity);
                    counter = intervalBtwSpawn;
                    if (worldSpeed < 0.4)
                    {
                        worldSpeed += 0.001f;
                    }   
                }
                else
                {
                    counter -= Time.deltaTime*(worldSpeed*6);
                }
                break;
        }
        
    }
}
