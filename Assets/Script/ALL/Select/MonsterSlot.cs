using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class MonsterSlot : MonoBehaviour
    {
        // Start is called before the first frame update

        bool[,] In = { { false, false, false, false }, { false, false, false, false }, { false, false, false, false } };

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
                ChoiceImage(N);
            }
            if (In[N, a] == false)
            {
                gameObject.GetComponent<RawImage>().color
                    = new Color(191, 191, 191, 255);
                //나중에 선택된 이미지 넣기
            }
            else
            {
                gameObject.GetComponent<RawImage>().color
                    = new Color(200, 200, 200, 255);
            }
        }

        public void OnClick()
        {
            DataIndex.MonsterID = DataIndex.StateOfMonster[N, a].Substring(0, 3);

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
                    GameObject.Find("SelectSystem").GetComponent<SelectSystem>().AddMonster();
                    In[N, a] = !In[N, a];
                }
            }
            Debug.Log("현재의 마나코스트는" + DataIndex.Cost + "입니다");
        }

        public void ChoiceImage(int x)
        {
            temp = "Image/Unit/" + DataIndex.StateOfMonster[N, a].Substring(0, 3);
            gameObject.GetComponent<RawImage>().texture = Resources.Load(temp) as Texture;
        }
    }
}