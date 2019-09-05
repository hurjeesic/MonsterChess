using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class BoardSetting : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < 3; i++)//판에 존재하는 값을 찾음
            {
                //y값
                for (int j = 0; j < 7; j++)
                {
                    //x값
                    if (Data.Instance.pan[i, j] != null)
                    {
                        AddToMoveList(i, j);
                        
                    }
                }
            }
            SortList();
            for (int i = 0; i < Data.Instance.MoveList.Count(); i++)
            {
                Debug.Log("정리한 무브리스트 : " + i);
                Debug.Log(Data.Instance.MoveList[i]);
            }
        }
        // Update is called once per frame
        void SortList()
        {
            for (int i = 0; i < Data.Instance.MoveList.Count; i++)
            {
                string TempList = Data.Instance.MoveList[i];
                switch (i)
                {
                    case 0:
                        for (int j = i+1; j < Data.Instance.MoveList.Count; j++)
                        {
                            if (int.Parse(Data.Instance.MoveList[j].Substring(2,1))==2)
                            {
                                Data.Instance.MoveList[i] = Data.Instance.MoveList[j];
                                Data.Instance.MoveList[j] = TempList;
                                break;
                            }
                        }
                        break;
                    default:
                        for (int j = i+1; j < Data.Instance.MoveList.Count; j++)
                        {
                            int Unit1 = int.Parse(TempList.Substring(2, 1));
                            int Unit2 = int.Parse(Data.Instance.MoveList[j].Substring(2, 1));
                            if (Unit1 == Unit2)
                            {
                                //같을때는 코스트를 비교
                                int cost1 = int.Parse(TempList.Substring(8, 1));
                                int cost2 = int.Parse(Data.Instance.MoveList[j].Substring(8, 1));
                                if (cost1 < cost2)
                                {
                                    Data.Instance.MoveList[i] = Data.Instance.MoveList[j];
                                    Data.Instance.MoveList[j] = TempList;
                                    TempList = Data.Instance.MoveList[i];
                                }
                            }
                            else if (Unit1 > Unit2)
                            {
                                Data.Instance.MoveList[i] = Data.Instance.MoveList[j];
                                Data.Instance.MoveList[j] = TempList;
                                TempList = Data.Instance.MoveList[i];
                            }
                            else { }
                        }
                        break;
                }
               
            }
        }
        void AddToMoveList(int y, int x)
        {
            //Y(1)+X(1)+ ID(3) , 이동가능거리(1),  이동가능방향(1),  공격거리(1),  코스트(1),  HP(1),  AP(1) + order + 상태+ Movey MoveX+ 이동방향
            // 0    1   2,3,4      5                    6               7           8           9       10       11     12     13     14
            string ID = Data.Instance.pan[y, x];
            string MonsterList = FindMonsterList(ID).Substring(0, 8);
            if (y == 0 && x == 3)
            {
                Data.Instance.MoveList.Add("" + y + x + MonsterList + Data.Instance.Order + 2 + 0 + 0 + 0);
            }
            else
            {
                Data.Instance.MoveList.Add("" + y + x + MonsterList + Data.Instance.Order + 0 + 0 + 0 + 0);
            }
            

        }
        string FindMonsterList(string ID)
        {
            for (int i = 0; i < Data.Instance.MonsterList.Count; i++)
            {
                if (ID == Data.Instance.MonsterList[i].Substring(0, 3))
                {
                    return Data.Instance.MonsterList[i];
                }
            }
            return "";
        }
    }

}

