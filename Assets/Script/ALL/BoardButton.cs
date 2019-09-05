using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
namespace MonsterChessClient
{
    public class BoardButton : MonoBehaviour
    {
       
        // Use this for initialization
        public void OnBoard()
        {
            if (GameObject.Find("SceneManager").GetComponent<SceneManager>().Present == SceneManager.SceneList.Place)
            {
                //플레이스 에서의 소환
                if (Data.Instance.SommonOn == true)
                {
                    int x = int.Parse(gameObject.name.Substring(2));
                    int y = int.Parse(gameObject.name.Substring(0, 1));
                    if (y < 3)
                    {
                        if (x == 3 && y == 0)
                        {
                            Debug.Log("히어로 칸 입니다.");
                        }
                        else
                        {
                            CheckPan();
                            Texture UnitImage = Resources.Load("Image/UnitMy/" + Data.Instance.SommonID) as Texture;
                            gameObject.GetComponent<RawImage>().texture = UnitImage;
                            gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                            Data.Instance.pan[y, x] = Data.Instance.SommonID;
                            Data.Instance.SommonOn = false;
                        }
                    }

                }
            }
            else
            {
                //플레이 에서의 기능
                if (Data.Instance.SommonOn == true && Data.Instance.Time <= 30)
                {

                    for (int i = 0; i < Data.Instance.MonsterList.Count(); i++)
                    {

                        if (Data.Instance.MonsterList[i].Substring(0, 3) == Data.Instance.SommonID)
                        {

                            int TempMana = int.Parse(Data.Instance.MonsterList[i].Substring(3, 1));
                            if (Data.Instance.Mana - TempMana < 0) { break; }
                            else
                            {
                                Debug.Log("4");
                                Data.Instance.Mana -= TempMana;
                                GameObject.Find("Mana").GetComponent<Text>().text = "" + Data.Instance.Mana;
                                int x = int.Parse(gameObject.name.Substring(0, 1));
                                int y = int.Parse(gameObject.name.Substring(2));
                                if (y < 3)
                                {
                                    Debug.Log("5");
                                    if (Data.Instance.pan[y, x] == null)
                                    {
                                        Debug.Log("6");
                                        Texture UnitImage = Resources.Load("Image/Unit/" + Data.Instance.SommonID) as Texture;
                                        gameObject.GetComponent<RawImage>().texture = UnitImage;
                                        gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                        Data.Instance.pan[x, y] = Data.Instance.SommonID;
                                        Data.Instance.MoveList.Add("" + y + x + Data.Instance.MonsterList[i] + Data.Instance.Order + 2 + 0 + 0 + 0);//2번상태 소환한상태
                                        Data.Instance.SommonOn = false;
                                    }
                                }
                                else
                                {
                                    //아군진영이 아님
                                }
                            }
                        }
                    }
                    //소환
                }
                else if (Data.Instance.ChangeListOn == true && Data.Instance.Time <= 30)
                {
                    //무르리스트 변경
                    for (int i = 0; i < Data.Instance.Range.Count(); i++)
                    {
                        if (Data.Instance.Range[i].name == gameObject.name)
                        {
                            int y = int.Parse(Data.Instance.origin.name.Substring(0, 1));
                            int x = int.Parse(Data.Instance.origin.name.Substring(2));
                            AnimatiomOFF();
                            int ListNumber = FindMoveList(y, x);
                            string Front = Data.Instance.MoveList[ListNumber].Substring(0, 9);
                            int MoveY = int.Parse(gameObject.name.Substring(0, 1));
                            int MoveX = int.Parse(gameObject.name.Substring(2));
                            string Back = "" + 1 + MoveY + MoveX + Data.Instance.Direction[i];
                            Data.Instance.MoveList[ListNumber] = Front + Back;
                            Debug.Log("바뀜 :" + Data.Instance.MoveList[ListNumber]);

                            Data.Instance.Range.Clear();
                            Data.Instance.Direction.Clear();
                            Data.Instance.MoveON = false;
                            break;
                        }

                    }


                }
                else if (Data.Instance.MoveON == false && Data.Instance.Time <= 30)
                {
                    //이동범위 표시
                    int y = int.Parse(gameObject.name.Substring(0, 1));
                    int x = int.Parse(gameObject.name.Substring(2));
                    for (int i = 0; i < Data.Instance.MoveList.Count(); i++)
                    {
                        if (Data.Instance.MoveList[i].Substring(0, 2) == "" + y + x)
                        {
                            if (Data.Instance.MoveList[i].Substring(8, 1) == "" + Data.Instance.Order)
                            {
                                UnitState();
                                UnitRange();
                                AnimationON();
                                Data.Instance.MoveON = true;
                                Data.Instance.origin = gameObject;

                            }
                            else
                            {
                                Debug.Log("자신의 유닛이 아닙니다.");
                            }
                        }
                    }

                }

            }

        string FindStateOfMonster(string ID)
        {
            //ID를 통해 몬스턴 전체 데이터를 가져옴
            switch (ID.Substring(0, 1))
            {
                case "0":
                    for (int i = 0; i < 12; i++)
                    {
                        if (Data.Instance.StateOfMonster[0, i].Substring(0, 3) == ID)
                        {
                            return Data.Instance.StateOfMonster[0, i];
                        }
                    }
                    break;
                case "1":
                    for (int i = 0; i < 6; i++)
                    {
                        if (Data.Instance.StateOfMonster[1, i].Substring(0, 3) == ID)
                        {
                            return Data.Instance.StateOfMonster[1, i];
                        }
                    }
                    break;
                case "2":
                    for (int i = 0; i < 3; i++)
                    {
                        if (Data.Instance.StateOfMonster[2, i].Substring(0, 3) == ID)
                        {
                            return Data.Instance.StateOfMonster[2, i];
                        }
                    }
                    break;

            }
            return "";
        }

        void CheckPan()
        {
            //소환했던 거라면 취소하고 정한위치에 재소환
            for (int i = 0; i < 3; i++)
            {
                //y값 설정
                for (int j = 0; j < 7; j++)
                {
                    //x값 설정
                    if (Data.Instance.pan[i, j] == Data.Instance.SommonID)
                    {
                        GameObject temp = GameObject.Find("" + i + "," + j);
                        temp.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        Data.Instance.pan[i, j] = null;
                    }
                }
            }

        }

            void AnimationON()
            {
                Debug.Log("에니메이션");
                for (int i = 0; i < DataIndex.Range.Count(); i++)
                {

                    Animation anim = DataIndex.Range[i].GetComponent<Animation>();
                    anim.Play("LightUnit");
                }

            }

            void AnimatiomOFF()
            {
                for (int i = 0; i < DataIndex.Range.Count(); i++)
                {
                    Debug.Log(DataIndex.Range[i].name);
                    Animation anim = DataIndex.Range[i].GetComponent<Animation>();
                    anim.Stop("LightUnit");
                    int y = int.Parse(DataIndex.Range[i].name.Substring(0, 1));
                    int x = int.Parse(DataIndex.Range[i].name.Substring(2));
                    if (DataIndex.pan[y, x] == null)
                    {
                        DataIndex.Range[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                    }
                    else
                    {
                        DataIndex.Range[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                    }

                }


            }

        }
}
