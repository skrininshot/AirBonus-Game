using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToMainMenu : MonoBehaviour
{
    public GameObject restartObj;
    void Start()
    {
        StartGameBut.setDefault = true;
        restartObj.SetActive(false);
    }
}
