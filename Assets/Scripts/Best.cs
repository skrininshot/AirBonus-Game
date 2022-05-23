using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Best : MonoBehaviour
{
    public Text BestScoreText;
    private string BestScore;
   
    void Start()
    {
        ReadBest();
        BestScoreText.text = "Лучший счет: " + BestScore;
    }

    private void ReadBest()
    {
         XmlTextReader reader = new XmlTextReader("Assets/XML/BestScore.xml");
        while (reader.Read())
        {
            if(!reader.IsEmptyElement)
            {
                if(reader.Name != "Information" && reader.Name != "")
                {
                    BestScore = reader.ReadString();
                }
            }
        }
        reader.Close();
    }
}
