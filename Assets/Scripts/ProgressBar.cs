using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] public Image Bar;
    //private static byte ColorBarR = 1;
    //private static byte ColorBarG = 255;
    //private int Percent;
    //public static bool AddFuel = false;
    

    public void Start()
    {
        Airplane.Fuel = 1;
        //ColorBarR = 1;
        //ColorBarG = 255;
        this.Bar.color = new Color32 (255, 255, 0, 255);
    }

    void FixedUpdate()
    {
        this.Bar.fillAmount = Airplane.Fuel;
        /*
        if(Percent %10==0)
        {   
            if(ColorBarR != 255)
            {
                ColorBarR += 1;
                this.Bar.color = new Color32 (ColorBarR, 255, 0, 255);
            }
            else if(ColorBarR == 255 && ColorBarG != 0)
            {
                ColorBarG -= 1;
                this.Bar.color = new Color32 (255, ColorBarG, 0, 255);
            }
        }  
        */
    }
}
