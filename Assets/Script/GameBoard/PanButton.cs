using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;


public class PanButton : MonoBehaviour
{
    int distence = 0;
    int direction = 0;
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    public void ButtonDistinction()
    {

        if (DataIndex.SommonON == true && DataIndex.Time <= 30)
        {

            for (int i = 0; i < DataIndex.MonsterList.Count(); i++)
            {

                if (DataIndex.MonsterList[i].Substring(0, 3) == DataIndex.SommonID)
                {

                    int TempMana = int.Parse(DataIndex.MonsterList[i].Substring(3, 1));
                    if (DataIndex.Mana - TempMana < 0) { break; }
                    else
                    {
                        Debug.Log("4");
                        DataIndex.Mana -= TempMana;
                        GameObject.Find("Mana").GetComponent<Text>().text = "" + DataIndex.Mana;
                        int x = int.Parse(gameObject.name.Substring(0, 1));
                        int y = int.Parse(gameObject.name.Substring(2));
                        if (y < 3)
                        {
                            Debug.Log("5");
                            if (DataIndex.pan[x, y] == null)
                            {
                                Debug.Log("6");
                                Texture UnitImage = Resources.Load("Image/Unit/" + DataIndex.SommonID) as Texture;
                                gameObject.GetComponent<RawImage>().texture = UnitImage;
                                gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                DataIndex.pan[x, y] = DataIndex.SommonID;
                                DataIndex.MoveList.Add("" + x + y + DataIndex.MonsterList[i] + DataIndex.Order + 2 + 0 + 0 + 0);//2번상태 소환한상태
                                DataIndex.SommonON = false;
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
        else if (DataIndex.MoveON == true && DataIndex.Time <= 30)
        {
            //무르리스트 변경
            for (int i = 0; i < DataIndex.Range.Count(); i++)
            {
                if (DataIndex.Range[i].name == gameObject.name)
                {
                    int x = int.Parse(DataIndex.origin.name.Substring(0, 1));
                    int y = int.Parse(DataIndex.origin.name.Substring(2));
                    AnimatiomOFF();
                    int ListNumber = FindMoveList(x, y);
                    string Front = DataIndex.MoveList[ListNumber].Substring(0, 9);
                    int MoveX = int.Parse(gameObject.name.Substring(0, 1));
                    int MoveY = int.Parse(gameObject.name.Substring(2));
                    string Back = "" + 1 + MoveX + MoveY + DataIndex.Direction[i];
                    DataIndex.MoveList[ListNumber] = Front + Back;
                    Debug.Log("바뀜 :" + DataIndex.MoveList[ListNumber]);

                    DataIndex.Range.Clear();
                    DataIndex.Direction.Clear();
                    DataIndex.MoveON = false;
                    break;
                }

            }


        }
        else if (DataIndex.MoveON == false && DataIndex.Time <= 30)
        {
            //이동범위 표시
            int x = int.Parse(gameObject.name.Substring(0, 1));
            int y = int.Parse(gameObject.name.Substring(2));
            for (int i = 0; i < DataIndex.MoveList.Count(); i++)
            {
                if (DataIndex.MoveList[i].Substring(0, 2) == "" + x + y)
                {
                    if (DataIndex.MoveList[i].Substring(8, 1) == "" + DataIndex.Order)
                    {
                        UnitState();
                        UnitRange();
                        AnimationON();
                        DataIndex.MoveON = true;
                        DataIndex.origin = gameObject;

                    }
                    else
                    {
                        Debug.Log("자신의 유닛이 아닙니다.");
                    }
                }
            }

        }
    }

    void UnitState()
    {
        //현재 유닛의 정보를 가져옴
        int x = int.Parse(gameObject.name.Substring(0, 1));
        int y = int.Parse(gameObject.name.Substring(2));

        if (DataIndex.pan[x, y] != null)
        {
            string ID = DataIndex.pan[x, y];
            string state = FindStateOfMonster(ID);
            distence = Int32.Parse(state.Substring(3, 1));
            direction = Int32.Parse(state.Substring(4, 1));
        }
        else
        {
            Debug.Log("말이없습니다.");

        }
    }

    void UnitRange()
    {
        Debug.Log("유닛레인지");
        int x = int.Parse(gameObject.name.Substring(0, 1));
        int y = int.Parse(gameObject.name.Substring(2));
        if (direction == 0)
        {
            //4방향
            for (int i = 0; i < 4; i++)
            {
                //0=동, 1=서, 2=남, 3=북 
                for (int j = 1; j < (distence + 1); j++)
                {
                    switch (i)
                    {
                        case 0:
                            if (x + j > 6) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x + j) + "," + y));
                            DataIndex.Direction.Add("0");
                            Debug.Log("동");
                            break;
                        case 1:
                            if (x - j < 0) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x - j) + "," + y));
                            Debug.Log("서");
                            DataIndex.Direction.Add("1");
                            break;
                        case 2:
                            if (y - j < 0) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x) + "," + (y - j)));
                            Debug.Log("남");
                            DataIndex.Direction.Add("2");
                            break;
                        case 3:
                            if (y + j > 6) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x) + "," + (y + j)));
                            Debug.Log("북");
                            DataIndex.Direction.Add("3");
                            break;
                    }
                }
            }
        }
        else if (direction == 1)
        {

            for (int i = 0; i < 8; i++)
            {
                //0=동, 1=서, 2=남, 3=북 4= 북동 5=남동 6=남서 7=북서
                for (int j = 1; j < (distence + 1); j++)
                {
                    switch (i)
                    {
                        case 0:
                            if (x + j > 6) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x + j) + "," + y));
                            Debug.Log("동");
                            DataIndex.Direction.Add("0");
                            break;
                        case 1:
                            if (x - j < 0) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x - j) + "," + y));
                            Debug.Log("서");
                            DataIndex.Direction.Add("1");
                            break;
                        case 2:
                            if (y - j < 0) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x) + "," + (y - j)));
                            Debug.Log("남");
                            DataIndex.Direction.Add("2");
                            break;
                        case 3:
                            if (y + j > 6) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x) + "," + (y + j)));
                            Debug.Log("북");
                            DataIndex.Direction.Add("3");
                            break;
                        case 4:
                            if (y + j > 6 || x + j > 6) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x + j) + "," + (y + j)));
                            Debug.Log("북동");
                            DataIndex.Direction.Add("4");
                            break;
                        case 5:
                            if (y - j < 0 || x + j > 6) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x + j) + "," + (y - j)));
                            Debug.Log("남동");
                            DataIndex.Direction.Add("5");
                            break;
                        case 6:
                            if (y - j < 0 || x - j < 0) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x - j) + "," + (y - j)));
                            DataIndex.Direction.Add("6");
                            break;
                        case 7:
                            if (y + j > 6 || x - j < 0) { break; }
                            DataIndex.Range.Add(GameObject.Find("" + (x - j) + "," + (y + j)));
                            DataIndex.Direction.Add("7");
                            break;

                    }
                }
            }


        }
        else if (direction == 2) { }
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
            int x = int.Parse(DataIndex.Range[i].name.Substring(0, 1));
            int y = int.Parse(DataIndex.Range[i].name.Substring(2));
            if (DataIndex.pan[x, y] == null)
            {
                DataIndex.Range[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
            }
            else
            {
                DataIndex.Range[i].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
            }

        }


    }

    int FindMoveList(int x, int y)
    {
        //x,y값을 넣으면 알맞은 무브리스트의 배열첨자값을 반환
        for (int i = 0; i < DataIndex.MoveList.Count(); i++)
        {
            if (DataIndex.MoveList[i].Substring(0, 2) == "" + x + y)
            {
                return i;
            }
        }
        return -1;

    }

    string FindStateOfMonster(string ID)
    {
        //ID를 통해 몬스턴 전체 데이터를 가져옴
        switch (ID.Substring(0, 1))
        {
            case "0":
                for (int i = 0; i < 12; i++)
                {
                    if (DataIndex.StateOfMonster[0, i].Substring(0, 3) == ID)
                    {
                        return DataIndex.StateOfMonster[0, i];
                    }
                }
                break;
            case "1":
                for (int i = 0; i < 6; i++)
                {
                    if (DataIndex.StateOfMonster[1, i].Substring(0, 3) == ID)
                    {
                        return DataIndex.StateOfMonster[1, i];
                    }
                }
                break;
            case "2":
                for (int i = 0; i < 3; i++)
                {
                    if (DataIndex.StateOfMonster[2, i].Substring(0, 3) == ID)
                    {
                        return DataIndex.StateOfMonster[2, i];
                    }
                }
                break;

        }
        return "";
    }

    string FindMonsterList(string ID)
    {
        //ID를 입력받으면 몬스터 리스트에서 찾아줌
        for (int i = 0; i < 6; i++)
        {
            if (DataIndex.MonsterList[i].Substring(0, 3) == ID)
            {
                return DataIndex.MoveList[i];
            }
        }
        return "";
    }

}
