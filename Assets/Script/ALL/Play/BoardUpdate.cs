using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class BoardUpdate : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
           
         

        }


        // Update is called once per frame
        void Update()
        {
            if (Data.Instance.PlayOn == false)
            {
                if (Data.Instance.Time >= 40)//30초 시작
                {
                  
                    Data.Instance.Mana++;
                    GameObject ManaText = GameObject.Find("Mana");
                    ManaText.GetComponent<Text>().text = Data.Instance.Mana + "";
                    //================================================
                    Data.Instance.SommonOn = false;
                    Data.Instance.MoveOn = false;
                    Data.Instance.Time = 31;

                }
                else if (Data.Instance.Time <= 0)
                {

                    Data.Instance.Time = 0;
                    GameObject TimeText = GameObject.Find("Time");
                    TimeText.GetComponent<Text>().text = Mathf.Floor(Data.Instance.Time) + "";
                    Data.Instance.Turn++;
                    //30초 끝
                    //시간초 초기화(재우)
                    AddList();
                    SortList();
                    Data.Instance.Time = 50;
                    Data.Instance.PlayOn = true;
                }
                else
                {
                    //시간이 진행됨
                    Data.Instance.Time -= Time.deltaTime;
                    GameObject TimeText = GameObject.Find("Time");
                    TimeText.GetComponent<Text>().text = Mathf.Floor(Data.Instance.Time) + "";
                    //30초 중
                    // 소환(성준)
                    //이동범위 표시(성준)
                }

            }


        }

        void AddList()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Data.Instance.pan[i, j] != null)
                    {

                        Data.Instance.PlayList.Add(GameObject.Find(i + "," + j));
                    }
                }
            }
        }
        void SortList()
        {
            GameObject temp;
            int tempx;
            int tempy;
            for (int i = 0; i < Data.Instance.PlayList.Count; i++)
            {
                temp = Data.Instance.PlayList[i];
                tempx = int.Parse(temp.name.Substring(2));
                tempy = int.Parse(temp.name.Substring(0, 1));
                if (i == 0)
                {
                    for (int j = i; j < Data.Instance.PlayList.Count; j++)
                    {
                        if (Data.Instance.PlayList[j].name == "0,3")
                        {
                            Data.Instance.PlayList[i] = Data.Instance.PlayList[j];
                            Data.Instance.PlayList[j] = temp;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = i + 1; j < Data.Instance.PlayList.Count; j++)
                    {
                        int x = int.Parse(Data.Instance.PlayList[j].name.Substring(2));
                        int y = int.Parse(Data.Instance.PlayList[j].name.Substring(0, 1));
                        if (int.Parse(Data.Instance.pan[tempy, tempx].Substring(0, 1)) > int.Parse(Data.Instance.pan[y, x].Substring(0, 1)))
                        {
                            Data.Instance.PlayList[i] = Data.Instance.PlayList[j];
                            Data.Instance.PlayList[j] = temp;
                            temp = Data.Instance.PlayList[i];
                            tempx = int.Parse(Data.Instance.PlayList[i].name.Substring(2));
                            tempy = int.Parse(Data.Instance.PlayList[i].name.Substring(0, 1));
                        }
                    }
                }
            }//항목별 정리
            string UnitNum;
            for (int i = 1; i < Data.Instance.PlayList.Count; i++)
            {
                temp = Data.Instance.PlayList[i];
                tempx = int.Parse(Data.Instance.PlayList[i].name.Substring(2));
                tempy = int.Parse(Data.Instance.PlayList[i].name.Substring(0, 1));
                UnitNum = Data.Instance.pan[tempy, tempx];
                for (int j = i + 1; j < Data.Instance.PlayList.Count; j++)
                {
                    int x = int.Parse(Data.Instance.PlayList[j].name.Substring(2));
                    int y = int.Parse(Data.Instance.PlayList[j].name.Substring(0, 1));
                    if (UnitNum.Substring(0, 1) == Data.Instance.pan[y, x].Substring(0, 1))
                    {
                        int cost = int.Parse(Data.Instance.FindStateOfMonster(UnitNum).Substring(6, 1));
                        int TempCost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.pan[y, x]).Substring(6, 1));
                        if (cost < TempCost)
                        {
                            Data.Instance.PlayList[i] = Data.Instance.PlayList[j];
                            Data.Instance.PlayList[j] = temp;
                            temp = Data.Instance.PlayList[i];
                            tempx = int.Parse(Data.Instance.PlayList[i].name.Substring(2));
                            tempy = int.Parse(Data.Instance.PlayList[i].name.Substring(0, 1));
                        }
                    }

                }

            }//코스트별 정리\
            for (int i = 0; i < Data.Instance.PlayList.Count; i++)
            {
                Debug.Log(i + "번" + Data.Instance.PlayList[i]);
            }
        }
    }
}
