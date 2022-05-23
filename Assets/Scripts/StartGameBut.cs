using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameBut : MonoBehaviour
{
    public Vector2 Direction = Vector2.right;
    public GameObject Help;
    public GameObject MainMenu;
    public bool CheckPress = false;
    private static Vector3 startPos;
    public static bool setDefault = false;

    private void Start()
    {
        startPos = MainMenu.transform.position;
    }
    IEnumerator waiter()
    {
        //MainMenu.transform.position = new Vector3(Mathf.Round(MainMenu.transform.position.x) + Direction.x * 100f, Mathf.Round(MainMenu.transform.position.y), 0f);
        MainMenu.SetActive(false);
        //this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + Direction.x * 100f, Mathf.Round(this.transform.position.y), 0f);
        //Help.transform.position = new Vector3(Mathf.Round(Help.transform.position.x) + Direction.x * 100f, Mathf.Round(Help.transform.position.y), 0f);
        yield return new WaitForSeconds(0.1f);
        CheckPress = false;
    }
    
    private void FixedUpdate()
    {
        if (setDefault)
        {
            MainMenu.transform.position = startPos;
            setDefault = false;
        }
        if(CheckPress)
        {
            StartCoroutine(waiter());
            if (Airplane.state == -1)
            {
                Airplane.state = 0;
                World.state = 1;
            }
        }
    }

    public void StartGame()
    {
      CheckPress = true;
    }
}
