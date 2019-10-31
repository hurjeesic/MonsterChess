using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class MonsterSlot : MonoBehaviour
    {
        int type, index;

        public Text ManaText;
        public Text HeroText;
        public Text UnitText;

        Data DataIndex = Data.Instance;

        void Awake()
        {
            type = 0;
            index = int.Parse(gameObject.transform.name);
        }

        void Update()
        {
            ManaText.text = DataIndex.costSum + "/15";
            UnitText.text = DataIndex.MonsterCount + "";
            if (DataIndex.bHero)
            {
                HeroText.text = "O";
            }
            else
            {
                HeroText.text = "X";
            }
            if (DataIndex.kind != type)
            {
                type = DataIndex.kind;
            }

            ChoiceImage();
        }

        public void OnClick()
        {
            if (DataIndex.StateOfMonster.Length > index)
            {
                DataIndex.unitId = DataIndex.StateOfMonster[type, index].Substring(0, 3);
                int tempCount = int.Parse(UnitText.text);
                if(DataIndex.unitId !="300") tempCount++;
                UnitText.text = ""+tempCount;
            }

            // 몬스터가 추가되어 있을 경우 제거

            if(type == 2)
            {
                Debug.Log("히어로 초이스");
                ChoiceHero();
            }
            else
            {
                Debug.Log("몬스터 초이스");
                ChoiceMonster();
            }
        }

        public void ChoiceImage()
        {
            string path = "Image/UnitMY/" + DataIndex.StateOfMonster[type, index].Substring(0, 3);
            RawImage img = gameObject.GetComponent<RawImage>();
            if (img != null)
            {
                img.texture = Resources.Load("Image/Pan/Button_Square") as Texture;
            }
            Object image = Resources.Load<Object>(path);
            if (image != null)
            {
                if (DataIndex.In[type, index] == false)
                {
                    gameObject.GetComponent<RawImage>().texture = Resources.Load(path) as Texture;
                }
                else
                {
                    gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/UnitEnemy/" + DataIndex.StateOfMonster[type, index].Substring(0, 3)) as Texture;
                }
            }
        }

        void ChoiceMonster()
        {
            //이미 추가된유닛 선택시 삭제
            if (DataIndex.In[type, index])
            {
                DataIndex.costSum -= int.Parse(DataIndex.StateOfMonster[type, index].Substring(6, 1));

                GameObject.Find("SelectSystem").GetComponent<SelectSystem>().RemoveMonster();
                DataIndex.In[type, index] = !DataIndex.In[type, index];
            }
            else
            {
                //몬스터가 추가되지 않았을 경우 추가
                if (DataIndex.costSum > DataIndex.MaxCost)
                {
                    //마나코스트의 합이 지정한 값을 넘길경우
                    Debug.Log("마나코스트의 합이 " + DataIndex.MaxCost + "을 넘겼습니다");
                }
                else
                {
                    //선택된 몬스터가 5마리 이하일 경우 몬스터 추가
                    if (DataIndex.MonsterCount <= 4)
                    {
                        GameObject.Find("SelectSystem").GetComponent<SelectSystem>().AddMonster();
                        DataIndex.costSum += int.Parse(DataIndex.StateOfMonster[type, index].Substring(6, 1));
                        DataIndex.In[type, index] = !DataIndex.In[type, index];
                    }
                }
            }
        }

        void ChoiceHero()
        {
            if (DataIndex.In[type, index])
            {
                GameObject.Find("SelectSystem").GetComponent<SelectSystem>().RemoveMonster();
                DataIndex.In[type, index] = !DataIndex.In[type, index];
            }
            else
            {
                //히어로 두개 고를라 하면 내치는 거 해야댐
                if (DataIndex.bHero)
                {
                    Debug.Log("이미 히어로를 선택했대요");
                }
                else
                {
                    GameObject.Find("SelectSystem").GetComponent<SelectSystem>().AddMonster();
                    DataIndex.In[type, index] = !DataIndex.In[type, index];

                }
            }
        }
    }
}