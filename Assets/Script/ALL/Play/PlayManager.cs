using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonsterChessClient;

public class PlayManager : MonoBehaviour
{
    int PlayListCount=0;
    int tempCount=0;
    void Start()
    {

    }
    void Update()
    {
        if (Data.Instance.PlayOn == true)
        {
            if (tempCount == PlayListCount)
            {
                StartCoroutine("Play");
                tempCount++;
            }
            if (tempCount == Data.Instance.PlayList.Count)
            {
                tempCount = 0;
                PlayListCount = 0;
                Data.Instance.PlayOn = false;
            }
        }


    }

    IEnumerator Play()
    {
        Debug.Log(PlayListCount);
        yield return new WaitForSeconds(2f);
        int x = int.Parse(Data.Instance.PlayList[PlayListCount].name.Substring(2));
        int y = int.Parse(Data.Instance.PlayList[PlayListCount].name.Substring(0, 1));
        int MoveX;
        int MoveY;
        string ID;
        int MoveDIrection;
        int Order;
        switch (Data.Instance.pan[y, x])
        {
            case "000":
                ID = "000";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().Status == 0)
                {
                    //대기
                    Debug.Log("대기");
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit000>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "001":
                ID = "001";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit001>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;

            case "002":
                ID = "002";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit002>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "003":
                ID = "003";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit003>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "004":
                ID = "004";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit004>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;

            case "005":
                ID = "005";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit005>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "006":
                ID = "006";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit006>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "007":
                ID = "007";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit007>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "008":
                ID = "008";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       //Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit008>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "009":
                ID = "009";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit009>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "010":
                ID = "010";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       // Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit010>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;

            case "011":
                ID = "011";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       // Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit011>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;

            case "100":
                ID = "100";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       // Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit100>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "101":
                ID = "101";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       // Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit101>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "102":
                ID = "102";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().Status == 1)
                {
                    //이동
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit102>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "103":
                ID = "103";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit103>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "104":
                ID = "104";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       // Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit104>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "105":
                ID = "105";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       // Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit105>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "200":
                ID = "200";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                       // Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit200>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "201":
                ID = "201";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit201>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;
            case "202":
                ID = "202";
                if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().Status == 0)
                {
                    //대기
                }
                else if (Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().Status == 1)
                {
                    //이동
                    Debug.Log("이동");
                    MoveX = Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().MoveX;
                    MoveY = Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().MoveY;
                    MoveDIrection = Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().MoveDirection;
                    Order = Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().Order;
                    CheckMove(ID, MoveDIrection, MoveX, MoveY, x, y, Order);
                    if (Data.Instance.pan[MoveY, MoveX] != null)
                    {
                        //Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().Attack(PlayListCount);
                    }
                    else
                    {
                        Data.Instance.PlayList[PlayListCount].GetComponent<Unit202>().Move();
                    }

                }
                else
                {
                    //소환 아무것도 안함
                }
                break;

        }
        PlayListCount++;
        Debug.Log("끝");
    }
    void CheckMove(string ID, int MoveDirection, int MoveX, int MoveY, int x, int y, int Order)
    {
        int TempX;
        switch (MoveDirection)
        {
            case 0://동

                for (int i = x + 1; i < MoveX + 1; i++)
                {
                    if (i < 7)
                    {
                        if (Data.Instance.pan[y, i] != null)
                        {
                            GameObject TargetObject = GameObject.Find(y + "," + i);
                            int TargetOrder;
                            switch (Data.Instance.pan[y, i])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit001>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit002>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit003>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "1200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                            }
                        }
                    }
                }
                return;
            case 1://서
                for (int i = x - 1; i > MoveX - 1; i--)
                {
                    if (i >= 0)
                    {
                        if (Data.Instance.pan[y, i] != null)
                        {
                            GameObject TargetObject = GameObject.Find(y + "," + i);
                            int TargetOrder;
                            switch (Data.Instance.pan[y, i])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit001>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit002>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit003>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveX = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveX = i;
                                        return;
                                    }
                            }
                        }
                    }
                }
                return;
            case 2://남
                for (int i = y - 1; i > MoveY - 1; i--)
                {
                    if (i >= 0)
                    {
                        if (Data.Instance.pan[i, x] != null)
                        {
                            GameObject TargetObject = GameObject.Find(i + "," + x);
                            int TargetOrder;
                            switch (Data.Instance.pan[i, x])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit001>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit002>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit003>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                            }
                        }
                    }
                }
                return;
            case 3://북
                for (int i = y + 1; i < MoveY + 1; i++)
                {
                    if (i < 7)
                    {
                        if (Data.Instance.pan[i, x] != null)
                        {
                            GameObject TargetObject = GameObject.Find(i + "," + x);
                            int TargetOrder;
                            switch (Data.Instance.pan[i, x])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit001>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit002>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit003>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        return;
                                    }
                            }
                        }
                    }
                }
                return;
            case 4://북동
                TempX = x + 1;
                for (int i = y + 1; i < MoveY + 1; i++)
                {
                    if (i < 7 && TempX < 7)
                    {
                        if (Data.Instance.pan[i, TempX] != null)
                        {
                            GameObject TargetObject = GameObject.Find(i + "," + TempX);
                            int TargetOrder;
                            switch (Data.Instance.pan[i, TempX])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit001>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit002>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit003>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                            }
                        }
                    }
                    TempX++;
                }
                return;
            case 5://남동
                TempX = x + 1;
                for (int i = y - 1; i > MoveY; i--)
                {
                    if (i >= 0 && TempX < 7)
                    {
                        if (Data.Instance.pan[i, TempX] != null)
                        {
                            GameObject TargetObject = GameObject.Find(i + "," + TempX);
                            int TargetOrder;
                            switch (Data.Instance.pan[i, TempX])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX - 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                            }
                        }
                    }
                    TempX++;

                }
                return;
            case 6://남서
                TempX = x - 1;
                for (int i = y - 1; i > MoveY; i--)
                {
                    if (i >= 0 && TempX >= 0)
                    {
                        if (Data.Instance.pan[i, TempX] != null)
                        {
                            GameObject TargetObject = GameObject.Find(i + "," + TempX);
                            int TargetOrder;
                            switch (Data.Instance.pan[i, TempX])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i + 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                            }
                        }
                    }
                    TempX--;

                }
                return;
            case 7://북서
                TempX = x - 1;
                for (int i = y + 1; i < MoveY; i++)
                {
                    if (i < 7 && TempX >= 0)
                    {
                        if (Data.Instance.pan[i, TempX] != null)
                        {
                            GameObject TargetObject = GameObject.Find(i + "," + TempX);
                            int TargetOrder;
                            switch (Data.Instance.pan[i, TempX])
                            {
                                case "000":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "001":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;

                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "002":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "003":
                                    TargetOrder = TargetObject.GetComponent<Unit000>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "004":
                                    TargetOrder = TargetObject.GetComponent<Unit004>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "005":
                                    TargetOrder = TargetObject.GetComponent<Unit005>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "006":
                                    TargetOrder = TargetObject.GetComponent<Unit006>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "007":
                                    TargetOrder = TargetObject.GetComponent<Unit007>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "008":
                                    TargetOrder = TargetObject.GetComponent<Unit008>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "009":
                                    TargetOrder = TargetObject.GetComponent<Unit009>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "010":
                                    TargetOrder = TargetObject.GetComponent<Unit010>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "011":
                                    TargetOrder = TargetObject.GetComponent<Unit011>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "100":
                                    TargetOrder = TargetObject.GetComponent<Unit100>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "101":
                                    TargetOrder = TargetObject.GetComponent<Unit101>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "102":
                                    TargetOrder = TargetObject.GetComponent<Unit102>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "103":
                                    TargetOrder = TargetObject.GetComponent<Unit103>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "104":
                                    TargetOrder = TargetObject.GetComponent<Unit104>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "105":
                                    TargetOrder = TargetObject.GetComponent<Unit105>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "200":
                                    TargetOrder = TargetObject.GetComponent<Unit200>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "201":
                                    TargetOrder = TargetObject.GetComponent<Unit201>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                                case "202":
                                    TargetOrder = TargetObject.GetComponent<Unit202>().Order;
                                    if (TargetOrder == Order)
                                    {
                                        //아군
                                        MoveY = i - 1;
                                        MoveX = TempX + 1;
                                        return;
                                    }
                                    else
                                    {
                                        MoveY = i;
                                        MoveX = TempX;
                                        return;
                                    }
                            }
                        }
                    }
                    TempX--;

                }
                return;

        }


    }
}