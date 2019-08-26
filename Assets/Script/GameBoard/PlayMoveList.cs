using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayMoveList : MonoBehaviour {
    static MonsterData DataIndex = MonsterData.Instance;
    // Use this for initialization
    int MoveX;
    int MoveY;
    int ListCount=0;
    int temp = 0;
    int num;
    string Front;
    string Back;
    List<string> AtackRange = new List<string>();
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (DataIndex.PlayON == true)
        {
            StartCoroutine("MoveUnit");
            if (temp + 1 == ListCount)
            {
                StopCoroutine("MoveUnit");
                temp = ListCount;
            }
            
        }
        

    }

    IEnumerator MoveUnit()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("플레이 : " + ListCount);
        string temp = DataIndex.MoveList[ListCount];
        switch (temp.Substring(9, 1))
        {
            case "0"://대기
                Debug.Log("대기");
                if (temp.Substring(2, 1) == "1")
                {
                    string AtackPosition = UnitSelect();
                    if (AtackPosition == null)
                    {

                    }
                    else
                    {
                        Debug.Log("결과값은" + AtackPosition);
                        Front = temp.Substring(0, 10);
                        Back = "" + AtackPosition.Substring(0, 1) + AtackPosition.Substring(1, 1) + 0;//공격목표추가
                        DataIndex.MoveList[ListCount] = Front + Back;

                        temp = DataIndex.MoveList[ListCount];
                        Debug.Log("결과값은" + temp);
                        MoveX = int.Parse(temp.Substring(10, 1));
                        MoveY = int.Parse(temp.Substring(11, 1));
                        Damege(temp);
                    }

                }
                
                break;
            case "1"://이동
                Debug.Log("이동");
                if (temp.Substring(2, 1) == "1")
                {
                    //이동하는 유닛이 원거리일경우
                    MoveX = int.Parse(temp.Substring(10, 1));
                    MoveY = int.Parse(temp.Substring(11, 1));
                    if (DataIndex.pan[MoveX, MoveY] != null)
                    {
                        //이동하려는 칸에 무언가 있을경우
                        //대기상태로 전환후 ListCount --
                        Front = temp.Substring(0, 8);
                        Back = "" + 0 + 0 + 0 + 0;//대기상태로 전환
                        DataIndex.MoveList[ListCount] = Front + Back;
                        ListCount--;

                    }
                    else
                    {
                        //null일 경우
                        int x = int.Parse(temp.Substring(0, 1));
                        int y = int.Parse(temp.Substring(1, 1));
                        GameObject MoveTarget = GameObject.Find("" + MoveX + "," + MoveY);
                        GameObject MoveOrigin = GameObject.Find("" + x + "," + y);
                        Vector2 MovePosition = MoveTarget.transform.position;
                        MoveTarget.transform.position = MoveOrigin.transform.position;
                        MoveOrigin.transform.position = MovePosition;
                        string MoveName = MoveTarget.name;
                        MoveTarget.name = MoveOrigin.name;
                        MoveOrigin.name = MoveName;
                        Debug.Log(DataIndex.pan[MoveX, MoveY]);
                        Debug.Log(DataIndex.pan[x, y]);
                        DataIndex.pan[MoveX, MoveY] = DataIndex.pan[x, y];
                        DataIndex.pan[x, y] = null;
                        ChangeMoveList(temp);


                    }

                }
                else
                {
                    MoveX = int.Parse(temp.Substring(10, 1));
                    MoveY = int.Parse(temp.Substring(11, 1));
                    Debug.Log("좌표값은" + MoveX + MoveY);
                    if (DataIndex.pan[MoveX, MoveY] != null)
                    {
                        num = FindMoveList(MoveX, MoveY);
                        Debug.Log("Num은"+num);
                        if (temp.Substring(8, 1) == DataIndex.MoveList[num].Substring(8, 1))
                        {
                            //움직이는 유닛의 아군일 경우
                            Front = temp.Substring(0, 8);
                            Back = "" + 0 + 0 + 0 + 0;//대기상태로 전환
                            DataIndex.MoveList[ListCount] = Front + Back;


                        }
                        else
                        {
                            //움직이는 유닛의 적군일 경우
                            //데미지 &넉백
                            switch (temp.Substring(12, 1))
                            {
                                case "0"://동
                                    if (MoveX + 1 > 6 || DataIndex.pan[MoveX + 1, MoveY] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX+1,MoveY,temp);
                                        
                                    }
                                    
                                    break;
                                case "1"://서
                                    if (MoveX - 1 < 0 || DataIndex.pan[MoveX - 1, MoveY] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX-1,MoveY, temp);
                                       
                                    }
                                    break;
                                case "2"://남
                                    if (MoveY - 1 < 0 || DataIndex.pan[MoveX, MoveY-1] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX,MoveY-1, temp);
                                       
                                    }
                                    break;
                                case "3"://북
                                    if (MoveY + 1 > 6 || DataIndex.pan[MoveX, MoveY+1] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX, MoveY + 1, temp);
                                       
                                    }
                                    break;
                                case "4"://북동
                                    if (MoveX + 1 > 6&& MoveY+1>6 || DataIndex.pan[MoveX + 1, MoveY+1] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX+1, MoveY + 1, temp);
                                       
                                    }
                                    break;
                                case "5"://남동
                                    if (MoveX + 1 > 6 && MoveY - 1 < 0 || DataIndex.pan[MoveX + 1, MoveY - 1] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX + 1, MoveY - 1, temp);
                                      
                                    }
                                    break;
                                case "6"://남서
                                    if (MoveX - 1 < 0 && MoveY - 1 < 0 || DataIndex.pan[MoveX - 1, MoveY - 1] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX - 1, MoveY - 1, temp);
                                       
                                    }
                                    break;
                                case "7"://북서
                                    if (MoveX - 1 < 0 && MoveY + 1 > 6 || DataIndex.pan[MoveX - 1, MoveY + 1] != null)
                                    {
                                        //넉백이 되지않을경우
                                        //동쪽으로 6을 넘을 경우 넉백되는 칸이 null이 아닐경우
                                        Damege(temp);
                                    }
                                    else
                                    {
                                        //넉백과 대미지
                                        Damege(temp);
                                        KnockBack(MoveX - 1, MoveY + 1, temp);
                                       
                                    }
                                    break;

                            }

                        }


                    }
                    else
                    {
                        //null일 경우
                        int x = int.Parse(temp.Substring(0, 1));
                        int y = int.Parse(temp.Substring(1, 1));
                        GameObject MoveTarget = GameObject.Find("" + MoveX + "," + MoveY);
                        GameObject MoveOrigin = GameObject.Find("" + x + "," + y);
                        Vector2 MovePosition = MoveTarget.transform.position;
                        MoveTarget.transform.position = MoveOrigin.transform.position;
                        MoveOrigin.transform.position = MovePosition;
                        string MoveName = MoveTarget.name;
                        MoveTarget.name = MoveOrigin.name;
                        MoveOrigin.name = MoveName;
                        DataIndex.pan[MoveX, MoveY] = DataIndex.pan[x, y];
                        DataIndex.pan[x, y] = null;
                        ChangeMoveList(temp);

                    }
                }
                break;
            case "2"://소환
                Debug.Log("소환");
                Front =temp.Substring(0, 8);
                Back = "" + 0 + 0 + 0;//대기상태로 전환
                DataIndex.MoveList[ListCount] = Front + Back;
                break;
        }
        ListCount++;
        if (ListCount == DataIndex.MoveList.Count())
        {
            ListCount = 0;
            DataIndex.PlayON = false;
            Debug.Log("플레이끝!");
        }

    }
    int FindMoveList(int x, int y)
    {
        //x,y값을 넣으면 알맞은 무브리스트의 배열첨자값을 반환
        for (int i = 0; i < DataIndex.MoveList.Count(); i++)
        {
            Debug.Log("" + x + y);
            if (DataIndex.MoveList[i].Substring(0, 2) == "" + x + y)
            {
                Debug.Log("반환 첨자는" + i);
                return i;
            }
        }
        return -1;

    }
    string FindMoveListID(string ID)
    {
        for (int i = 0; i < DataIndex.MoveList.Count(); i++)
        {
            if (DataIndex.MoveList[i].Substring(2, 3) == ID)
            {
                return DataIndex.MoveList[i];
            }
        }
        return "";
    }
    void KnockBack(int KnockBackX,int KnockBackY,string temp)
    {
        int x = int.Parse(temp.Substring(0, 1));
        int y = int.Parse(temp.Substring(1, 1));
        GameObject MoveTarget = GameObject.Find("" + MoveX + "," + MoveY);//해당유닛 이동칸
        GameObject MoveOrigin = GameObject.Find("" + x + "," + y);//해당유니 칸
        GameObject KnockBackArea = GameObject.Find("" + KnockBackX + "," + KnockBackY);//넉백칸
        Vector2 MovePosition = MoveTarget.transform.position;//해당유닛 이동포지션
        Vector2 OriginPosition = MoveOrigin.transform.position;//해당유닛 포지션
        MoveTarget.transform.position = KnockBackArea.transform.position;
        MoveOrigin.transform.position = MovePosition;
        KnockBackArea.transform.position = OriginPosition;
        string MoveName = MoveTarget.name;
        string OriginName = MoveOrigin.name;
        MoveTarget.name = KnockBackArea.name;
        MoveOrigin.name = MoveName;
        KnockBackArea.name = OriginName;
        Debug.Log(DataIndex.pan[MoveX, MoveY]);
        Debug.Log(DataIndex.pan[x, y]);
        string ID=DataIndex.pan[MoveX,MoveY];
        DataIndex.pan[MoveX, MoveY] = DataIndex.pan[x, y];
        DataIndex.pan[x, y] = null;
        DataIndex.pan[KnockBackX, KnockBackY] = ID;



    }
    void Damege(string temp)
    {
        Debug.Log("데미지");
        num = FindMoveList(MoveX, MoveY);
        Debug.Log(num);
        int HP = int.Parse(DataIndex.MoveList[num].Substring(7, 1));
        Debug.Log(HP);
        HP -= int.Parse(temp.Substring(6, 1));
        if (HP < 0)
        {
            //destroy
        }
        Debug.Log(HP);
        Front = DataIndex.MoveList[num].Substring(0, 6);
        Back = DataIndex.MoveList[num].Substring(8);
        DataIndex.MoveList[num] = Front + HP + Back;
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

    string UnitSelect()
    {
        Debug.Log("유닛샐랙트");
        Debug.Log(DataIndex.MoveList[ListCount]);
        string UnitTurn = FindMoveListID(DataIndex.MoveList[ListCount].Substring(2,3)).Substring(8,1);
        string data = FindStateOfMonster(DataIndex.MoveList[ListCount].Substring(2, 3));
        int AR = int.Parse(data.Substring(5, 1));
        int MinY = int.Parse(DataIndex.MoveList[ListCount].Substring(1, 1)) - AR;
        int MaxY = int.Parse(DataIndex.MoveList[ListCount].Substring(1, 1)) + AR;
        int MinX = int.Parse(DataIndex.MoveList[ListCount].Substring(0, 1)) - AR;
        int MaxX = int.Parse(DataIndex.MoveList[ListCount].Substring(0, 1)) + AR;

        for (int i = MinY; i < MaxY + 1; i++)
        {
            for (int j = MinX; j < MaxX + 1; j++)
            {
                if (i > 0 && i < 6 && j > 0 && j < 6)
                {
                    if (DataIndex.pan[j, i] != null)
                    {
                        Debug.Log("에러지점" + j + i);
                        int TempNum = FindMoveList(j, i);
                        Debug.Log("에러" + DataIndex.MoveList[TempNum].Substring(8, 1) + ", " + UnitTurn);
                        if (DataIndex.MoveList[TempNum].Substring(8, 1) != UnitTurn)
                        {
                            AtackRange.Add("" + j + i);
                        }
                    }

                }
            }
        }
        
        int ramdom = Random.Range(0, AtackRange.Count());
        if (ramdom == 0)
        {
            AtackRange.Clear();
            return null;
        }
        else
        {
            string Select = AtackRange[ramdom];
            AtackRange.Clear();
            return Select;

        }
       
    }
    void ChangeMoveList(string temp)
    {
        string coordinate = "" + MoveX + MoveY;
        Front = temp.Substring(2, 7);
        Back = "" + 0 + 0 + 0 + 0;//대기상태로 전환
        DataIndex.MoveList[ListCount] = coordinate + Front + Back;
    }
}
