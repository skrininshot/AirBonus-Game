using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class DelBonus : MonoBehaviour
{
    public GameObject[] Awards;

    public void UpdateObjects()
    {
        Awards[0].SetActive(false);
        Awards[1].SetActive(false);
        Awards[2].SetActive(false);
        Awards[3].SetActive(false);
    }

    public void DeleteBonus()
    {
        Airplane.Priority = 0;
        XmlDocument xmlDoc = new XmlDocument();
	    xmlDoc.Load("Assets/XML/Inventory.xml");
        XmlNode root = xmlDoc.DocumentElement;
        root.RemoveAll();
        xmlDoc.Save("Assets/XML/Inventory.xml");
        Debug.Log("Бонусы удалены!");
        DeletePromocodes();
        UpdateObjects();
    } 

    public void DeletePromocodes()
    {
        XmlDocument xmlDoc = new XmlDocument();
	    xmlDoc.Load("Assets/XML/Promocodes.xml");
        XmlNode root = xmlDoc.DocumentElement;
        root.RemoveAll();
        xmlDoc.Save("Assets/XML/Promocodes.xml");
        Debug.Log("Промокоды удалены!");
    }

   
}
