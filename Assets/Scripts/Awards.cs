using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class Awards : MonoBehaviour
{
    public Text[] textPromo;
    public GameObject[] AwardsObjects;
    private bool Super = false;
    private bool Five = false;
    private bool Three = false; 
    public string Promocode = "";

    private void ReadBonus()
    {
        XmlTextReader reader = new XmlTextReader("Assets/XML/Inventory.xml");
        while (reader.Read())
        {
            if(!reader.IsEmptyElement)
            {
                if(reader.Name == "SuperBonus")
                {
                    Super = true;
                }
                else if(reader.Name == "BonusFive")
                {
                    Five = true;
                }
                else if(reader.Name == "Bonus")
                {
                    Three = true;
                }

            }
        }
        reader.Close();
    }

    private void ReadPromo()
    {
         XmlTextReader reader = new XmlTextReader("Assets/XML/Promocodes.xml");
        while (reader.Read())
        {
            if(!reader.IsEmptyElement)
            {
                if(reader.Name != "Information" && reader.Name != "")
                {
                    Promocode = reader.ReadString();
                }
            }
        }
        reader.Close();
        Debug.Log("Промокод равен = " + Promocode);
    }

    public void GenerateAwards()
    {
        Promocode = "";
        ReadPromo();
        ReadBonus();
        if(Super)
        {
            AwardsObjects[2].SetActive(true);
            textPromo[0].text = Promocode;
            Debug.Log("Super");
        }
        else if(Five)
        {
            AwardsObjects[1].SetActive(true);
            textPromo[1].text = Promocode;
            Debug.Log("5%");
        }
        else if(Three)
        {
            AwardsObjects[0].SetActive(true);
            textPromo[2].text = Promocode;
            Debug.Log("3%");
        }
        else
        {
            AwardsObjects[3].SetActive(false);
        }
        Promocode = "";
        UpdateAwards();
    }

    public void UpdateAwards()
    {
        Super = false;
        Five = false;
        Three = false;
    }
}
