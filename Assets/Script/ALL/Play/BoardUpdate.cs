using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class BoardUpdate : MonoBehaviour
    {
        Text timeText;

        void Start()
        {
            timeText = GameObject.Find("Time").GetComponent<Text>();
        }

        void Update()
        {
            if (Data.Instance.bPlaying == false)
            {
                if (Data.Instance.time >= 40) //30초 시작
                {
                    Data.Instance.mana++;
                    GameObject.Find("Mana").GetComponent<Text>().text = Data.Instance.mana.ToString();
                    //================================================
                    Data.Instance.bSommons = false;
                    Data.Instance.bMoving = false;
                    Data.Instance.time = 31;
                }
                else if (Data.Instance.time <= 0)
                {
                    Data.Instance.time = 0;
                    timeText.text = Mathf.Floor(Data.Instance.time).ToString();
                    Data.Instance.turnNum++;
                    // 30초 끝
                    // 시간 초 초기화(재우)
                    AddList();
                    SortList();
                    Data.Instance.time = 50;
                    Data.Instance.bPlaying = true;
                }
                else
                {
                    // 시간이 진행됨
                    Data.Instance.time -= Time.deltaTime;
                    timeText.text = Mathf.Floor(Data.Instance.time).ToString();
                    // 30초 중
                    // 소환(성준)
                    // 이동범위 표시(성준)
                }
            }
        }

        private void AddList()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Data.Instance.board[i, j] != null)
                    {
                        Data.Instance.playList.Add(GameObject.Find(i + "," + j));
                    }
                }
            }
        }

        private void SortList()
        {
            GameObject temp;
            int tempX, tempY;

            // 항목별 정리
            for (int i = 0; i < Data.Instance.playList.Count; i++)
            {
                temp = Data.Instance.playList[i];
                tempX = int.Parse(temp.name.Substring(2));
                tempY = int.Parse(temp.name.Substring(0, 1));
                if (i == 0)
                {
                    for (int j = i; j < Data.Instance.playList.Count; j++)
                    {
                        if (Data.Instance.playList[j].name == "0,3")
                        {
                            Data.Instance.playList[i] = Data.Instance.playList[j];
                            Data.Instance.playList[j] = temp;
                            break;
                        }
                    }
                }
                else
                {
                    for (int j = i + 1; j < Data.Instance.playList.Count; j++)
                    {
                        int x = int.Parse(Data.Instance.playList[j].name.Substring(2));
                        int y = int.Parse(Data.Instance.playList[j].name.Substring(0, 1));
                        if (int.Parse(Data.Instance.board[tempY, tempX].Substring(0, 1)) > int.Parse(Data.Instance.board[y, x].Substring(0, 1)))
                        {
                            Data.Instance.playList[i] = Data.Instance.playList[j];
                            Data.Instance.playList[j] = temp;
                            temp = Data.Instance.playList[i];
                            tempX = int.Parse(Data.Instance.playList[i].name.Substring(2));
                            tempY = int.Parse(Data.Instance.playList[i].name.Substring(0, 1));
                        }
                    }
                }
            }

            // 코스트별 정리
            string UnitNum;
            for (int i = 1; i < Data.Instance.playList.Count; i++)
            {
                temp = Data.Instance.playList[i];
                tempX = int.Parse(Data.Instance.playList[i].name.Substring(2));
                tempY = int.Parse(Data.Instance.playList[i].name.Substring(0, 1));
                UnitNum = Data.Instance.board[tempY, tempX];
                for (int j = i + 1; j < Data.Instance.playList.Count; j++)
                {
                    int x = int.Parse(Data.Instance.playList[j].name.Substring(2));
                    int y = int.Parse(Data.Instance.playList[j].name.Substring(0, 1));
                    if (UnitNum.Substring(0, 1) == Data.Instance.board[y, x].Substring(0, 1))
                    {
                        int cost = int.Parse(Data.Instance.FindStateOfMonster(UnitNum).Substring(6, 1));
                        int TempCost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.board[y, x]).Substring(6, 1));
                        if (cost < TempCost)
                        {
                            Data.Instance.playList[i] = Data.Instance.playList[j];
                            Data.Instance.playList[j] = temp;
                            temp = Data.Instance.playList[i];
                            tempX = int.Parse(Data.Instance.playList[i].name.Substring(2));
                            tempY = int.Parse(Data.Instance.playList[i].name.Substring(0, 1));
                        }
                    }
                }
            }

            for (int i = 0; i < Data.Instance.playList.Count; i++)
            {
                Debug.Log(i + "번" + Data.Instance.playList[i]);
            }
        }
    }
}
