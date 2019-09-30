using UnitType;
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
                    Data.Instance.bSummons = false;
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
            // 항목별 정리
            Data.Instance.playList.Sort(delegate (GameObject first, GameObject second)
            {
                Unit firstUnit = first.GetComponent<Unit>(), secondUnit = second.GetComponent<Unit>();
                if (firstUnit.ID[0] == secondUnit.ID[0])
                {
                    if (firstUnit.Cost == secondUnit.Cost)
                    {
                        return 0;
                    }
                    else
                    {
                        return firstUnit.Cost > secondUnit.Cost ? 1 : -1;
                    }
                }
                else
                {
                    return firstUnit.ID[0] > secondUnit.ID[0] ? 1 : -1;
                }
            });

            for (int i = 0; i < Data.Instance.playList.Count; i++)
            {
                Debug.Log(i + "번" + Data.Instance.playList[i]);
            }
        }
    }
}
