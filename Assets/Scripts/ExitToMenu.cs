using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenu : MonoBehaviour
{
  public void ExitMenu()
  {
        World.worldSpeed = World.defaultSpeed;
      SceneManager.LoadScene ("SampleScene");
      Debug.Log("Exit to main menu");
  }
}
