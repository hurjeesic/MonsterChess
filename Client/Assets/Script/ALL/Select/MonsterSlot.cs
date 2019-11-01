using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class MonsterSlot : MonoBehaviour
    {
        int index;

        public Text ManaText;
        public Text HeroText;
        public Text UnitText;

        Data DataIndex = Data.Instance;

        void Awake()
        {
            index = int.Parse(gameObject.transform.name);
        }

        void Update()
        {
<<<<<<< HEAD
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
=======
            ChoiceImage(DataIndex.kind);
>>>>>>> develop
        }

        public void OnClick()
        {
            if (DataIndex.StateOfMonster.Length > index)
            {
                DataIndex.unitId = DataIndex.StateOfMonster[DataIndex.kind, index].Substring(0, 3);
                int tempCount = int.Parse(UnitText.text);
                if(DataIndex.unitId !="300") tempCount++;
                UnitText.text = ""+tempCount;
            }

            // 몬스터가 추가되어 있을 경우 제거
<<<<<<< HEAD

            if(type == 2)
            {
                ChoiceHero();
            }
            else
            {
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
=======
            if (DataIndex.In[DataIndex.kind, index])
>>>>>>> develop
            {
                DataIndex.costSum -= int.Parse(DataIndex.StateOfMonster[DataIndex.kind, index].Substring(6, 1));

                GameObject.Find("SelectSystem").GetComponent<SelectSystem>().RemoveMonster();
                DataIndex.In[DataIndex.kind, index] = !DataIndex.In[DataIndex.kind, index];
            }
            else
            {
                //몬스터가 추가되지 않았을 경우 추가
<<<<<<< HEAD
=======
                DataIndex.costSum += int.Parse(DataIndex.StateOfMonster[DataIndex.kind, index].Substring(6, 1));
>>>>>>> develop
                if (DataIndex.costSum > DataIndex.MaxCost)
                {
                    //마나코스트의 합이 지정한 값을 넘길경우
                    Debug.Log("마나코스트의 합이 " + DataIndex.MaxCost + "을 넘겼습니다");
<<<<<<< HEAD
=======
                    DataIndex.costSum -= int.Parse(DataIndex.StateOfMonster[DataIndex.kind, index].Substring(6, 1));
>>>>>>> develop
                }
                else
                {
                    //선택된 몬스터가 5마리 이하일 경우 몬스터 추가
                    if (DataIndex.MonsterCount <= 4)
                    {
                        GameObject.Find("SelectSystem").GetComponent<SelectSystem>().AddMonster();
<<<<<<< HEAD
                        DataIndex.costSum += int.Parse(DataIndex.StateOfMonster[type, index].Substring(6, 1));
                        DataIndex.In[type, index] = !DataIndex.In[type, index];
                    }
                }
            }
=======
                        if (DataIndex.In[DataIndex.kind, index])
                        {
                            DataIndex.costSum -= int.Parse(DataIndex.StateOfMonster[DataIndex.kind, index].Substring(6, 1));
                        }

                        DataIndex.In[DataIndex.kind, index] = !DataIndex.In[DataIndex.kind, index];
                    }
                }
            }

            ManaText.text = DataIndex.costSum + "/" + DataIndex.MaxCost;
            UnitText.text = DataIndex.MonsterCount.ToString();
            HeroText.text = DataIndex.bHero ? "O" : "X";
>>>>>>> develop
        }

        void ChoiceHero()
        {
<<<<<<< HEAD
<<<<<<< HEAD
            // 몬스터가 추가되어 있을 경우 제거
            if (DataIndex.In[type, index])
=======
            string id = DataIndex.StateOfMonster[type, index].Substring(0, 3) == "300" ? "" : DataIndex.StateOfMonster[type, index].Substring(0, 3);
=======
            string id = DataIndex.StateOfMonster[DataIndex.kind, index].Substring(0, 3);
>>>>>>> develop
            string path = "Image/UnitMY/" + id;
            RawImage img = gameObject.GetComponent<RawImage>();
            if (img != null)
>>>>>>> develop
            {
                DataIndex.costSum -= int.Parse(DataIndex.StateOfMonster[type, index].Substring(6, 1));

                GameObject.Find("SelectSystem").GetComponent<SelectSystem>().RemoveMonster();
                DataIndex.In[type, index] = !DataIndex.In[type, index];
            }
            else
            {
<<<<<<< HEAD
                //몬스터가 추가되지 않았을 경우 추가
                DataIndex.costSum += int.Parse(DataIndex.StateOfMonster[type, index].Substring(6, 1));
                //히어로 두개 고를라 하면 내치는 거 해야댐
                if (DataIndex.bHero && (DataIndex.kind == 2))
=======
                if (DataIndex.In[DataIndex.kind, index] == false)
>>>>>>> develop
                {
                    Debug.Log("이미 히어로를 선택했대요");
                }
                else
                {
<<<<<<< HEAD
                    GameObject.Find("SelectSystem").GetComponent<SelectSystem>().AddMonster();
                    DataIndex.In[type, index] = !DataIndex.In[type, index];
=======
                    gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/UnitEnemy/" + DataIndex.StateOfMonster[DataIndex.kind, index].Substring(0, 3)) as Texture;
>>>>>>> develop
                }
            }
        }
    }
}