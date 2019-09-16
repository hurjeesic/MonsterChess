using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;
public class Unit005 : MonoBehaviour {

    public string ID = "005";
    public int Distence = 1;
    public int Direction = 0;
    public int cost = 2;
    public int FullHP = 3;
    public int AP = 2;

    public int x;
    public int y;
    public int Order;
    public int Status;
    public int MoveX;
    public int MoveY;
    public int HP;
    public int MoveDirection;
    public string EnemyID;
    public List<GameObject> Range = new List<GameObject>();
    public List<int> TempMoveDirection = new List<int>();
    int RangeNum;

    public void MoveRange()
    {
        switch (Direction)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    //0=동, 1=서, 2=남, 3=북 
                    for (int j = 1; j < (Distence + 1); j++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (x + j > 6) { break; }
                                Range.Add(GameObject.Find("" + y + "," + (x + j)));
                                TempMoveDirection.Add(0);
                                Debug.Log("동");
                                break;
                            case 1:
                                if (x - j < 0) { break; }
                                Range.Add(GameObject.Find("" + y + "," + (x - j)));
                                Debug.Log("서");
                                TempMoveDirection.Add(1);
                                break;
                            case 2:
                                if (y - j < 0) { break; }
                                Range.Add(GameObject.Find("" + (y - j) + "," + x));
                                Debug.Log("남");
                                TempMoveDirection.Add(2);
                                break;
                            case 3:
                                if (y + j > 6) { break; }
                                Range.Add(GameObject.Find("" + (y + j) + "," + x));
                                Debug.Log("북");
                                TempMoveDirection.Add(3);
                                break;
                        }
                    }
                }
                break;
            case 1:
                for (int i = 0; i < 8; i++)
                {
                    //0=동, 1=서, 2=남, 3=북 4= 북동 5=남동 6=남서 7=북서
                    for (int j = 1; j < (Distence + 1); j++)
                    {
                        switch (i)
                        {
                            case 0:
                                if (x + j > 6) { break; }
                                Range.Add(GameObject.Find("" + y + "," + (x + j)));
                                TempMoveDirection.Add(0);
                                Debug.Log("동");
                                break;
                            case 1:
                                if (x - j < 0) { break; }
                                Range.Add(GameObject.Find("" + y + "," + (x - j)));
                                Debug.Log("서");
                                TempMoveDirection.Add(1);
                                break;
                            case 2:
                                if (y - j < 0) { break; }
                                Range.Add(GameObject.Find("" + (y - j) + "," + x));
                                Debug.Log("남");
                                TempMoveDirection.Add(2);
                                break;
                            case 3:
                                if (y + j > 6) { break; }
                                Range.Add(GameObject.Find("" + (y + j) + "," + x));
                                Debug.Log("북");
                                TempMoveDirection.Add(3);
                                break;
                            case 4:
                                if (y + j > 6 || x + j > 6) { break; }
                                Range.Add(GameObject.Find("" + (y + j) + "," + (x + j)));
                                Debug.Log("북동");
                                TempMoveDirection.Add(4);
                                break;
                            case 5:
                                if (y - j < 0 || x + j > 6) { break; }
                                Range.Add(GameObject.Find("" + (y - j) + "," + (x + j)));
                                Debug.Log("남동");
                                TempMoveDirection.Add(5);
                                break;
                            case 6:
                                if (y - j < 0 || x - j < 0) { break; }
                                Range.Add(GameObject.Find("" + (y - j) + "," + (x - j)));
                                TempMoveDirection.Add(6);
                                break;
                            case 7:
                                if (y + j > 6 || x - j < 0) { break; }
                                Range.Add(GameObject.Find("" + (y + j) + "," + (x - j)));
                                TempMoveDirection.Add(7);
                                break;

                        }
                    }
                }
                break;
            case 2:
                break;
        }
        AnimationON();
    }
    void AnimationON()
    {
        Debug.Log("에니메이션");
        for (int i = 0; i < Range.Count(); i++)
        {

            Animation anim = Range[i].GetComponent<Animation>();
            anim.Play("LightUnit");
        }

    }
   
    public bool CheckRange()
    {
        for (int i = 0; i < Range.Count; i++)
        {
            if (Range[i].name == MoveY + "," + MoveX)
            {
                RangeNum = i;
              
                return true;
            }
        }
        return false;
    }
}
