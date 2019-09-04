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
            if (Data.Instance.Time >= 40)//30초 시작
            {
                //SortList();//무브리스트 정렬
                Data.Instance.Mana++;
                GameObject ManaText = GameObject.Find("Mana");
                ManaText.GetComponent<Text>().text = Data.Instance.Mana + "";
                //================================================
                Data.Instance.ChangeListOn = false;
                Data.Instance.SommonOn = false;
                Data.Instance.Time = 31;

            }
            else if (Data.Instance.Time <= 1)
            {

                Data.Instance.Time = 0;
                GameObject TimeText = GameObject.Find("Time");
                TimeText.GetComponent<Text>().text = Mathf.Floor(Data.Instance.Time) + "";
                Data.Instance.Turn++;
                //30초 끝
                //시간초 초기화(재우)
                Data.Instance.Time = 50;
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
        /* void SortList()
        {
            for (int i = 0; i < Data.Instance.MoveList.Count(); i++)
            {

                for (int j = i; j < Data.Instance.MoveList.Count(); j++)
                {
                    if (i == 0)
                    {
                        if (Data.Instance.MoveList[j].Substring(2, 1) == "2")
                        {
                            if (Data.Instance.MoveList[j].Substring(8, 1) == "0")
                            {
                                string temp = Data.Instance.MoveList[j];
                                Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                Data.Instance.MoveList[i] = temp;
                                break;
                            }
                        }
                    }
                    else if (i == 1)
                    {
                        if (Data.Instance.MoveList[j].Substring(2, 1) == "2")
                        {
                            if (Data.Instance.MoveList[j].Substring(8, 1) == "1")
                            {
                                string temp = Data.Instance.MoveList[j];
                                Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                Data.Instance.MoveList[i] = temp;
                                break;
                            }
                        }
                    }
                    else if (i < Data.Instance.CountUnit[0] + 2)
                    {
                        if (i % 2 == 0)
                        {
                            if (Data.Instance.MoveList[j].Substring(2, 1) == "0")
                            {
                                if (Data.Instance.MoveList[j].Substring(8, 1) == "0")
                                {
                                    string temp = Data.Instance.MoveList[j];
                                    Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                    Data.Instance.MoveList[i] = temp;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (Data.Instance.MoveList[j].Substring(2, 1) == "0")
                            {
                                if (Data.Instance.MoveList[j].Substring(8, 1) == "1")
                                {
                                    string temp = Data.Instance.MoveList[j];
                                    Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                    Data.Instance.MoveList[i] = temp;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            if (Data.Instance.MoveList[j].Substring(2, 1) == "1")
                            {
                                if (Data.Instance.MoveList[j].Substring(8, 1) == "0")
                                {
                                    string temp = Data.Instance.MoveList[j];
                                    Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                    Data.Instance.MoveList[i] = temp;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (Data.Instance.MoveList[j].Substring(2, 1) == "1")
                            {
                                if (Data.Instance.MoveList[j].Substring(8, 1) == "1")
                                {
                                    string temp = Data.Instance.MoveList[j];
                                    Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                    Data.Instance.MoveList[i] = temp;
                                    break;
                                }
                            }
                        }
                    }


                }
                //항목별 정리

            }

            for (int i = 2; i < Data.Instance.MoveList.Count(); i++)
            {
                for (int j = i + 2; j < Data.Instance.MoveList.Count(); j++)
                {
                    //항목안에서의 정리
                    if (i < Data.Instance.CountUnit[0] + 2)
                    {
                        if (i % 2 == 0)
                        {

                            if (Data.Instance.MoveList[j].Substring(2, 1) == "0")
                            {
                                if (Data.Instance.MoveList[j].Substring(8, 1) == "0")
                                {
                                    if (int.Parse(Data.Instance.MoveList[i].Substring(5, 1)) < int.Parse(Data.Instance.MoveList[j].Substring(5, 1)))
                                    {
                                        string temp = Data.Instance.MoveList[j];
                                        Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                        Data.Instance.MoveList[i] = temp;
                                    }

                                }
                            }

                        }
                        else
                        {
                            if (Data.Instance.MoveList[j].Substring(2, 1) == "0")
                            {
                                if (Data.Instance.MoveList[j].Substring(8, 1) == "1")
                                {
                                    if (int.Parse(Data.Instance.MoveList[i].Substring(5, 1)) < int.Parse(Data.Instance.MoveList[j].Substring(5, 1)))
                                    {
                                        string temp = Data.Instance.MoveList[j];
                                        Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                        Data.Instance.MoveList[i] = temp;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            if (Data.Instance.MoveList[j].Substring(2, 1) == "1")
                            {
                                if (int.Parse(Data.Instance.MoveList[i].Substring(5, 1)) < int.Parse(Data.Instance.MoveList[j].Substring(5, 1)))
                                {
                                    string temp = Data.Instance.MoveList[j];
                                    Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                    Data.Instance.MoveList[i] = temp;
                                }
                            }
                        }
                        else
                        {
                            if (Data.Instance.MoveList[j].Substring(2, 1) == "1")
                            {
                                if (int.Parse(Data.Instance.MoveList[i].Substring(5, 1)) < int.Parse(Data.Instance.MoveList[j].Substring(5, 1)))
                                {
                                    string temp = Data.Instance.MoveList[j];
                                    Data.Instance.MoveList[j] = Data.Instance.MoveList[i];
                                    Data.Instance.MoveList[i] = temp;
                                }
                            }
                        }
                    }

                }
            }

            for (int i = 0; i < Data.Instance.MoveList.Count(); i++)
            {
                Debug.Log("" + i + " : " + Data.Instance.MoveList[i]);

            }
        }
        
    }
    */
    }

}
