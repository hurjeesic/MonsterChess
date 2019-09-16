using UnityEngine;
using UnityEngine.UI;
namespace MonsterChessClient
{
    public class Setting_Place : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject[] SommonButton = new GameObject[5];
                SommonButton[i] = GameObject.Find("" + i);
                string temp = "Image/ButtonUnit/" + Data.Instance.Unit[i];
                SommonButton[i].GetComponent<RawImage>().texture = Resources.Load(temp) as Texture;
                SommonButton[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                Data.Instance.SommonOn = false;
            }
            GameObject Hero = GameObject.Find("0,3");
            Hero.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.Unit[5]) as Texture;
            Hero.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            Data.Instance.pan[0, 3] = Data.Instance.Unit[5];
            switch (Data.Instance.Unit[5])
            {
                case "200":
                    Hero.AddComponent<Unit200>();
                    Hero.GetComponent<Unit200>().Order = Data.Instance.Order;
                    Hero.GetComponent<Unit200>().x = 3;
                    Hero.GetComponent<Unit200>().y = 0;
                    Hero.GetComponent<Unit200>().Status = 0;
                    break;
                case "201":
                    Hero.AddComponent<Unit201>();
                    Hero.GetComponent<Unit201>().Order = Data.Instance.Order;
                    Hero.GetComponent<Unit201>().x = 3;
                    Hero.GetComponent<Unit201>().y = 0;
                    Hero.GetComponent<Unit201>().Status = 0;
                    break;
                case "202":
                    Hero.AddComponent<Unit202>();
                    Hero.GetComponent<Unit202>().Order = Data.Instance.Order;
                    Hero.GetComponent<Unit202>().x = 3;
                    Hero.GetComponent<Unit202>().y = 0;
                    Hero.GetComponent<Unit202>().Status = 0;
                    break;
            }
        }
    }

}
