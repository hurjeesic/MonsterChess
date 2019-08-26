using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PanUpdate : MonoBehaviour {
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    void Start () {
        Debug.Log("시간은" + DataIndex.Time);
    }



    // Update is called once per frame
    void Update () {
        if (DataIndex.PlayON == false)
        {
            if (DataIndex.Time >= 40)//30초 시작
            {
                SortList();//무브리스트 정렬
                DataIndex.Mana++;
                GameObject ManaText = GameObject.Find("Mana");
                ManaText.GetComponent<Text>().text = DataIndex.Mana + "";

                //================================================
                DataIndex.MoveON = false;
                DataIndex.SommonON = false;
                DataIndex.Time = 31;
                //마나 스톤 생성및 관리
                if (DataIndex.ManaStoneOn == false)
                {
                    ManaSton();//마나스톤 생성
                }
                else
                {
                    //생성이되면
                    int x = int.Parse(DataIndex.ManaStoneArea.name.Substring(0, 1));
                    int y = int.Parse(DataIndex.ManaStoneArea.name.Substring(2));
                    for (int i = 0; i < DataIndex.MoveList.Count(); i++)
                    {
                        if ("" + x + y == DataIndex.MoveList[i].Substring(0, 2) && DataIndex.MoveList[i].Substring(8, 1) == "" + DataIndex.Order)
                        {
                            DataIndex.ManaStoneCount--;
                            if (DataIndex.ManaStoneCount == 0)
                            {
                                DataIndex.ManaStone++;
                                GameObject temp = GameObject.Find("ManaArea");
                                temp.transform.position = new Vector2(4000, 100);
                            }
                        }
                        else
                        {
                            DataIndex.ManaStoneCount = 0;
                        }

                    }

                }


            }
            else if (DataIndex.Time <= 1)
            {

                DataIndex.Time = 0;
                GameObject TimeText = GameObject.Find("Time");
                TimeText.GetComponent<Text>().text = Mathf.Floor(DataIndex.Time) + "";
                DataIndex.turn++;
                //30초 끝
                //시간초 초기화(재우)
                DataIndex.Time = 50;
                DataIndex.MoveON = true;
                DataIndex.PlayON = true;
                for (int i = 0; i < 10; i++) { }


            }
            else
            {
                //시간이 진행됨
                DataIndex.Time -= Time.deltaTime;
                GameObject TimeText = GameObject.Find("Time");
                TimeText.GetComponent<Text>().text = Mathf.Floor(DataIndex.Time) + "";
                //30초 중
                // 소환(성준)
                //이동범위 표시(성준)
            }

        }
    }

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
    void ManaSton()
    {
        Debug.Log("마나스톤함수");
        if (DataIndex.turn == 0)//여기 수정
        {
            DataIndex.ManaStoneOn = true; ;
            int ramdom = Random.Range(0, 6);
            DataIndex.ManaStoneArea = GameObject.Find(ramdom + ",3");
            GameObject temp = GameObject.Find("ManaArea");
            temp.transform.position = DataIndex.ManaStoneArea.transform.position;


        }
    }
}
