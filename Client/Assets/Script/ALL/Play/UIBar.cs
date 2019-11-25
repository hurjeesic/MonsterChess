using UnitType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class UIBar : MonoBehaviour
    {
        public Image M_Img;
        
        public Text AttackText;
        public Text DefenceText;
        public Text HpText;
        public Text NameText;

        MonsterSlot monsterSlot = new MonsterSlot();


        public void UiBarID(string id)
        {
            GameObject.Find("UnitImg").GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + id) as Texture;
            GameObject.Find("UnitImg").GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            GameObject.Find("HPimg").GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            GameObject.Find("APimg").GetComponent<RawImage>().color = new Color(255, 255, 255, 255);

            string temp = Data.Instance.FindStateOfMonster(id);


            HpText = GameObject.Find("HPtext").GetComponent<Text>();
            HpText.text = temp[7] + "/" + temp[7];

            AttackText = GameObject.Find("APtext").GetComponent<Text>();
            AttackText.text = "" + temp[8];

            NameText = GameObject.Find("NameText").GetComponent<Text>();
            NameText.text = temp.Substring(10);
        }
    
        //소환시킬때 ui바에 정보 표시
        public void UiBarUnit(Unit unit)
        {
            GameObject.Find("UnitImg").GetComponent<RawImage>().texture = Resources.Load("/UnitMy/"+unit.ID) as Texture;
            GameObject.Find("UnitImg").GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            GameObject.Find("HPimg").GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            GameObject.Find("APimg").GetComponent<RawImage>().color = new Color(255, 255, 255, 255);




            HpText = GameObject.Find("HPtext").GetComponent<Text>();
            HpText.text= unit.hp+"/"+unit.fullHp;

            AttackText = GameObject.Find("APtext").GetComponent<Text>();
            AttackText.text = ""+unit.ap;
        }
        
       
      
       
    }
}
