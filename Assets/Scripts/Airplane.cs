using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class Airplane : MonoBehaviour
{
    public GameObject restartMenu;
    public float smooth = 3; 
    public static int pos = 0; 
    public static Vector3 direction; 
    private Vector3 landing;
    private float landingSpeed = 0.1f;
    public static int state;
    public static float Fuel = 1;
    private float SpendingSpeed = 1f;
    public GameObject Controller;
    private bool disappear;
    public GameObject LifeBar;
    public Vector3 BarPos;
    public static int Priority = 0;
    private int Randomcharacter = 0;
    private string[] Character = new string[] {"A", "F", "I", "X", "Z", "M", "Y", "T", "H", "K", "W", "Q", "E", "O", "P", "S", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0"}; 
    private string Promocode = "";
    private float score;
    public int finalScore;
    public Text scoreText;
    public Text BestScoreText;
    public string BestScore;
    


    void Start()
    {
        direction = new Vector3(0, transform.position.y);
        landing = new Vector3(0, 0);
        state = -1;
        disappear = false;
        BarPos = LifeBar.transform.position;
        LifeBar.transform.position = new Vector3(BarPos.x, BarPos.y - 100f);
    }

    //airplane movement
    void Update()
    {
        ReadBonus();
        if (state != 1) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (pos > -1)
            {
                pos--;
                direction = new Vector3(pos * 2, transform.position.y);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (pos < 1)
            {
                pos++;
                direction = new Vector3(pos * 2, transform.position.y);
            }
        }
    }

    //statements
    void FixedUpdate()
    {
        ReadBonus();
        switch (state)
        {
            //start animation
            case 0:
                AnimationOnStart();
                break;

            //playing
            case 1:
                //Getting up lifebar animation
                if (LifeBar.transform.position.y < BarPos.y)
                {
                    LifeBar.transform.position += new Vector3(0, (BarPos.y - LifeBar.transform.position.y) / smooth);
                }

                if (transform.position.x != direction.x)
                {
                    transform.position += new Vector3(((direction.x - transform.position.x) / smooth)*(World.worldSpeed*5), 0);
                }
                

                if (Fuel <= 0)
                {
                    //off the airplane
                    state = 2;
                    GetComponent<Collider2D>().enabled = false;
                    Controller.SetActive(false);
                    LifeBar.transform.position = new Vector3(BarPos.x, BarPos.y - 100f);
                    score = 0;
                    scoreText.text = "";
                    transform.position += new Vector3(0, 0, 10);

                    //set obstacle spawn off
                    World.state = 0;
                    Obstacle.FuelQuantity = false;

                }
                else
                {
                    Fuel -= (SpendingSpeed / 2500);
                    //score
                    score += World.worldSpeed;
                    finalScore = (int)Mathf.Round(score);
                    scoreText.text = finalScore.ToString();
                }
                break;

            //landing
            case 2:

                 Priority = 0;
                //changing y-position
                transform.position += new Vector3(((direction.x - transform.position.x) / smooth) * (World.worldSpeed * 5), 0);
                if (transform.position.y < landing.y)
                {
                    transform.position += new Vector3(0, (landing.y - transform.position.y)*landingSpeed*World.worldSpeed); 
                }

                //changing size
                if (transform.localScale.x > 0.02f)
                {
                    transform.localScale -= new Vector3((transform.localScale.x / (smooth*10)) / 3, (transform.localScale.y / (smooth * 10)) / 3);
                    //if size is ... - death
                    if (transform.localScale.x < 0.1 && !disappear)
                    {
                        Debug.Log("Landing");
                        disappear = true;
                        ReadBest();
                        int PPP = int.Parse(BestScore);
                        Debug.Log("PPP = " + PPP);
                        if(PPP < finalScore)
                        {
                            PPP = finalScore;
                            BestScore = PPP.ToString();
                            Debug.Log("Best = " + BestScore);
                            Debug.Log("final = " + finalScore);
                            SaveBest();
                        }
                        restartMenu.SetActive(true);
                    }
                    if (transform.localScale.x < 0.05 && transform.position.z < 15)
                    {
                        transform.position += new Vector3(0, 0, 10);
                    }
                }
                else
                {
                    if (gameObject.GetComponent<SpriteRenderer>().enabled == true)
                    {
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    if (disappear)
                    {
                        state = 0;
                    }
                }
                break;
        }
    }

    private void SaveBest()
    {
        XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("Assets/XML/BestScore.xml");
        xmlDoc.SelectSingleNode("Information/Best").InnerText = BestScore.ToString();
        xmlDoc.Save("Assets/XML/BestScore.xml");
        Debug.Log("Новый рекорд!");
    }

    public void ReadBest()
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
    public void RandomText()
    {
        Promocode = "";
        for(int i = 0; i < 8; i++)
        {
            Randomcharacter = Random.Range(0, 25);
            Promocode += Character[Randomcharacter];
        }
        Debug.Log("Рандом текст создан: " + Promocode);
        SavePromo();
        Promocode = "";
    }

    private void SavePromo()
    {
        XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("Assets/XML/Promocodes.xml");
        XmlNode elmNew;
        elmNew = xmlDoc.CreateElement("Promocode");
        elmNew.InnerText = Promocode;
        xmlDoc.DocumentElement.AppendChild(elmNew);
        xmlDoc.Save("Assets/XML/Promocodes.xml");
        Debug.Log("Промокод сохранен!");
    }

    public void AddBonusThree()
    {
        XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("Assets/XML/Inventory.xml");
        XmlNode elmNew;
        elmNew = xmlDoc.CreateElement("Bonus");
        elmNew.InnerText = "Скидка 3%";
        xmlDoc.DocumentElement.AppendChild(elmNew);
        Debug.Log("Добавлена скидка 3%");
        RandomText();
        xmlDoc.Save("Assets/XML/Inventory.xml");
    }
    public void AddBonusFive()
    {
        XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("Assets/XML/Inventory.xml");
        XmlNode elmNew;
        elmNew = xmlDoc.CreateElement("BonusFive");
        elmNew.InnerText = "Скидка 5%";
        xmlDoc.DocumentElement.AppendChild(elmNew);
        Debug.Log("Добавлена скидка 5%");
        RandomText();
        xmlDoc.Save("Assets/XML/Inventory.xml");
        
    }
    public void AddBonusSuper()
    {
        XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.Load("Assets/XML/Inventory.xml");
        XmlNode elmNew;
        elmNew = xmlDoc.CreateElement("SuperBonus");
        elmNew.InnerText = "Бесплатный билет";
        xmlDoc.DocumentElement.AppendChild(elmNew);
        Debug.Log("Добавлен бесплатный билет");
        RandomText();
        xmlDoc.Save("Assets/XML/Inventory.xml");
    }

    public static void AddFuel()
    {
        Fuel += 0.33f;
        if (Fuel > 1)
        {
            Fuel = 1;
        }
    }

    //Delete fuel on collision with obstacles
    public static void DelFuel()
    {
        Fuel -= 0.5f;
        if (Fuel < 0)
        {
            Fuel = 0;
        }
    }

    //state 0 - start animation
    private void AnimationOnStart()
    {
        if (transform.position.y < - 3.01)
        {
            if (Time.time > 0.25)
            {
                transform.position += new Vector3(0, Mathf.Abs(-3 - transform.position.y) / Mathf.Pow(smooth, 2));
            }      
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y));
            GetComponent<Collider2D>().enabled = true;
            Obstacle.FuelQuantity = true;
            disappear = false;
            state = 1;
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
                    Priority = 3;
                }
                else if(reader.Name == "BonusFive")
                {
                    Priority = 2;
                }
                else if(reader.Name == "Bonus")
                {
                    Priority = 1;
                }
                else
                {
                    Priority = 0;
                }
            }
        }
        reader.Close();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fuel")
        {
            AddFuel();
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            DelFuel();
        }
        else if(collision.gameObject.tag == "BonusThree")
        {
            if(Priority == 0)
            {
                AddBonusThree();
            }
            
        }
        else if(collision.gameObject.tag == "BonusFive")
        {
            if(Priority == 1 || Priority == 0)
            {
                AddBonusFive();
            }
        }
        else if(collision.gameObject.tag == "BonusSuper")
        {
            if(Priority == 1 || Priority == 0 || Priority == 2)
            {
                AddBonusSuper();
            }
        }
    }
}
