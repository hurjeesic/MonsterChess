using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace MonsterChessClient
{
    public class MonsterSlot : MonoBehaviour
    {
        // Start is called before the first frame update

        bool[,] In = { { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
        ,{ false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
        ,{ false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }};

        


        int N = 0;
        int a = 0;
        string temp;

        static Data DataIndex = Data.Instance;


        void Start()
        {
            a = int.Parse(this.name);
            ChoiceImage(N);
            Debug.Log(In[N, a]);
        }

        // Update is called once per frame

        void Update()
        {
            if (DataIndex.Kind != N)
            {
                N = DataIndex.Kind;
            }
            ChoiceImage(N);
        }

        public void OnClick()
        {

            
            if (DataIndex.StateOfMonster.Length > a)
            {
                DataIndex.MonsterID = DataIndex.StateOfMonster[N, a].Substring(0, 3);
            }

            //몬스터가 추가되어 있을 경우 제거
            if (In[N, a] == true)
            {
                DataIndex.Cost -= int.Parse(DataIndex.StateOfMonster[N, a].Substring(6, 1));

                GameObject.Find("SelectSystem").GetComponent<SelectSystem>().RemoveMonster();
                In[N, a] = !In[N, a];
            }
            else
            {
                //몬스터가 추가되지 않았을 경우 추가
                DataIndex.Cost += int.Parse(DataIndex.StateOfMonster[N, a].Substring(6, 1));
                if (DataIndex.Cost > 10)
                {
                    //마나코스트의 합이 10을 넘길경우
                    Debug.Log("마나코스트의 합이 10을 넘겼습니다");
                    DataIndex.Cost -= int.Parse(DataIndex.StateOfMonster[N, a].Substring(6, 1));
                }
                else
                {
                    //히어로 두개 고를라 하면 내치는 거 해야댐
                    if ((DataIndex.Hero == true) && (DataIndex.Kind == 2))
                    {
                        Debug.Log("이미 히어로를 선택했대요");
                    }
                    else
                    {
                        GameObject.Find("SelectSystem").GetComponent<SelectSystem>().AddMonster();
                        In[N, a] = !In[N, a];
                    }

                }
            }
            Debug.Log("현재의 마나코스트는" + DataIndex.Cost + "입니다");
        }

        public void ChoiceImage(int x)
        {
            gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/Pan/Button_Square") as Texture;
            temp = "Image/UnitMY/" + DataIndex.StateOfMonster[N, a].Substring(0, 3);

            Object[] images = Resources.LoadAll<Object>("Image/");
            Object image = Resources.Load<Object>(temp);
            if (image != null)
            {
                if (In[N, a] == false)
                {
                    gameObject.GetComponent<RawImage>().texture = Resources.Load(temp) as Texture;
                }
                else
                {
                    gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/UnitEnemy/" + DataIndex.StateOfMonster[N, a].Substring(0, 3)) as Texture;
                }
            }
        }
    }
}