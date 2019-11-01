using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitType;

namespace MonsterChessClient
{

    public class ViewHP : MonoBehaviour
    {
        int fullHP;
        int hp;
        public Color hpColor;
        // Use this for initialization

        private void Update()
        {
            int x = int.Parse(transform.parent.name.Substring(0,1));
            int y = int.Parse(transform.parent.name.Substring(2));

            Unit unit = GameObject.Find(x + "," + y).GetComponent<Unit>();
            if (unit != null)
            {
                float fullHP = unit.fullHp;
                float hp = unit.hp;
                hpColor = new Color(255 / 255, 0, 0, 255 / 255);
                gameObject.GetComponent<Image>().color = hpColor;
                gameObject.GetComponent<Image>().fillAmount = hp / fullHP;

            }
            else
            {
                hpColor = new Color(0, 0, 0, 0);
                gameObject.GetComponent<Image>().color = hpColor;

            }

        }

    }
    

}

