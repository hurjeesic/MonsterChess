using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class SettingPlace : MonoBehaviour
    {
        void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject[] sommonBtn = new GameObject[5];
                sommonBtn[i] = GameObject.Find(i.ToString());
                sommonBtn[i].GetComponent<RawImage>().texture = Resources.Load("Image/ButtonUnit/" + Data.Instance.units[i]) as Texture;
                sommonBtn[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                Data.Instance.bSommons = false;
            }

            GameObject heroBtn = GameObject.Find("0,3");
            heroBtn.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.units[5]) as Texture;
            heroBtn.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            Data.Instance.board[0, 3] = Data.Instance.units[5];
            Unit unit = null;
            switch (Data.Instance.units[5])
            {
                case "200": unit = heroBtn.AddComponent<Unit200>(); break;
                case "201": unit = heroBtn.AddComponent<Unit201>(); break;
                case "202": unit = heroBtn.AddComponent<Unit202>(); break;
            }

            if (unit != null)
            {
                unit.order = Data.Instance.order;
                unit.x = 3;
                unit.y = 0;
                unit.status = 0;
            }
        }
    }

}
