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
            Hero.GetComponent<RawImage>().texture = Resources.Load("Image/Unit/" + Data.Instance.Unit[5]) as Texture;
            Hero.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            Data.Instance.pan[0, 3] = Data.Instance.Unit[5];
        }
    }

}
