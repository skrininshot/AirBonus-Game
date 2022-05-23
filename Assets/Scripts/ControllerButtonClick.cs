using UnityEngine;

public class ControllerButtonClick : MonoBehaviour
{
    public GameObject air;

    public void OnButtonDown()
    {
        if (this.name == "Left")
        {
            if (Airplane.pos > -1)
            {
                Airplane.pos--;
                Airplane.direction = new Vector3(Airplane.pos * 2, air.transform.position.y);
            }
        }
        if (this.name == "Right")
        {
            if (Airplane.pos < 1)
            {
                Airplane.pos++;
                Airplane.direction = new Vector3(Airplane.pos * 2, air.transform.position.y);
            }
        }
    }
}
