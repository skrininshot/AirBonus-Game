using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class RestartGameBut : MonoBehaviour
{
    public GameObject airplaneObj;
    public bool CheckPress = false;
    public GameObject restartMenu;
    private int PriorityS = 0;
    

    public void RestartGame()
    {
        CheckPress = true;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            CheckPress = true;
        }
        if (CheckPress)
        {
            if (Airplane.state == 2)
            { 
                ReadBonus();
                Airplane.Priority = PriorityS;
                Obstacle.FuelQuantity = true;
                airplaneObj.transform.position = new Vector3(0f, -6f, 0f);
                airplaneObj.transform.localScale = new Vector3(0.1342f, 0.1299f, 1f);
                airplaneObj.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                World.state = 1;
                Airplane.state = 0;
                World.worldSpeed = 0.1f;
                CheckPress = false;
                Airplane.Fuel = 1;
                restartMenu.SetActive(false);
            }
        }
    }

    private void ReadBonus()
    {
         XmlTextReader reader = new XmlTextReader("Assets/XML/Inventory.xml");
        while (reader.Read())
        {
            if(!reader.IsEmptyElement)
            {
                if(reader.Name != "Information" && reader.Name != "")
               if(reader.Name == "SuperBonus")
                {
                    PriorityS = 3;
                }
                else if(reader.Name == "BonusFive")
                {
                    PriorityS = 2;
                }
                else if(reader.Name == "Bonus")
                {
                    PriorityS = 1;
                }
                else
                {
                    PriorityS = 0;
                }
            }
        }
        reader.Close();
    }
}
