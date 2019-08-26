using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using UnityEngine.UI;
using System.Linq;

public class PanSetting : MonoBehaviour
{



    // Use this for initialization
    static MonsterData DataIndex = MonsterData.Instance;

    void Start()
    {

        //네트워크로 판에 데이터 넣기필요!!
        int ListCount = 0;
        Debug.Log("스타트");
        for (int i = 0; i < 7; i++)//판에 존재하는 값을 찾음
        {
            for (int j = 0; j < 7; j++)
            {

                if (DataIndex.pan[j, i] != null)
                {
                    Debug.Log(DataIndex.pan[j, i]);
                    if (DataIndex.Order == 0)
                    {
                        if (i > 3)
                        {
                            //적
                            DataIndex.MoveList.Add("" + j + i + DataIndex.MonsterList[ListCount] + 1 + 0 + 0 + 0 + 0);
                            //x(1),y(1),ID(3),코스트(1),AP(1),HP(1),선공or후공(1), 상태(1), movex(1),movey(1),이동방향(1)

                        }
                        else
                        {
                            //아군
                            DataIndex.MoveList.Add("" + j + i + DataIndex.MonsterList[ListCount] + 0 + 0 + 0 + 0 + 0);

                        }

                    }
                    else
                    {
                        if (i > 3)
                        {
                            //적
                            DataIndex.MoveList.Add("" + j + i + DataIndex.MonsterList[ListCount] + 0 + 0 + 0 + 0 + 0);
                            //x(1),y(1),ID(3),코스트(1),AP(1),HP(1),선공or후공(1), 상태(1), movex(1),movey(1)

                        }
                        else
                        {
                            //아군
                            DataIndex.MoveList.Add("" + j + i + DataIndex.MonsterList[ListCount] + 1 + 0 + 0 + 0 + 0);

                        }
                    }
                    Debug.Log(ListCount);
                    switch (DataIndex.MonsterList[ListCount].Substring(0, 1))
                    {
                        case "0":
                            DataIndex.CountUnit[0]++;
                            break;
                        case "1":
                            DataIndex.CountUnit[1]++;
                            break;
                        case "2":
                            DataIndex.CountUnit[2]++;
                            break;
                    }
                    ListCount++;
                    for (int k = 0; k < 6; k++)
                    {
                        if (DataIndex.pan[j, i] == DataIndex.Unit[k])
                        {
                            GameObject temp = GameObject.Find("" + j + "," + i);
                            temp.GetComponent<RawImage>().texture = Resources.Load("Image/Unit/" + DataIndex.Unit[k]) as Texture;
                            temp.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                            Debug.Log(temp.name);

                        }
                    }

                }

            }

        }
        SortList();
        for (int i = 0; i < 5; i++)
        {
            //버튼추가
            GameObject[] SommonButton = new GameObject[5];
            SommonButton[i] = GameObject.Find("" + i);
            string temp = "Image/ButtonUnit/" + DataIndex.Unit[i];
            Debug.Log(temp);
            SommonButton[i].GetComponent<RawImage>().texture = Resources.Load(temp) as Texture;
            SommonButton[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            DataIndex.SommonON = false;
        }



        //히어로 유닛추가와 적배치를 시키고 다시 챙겨보기

    }

    // Update is called once per frame


    void SortList()
    {
        for (int i = 0; i < DataIndex.MoveList.Count(); i++)
        {

            for (int j = i; j < DataIndex.MoveList.Count(); j++)
            {
                if (i == 0)
                {
                    if (DataIndex.MoveList[j].Substring(2, 1) == "2")
                    {
                        if (DataIndex.MoveList[j].Substring(8, 1) == "0")
                        {
                            string temp = DataIndex.MoveList[j];
                            DataIndex.MoveList[j] = DataIndex.MoveList[i];
                            DataIndex.MoveList[i] = temp;
                            break;
                        }
                    }
                }
                else if (i == 1)
                {
                    if (DataIndex.MoveList[j].Substring(2, 1) == "2")
                    {
                        if (DataIndex.MoveList[j].Substring(8, 1) == "1")
                        {
                            string temp = DataIndex.MoveList[j];
                            DataIndex.MoveList[j] = DataIndex.MoveList[i];
                            DataIndex.MoveList[i] = temp;
                            break;
                        }
                    }
                }
                else if (i < DataIndex.CountUnit[0] + 2)
                {
                    if (i % 2 == 0)
                    {
                        if (DataIndex.MoveList[j].Substring(2, 1) == "0")
                        {
                            if (DataIndex.MoveList[j].Substring(8, 1) == "0")
                            {
                                string temp = DataIndex.MoveList[j];
                                DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                DataIndex.MoveList[i] = temp;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (DataIndex.MoveList[j].Substring(2, 1) == "0")
                        {
                            if (DataIndex.MoveList[j].Substring(8, 1) == "1")
                            {
                                string temp = DataIndex.MoveList[j];
                                DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                DataIndex.MoveList[i] = temp;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        if (DataIndex.MoveList[j].Substring(2, 1) == "1")
                        {
                            if (DataIndex.MoveList[j].Substring(8, 1) == "0")
                            {
                                string temp = DataIndex.MoveList[j];
                                DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                DataIndex.MoveList[i] = temp;
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (DataIndex.MoveList[j].Substring(2, 1) == "1")
                        {
                            if (DataIndex.MoveList[j].Substring(8, 1) == "1")
                            {
                                string temp = DataIndex.MoveList[j];
                                DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                DataIndex.MoveList[i] = temp;
                                break;
                            }
                        }
                    }
                }


            }
            //항목별 정리

        }

        for (int i = 2; i < DataIndex.MoveList.Count(); i++)
        {
            for (int j = i + 2; j < DataIndex.MoveList.Count(); j++)
            {
                //항목안에서의 정리
                if (i < DataIndex.CountUnit[0] + 2)
                {
                    if (i % 2 == 0)
                    {

                        if (DataIndex.MoveList[j].Substring(2, 1) == "0")
                        {
                            if (DataIndex.MoveList[j].Substring(8, 1) == "0")
                            {
                                if (int.Parse(DataIndex.MoveList[i].Substring(5, 1)) < int.Parse(DataIndex.MoveList[j].Substring(5, 1)))
                                {
                                    string temp = DataIndex.MoveList[j];
                                    DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                    DataIndex.MoveList[i] = temp;
                                }

                            }
                        }

                    }
                    else
                    {
                        if (DataIndex.MoveList[j].Substring(2, 1) == "0")
                        {
                            if (DataIndex.MoveList[j].Substring(8, 1) == "1")
                            {
                                if (int.Parse(DataIndex.MoveList[i].Substring(5, 1)) < int.Parse(DataIndex.MoveList[j].Substring(5, 1)))
                                {
                                    string temp = DataIndex.MoveList[j];
                                    DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                    DataIndex.MoveList[i] = temp;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        if (DataIndex.MoveList[j].Substring(2, 1) == "1")
                        {
                            if (int.Parse(DataIndex.MoveList[i].Substring(5, 1)) < int.Parse(DataIndex.MoveList[j].Substring(5, 1)))
                            {
                                string temp = DataIndex.MoveList[j];
                                DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                DataIndex.MoveList[i] = temp;
                            }
                        }
                    }
                    else
                    {
                        if (DataIndex.MoveList[j].Substring(2, 1) == "1")
                        {
                            if (int.Parse(DataIndex.MoveList[i].Substring(5, 1)) < int.Parse(DataIndex.MoveList[j].Substring(5, 1)))
                            {
                                string temp = DataIndex.MoveList[j];
                                DataIndex.MoveList[j] = DataIndex.MoveList[i];
                                DataIndex.MoveList[i] = temp;
                            }
                        }
                    }
                }

            }
        }

        for (int i = 0; i < DataIndex.MoveList.Count(); i++)
        {
            Debug.Log("" + i + " : " + DataIndex.MoveList[i]);

        }
    }
}


