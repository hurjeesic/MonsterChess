using UnitType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class UIBar : MonoBehaviour
    {
        Data DataIndex = Data.Instance;

        string[,] S_M_Id = { { "0" }, { "0" } };//현재 선택된 유닛을 넣어야 해요
        
        public Image M_Img;
        public Text AttackText;
        public Text DefenceText;
        public Text HpText;

        int x = 0;
        int y = 0;

        int fullHp = 0;
        int hp = 0;
        int ap = 0;
        int dp = 0;

        GameObject NT = GameObject.Find("NameText");
        MonsterSlot monsterSlot = new MonsterSlot();

        /*
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
        }
        */

        public void Place_UiBar(Unit unit)
        {
            x = int.Parse(unit.ID.Substring(0, 1));
            y = int.Parse(unit.ID.Substring(2, 1));

            fullHp = unit.fullHp;
            hp = unit.hp;
            dp = unit.dp;
            ap = unit.ap;

            Unit_Img(unit.ID);
            Unit_Hp();
            Unit_Ap();
            Unit_Dp();
        }

        //소환시킬때 ui바에 정보 표시
        public void Summon_UiBar(string id)
        {
            Unit_Img(id);
            
            NT = GameObject.Find("HPtext");
            NT.GetComponent<Text>().text = DataIndex.StateOfMonster[x, y].Substring(7, 1);

            NT = GameObject.Find("APtext");
            NT.GetComponent<Text>().text = DataIndex.StateOfMonster[x, y].Substring(5, 1);

            NT = GameObject.Find("DPtext");
            NT.GetComponent<Text>().text = DataIndex.StateOfMonster[x, y].Substring(9, 1);
        }
        
        void Unit_Img(string id)
        {
            NT = GameObject.Find("UnitImg");
            x = int.Parse(id.Substring(0, 1));
            y = int.Parse(id.Substring(2, 1));
            //대충 유닛 이미지 받아오는 함수
            //받아온 이미지를 Ui바 이미지 칸에 넣어야 해요

            string path = "Image/UnitMY/" + DataIndex.StateOfMonster[x, y].Substring(0, 3);
            RawImage img = NT.GetComponent<RawImage>();
            if (img != null)
            {
                //img.texture = Resources.Load("Image/Pan/Button_Square") as Texture; 오류나면 주석 취소 해보죠 뭐
            }

            Object image = Resources.Load<Object>(path);
            if (image != null)
            {
                if (DataIndex.In[x, y] == false)
                {
                    NT.GetComponent<RawImage>().texture = Resources.Load(path) as Texture;
                }
                else
                {
                    NT.GetComponent<RawImage>().texture = Resources.Load("Image/UnitEnemy/" + DataIndex.StateOfMonster[x, y].Substring(0, 3)) as Texture;
                }
            }
        }
        
        void Unit_Hp()
        {
            //대충 유닛 체력 받아오는 함수
            //선택된 유닛 체력최대치에서 계산해요

            //Ui바 체력 란에 표시하기

            NT = GameObject.Find("HPtext");
            NT.GetComponent<Text>().text = hp + "/" + fullHp;
        }
        void Unit_Ap()
        {
            //대충 유닛 공격력 받아오는 함수
            //Ui바 공격력 란에 표시하기
            
            NT = GameObject.Find("APtext");
            NT.GetComponent<Text>().text = ap + "";
        }
        void Unit_Dp()
        {
            //대충 유닛 방어력 받아오는 함수
            //Ui바 방어력 란에 표시하기
            NT = GameObject.Find("DPtext");
            NT.GetComponent<Text>().text = dp + "";//아무튼방어력 훔쳐오는 프로그램
        }
    }

    
}
