using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class Data
    {
        private static Data instance = null;
        public static Data Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Data();
                }
                return instance;
            }
        }

        private Data()
        {

        }

        //몬스터 리스트는 스크립트에 접근하지 않고 정보를 가져오기 위한 저장장
        public string[,] StateOfMonster = {{"000100121고블린","001110121벤시","002100121놀","003210222드레이크","004110232오크"
                                        ,"005100232골렘","006210342트윈헤드오우거","007200333광전사","008110332웜","009210443데스나이트"
                                        ,"010210434발키리","011210443용사" },
                                        {"100112221사냥꾼","101112222정령궁수","102112332마법사","103112322대포"
                                           ,"104112433히드라","105112442주작","000000000000","000000000000","000000000000","000000000000","000000000000","000000000000"},
                                         {"200320053제우스","201310053아누비스","202210054해태","000000000000","000000000000","000000000000","000000000000","000000000000","000000000000","000000000000","000000000000","000000000000"}};
        // ID(3) , 이동거리(1),  이동방향(1),  공격거리(1),  코스트(1),  HP(1),  AP(1),  이름
        //  0,1,2       3           4               5               6       7       8       9~

        public string[,] pan = new string[7, 7];//현재 판의 데이터 배열
        public string[] Unit = { "002", "005", "008", "101", "102", "201" };//유닛넣는곳(임시로 넣었음)
        
        public List<string> MoveDirection= new List<string>(); // 유닛의 이동방향을 나타냄
        public string SommonID;//소환할 ID를 가져옴


        public int Mana = 10;//소환에 필요한 마나
        public int Turn = 0;//턴수
        public int Order = 0;// 0=선공, 1=후공
        public float Time = 50f; // 턴의 시간

        public GameObject Origin;
        public List<GameObject> PlayList = new List<GameObject>();//실제 게임진행하는 리스트

        public bool SommonOn;//소환
        public bool ManaStoneOn;//마나스톤 생성
        public bool MoveOn=false;
        public bool PlayOn = false;
        public bool ChangeListOn;//준비시간에

        // 여기서 부터는 SelectUnit 관련 변수들

        public int Kind = 0;//현재 선택된 탭의 번호
        public string MonsterID;//선택된 몬스터의 이름
        public int Cost = 0;//선택된 몬스터의 코스트 합
        public bool Hero = false;//히어로가 선택 되었는지
        public int MaxCost = 15;

        //함수
        public void MoveRange(int x, int y, int Direction, int Distence, List<GameObject> Range, List<int>TempMoveDirection)
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
            Debug.Log("에니메이션");
            for (int i = 0; i < Range.Count(); i++)
            {

                Animation anim = Range[i].GetComponent<Animation>();
                anim.Play("LightUnit");
            }
        }
        public void SaveMove(List<GameObject> Range, int MoveDirection, int MoveX, int MoveY, int Status)
        {

            for (int i = 0; i < Range.Count; i++)
            {
                int RangeX = int.Parse(Range[i].name.Substring(2));
                int RangeY = int.Parse(Range[i].name.Substring(0, 1));
                int x = int.Parse(instance.Origin.name.Substring(2));
                int y = int.Parse(instance.Origin.name.Substring(0, 1));
                if (Range[i].name == MoveY + "," + MoveX)
                {
                    switch (instance.pan[y, x])
                    {
                        case "000":
                            Unit000 unit000 = Origin.GetComponent<Unit000>();
                            unit000.Status = 1;
                            break;
                        case "001":
                            Unit001 unit001 = Origin.GetComponent<Unit001>();
                            unit001.Status = 1;
                            break;
                        case "002":
                            Unit002 unit002 = Origin.GetComponent<Unit002>();
                            unit002.Status = 1;
                            break;
                        case "003":
                            Unit003 unit003 = Origin.GetComponent<Unit003>();
                            unit003.Status = 1;
                            break;
                        case "004":
                            Unit004 unit004 = Origin.GetComponent<Unit004>();
                            unit004.Status = 1;
                            break;
                        case "005":
                            Unit005 unit005 = Origin.GetComponent<Unit005>();
                            unit005.Status = 1;
                            break;
                        case "006":
                            Unit006 unit006 = Origin.GetComponent<Unit006>();
                            unit006.Status = 1;
                            break;
                        case "007":
                            Unit007 unit007 = Origin.GetComponent<Unit007>();
                            unit007.Status = 1;
                            break;
                        case "008":
                            Unit008 unit008 = Origin.GetComponent<Unit008>();
                            unit008.Status = 1;
                            break;
                        case "009":
                            Unit009 unit009 = Origin.GetComponent<Unit009>();
                            unit009.Status = 1;
                            break;
                        case "010":
                            Unit010 unit010 = Origin.GetComponent<Unit010>();
                            unit010.Status = 1;
                            break;
                        case "011":
                            Unit011 unit011 = Origin.GetComponent<Unit011>();
                            unit011.Status = 1;
                            break;
                        case "100":
                            Unit100 unit100 = Origin.GetComponent<Unit100>();
                            unit100.Status = 1;
                            break;
                        case "101":
                            Unit101 unit101 = Origin.GetComponent<Unit101>();
                            unit101.Status = 1;
                            break;
                        case "102":
                            Unit102 unit102 = Origin.GetComponent<Unit102>();
                            unit102.Status = 1;
                            break;
                        case "103":
                            Unit103 unit103 = Origin.GetComponent<Unit103>();
                            unit103.Status = 1;
                            break;
                        case "104":
                            Unit104 unit104 = Origin.GetComponent<Unit104>();
                            unit104.Status = 1;
                            break;
                        case "105":
                            Unit105 unit105 = Origin.GetComponent<Unit105>();
                            unit105.Status = 1;
                            break;
                        case "200":
                            Unit200 unit200 = Origin.GetComponent<Unit200>();
                            unit200.Status = 1;
                            break;
                        case "201":
                            Unit201 unit201 = Origin.GetComponent<Unit201>();
                            unit201.Status = 1;
                            break;
                        case "202":
                            Unit202 unit202 = Origin.GetComponent<Unit202>();
                            unit202.Status = 1;
                            break;
                    }
                    MoveX = RangeX;
                    MoveY = RangeY;
                    for (int j = 0; j < Range.Count; j++)
                    {
                        Animation anim = Range[j].GetComponent<Animation>();
                        anim.Stop("LightUnit");
                        RangeX = int.Parse(Range[j].name.Substring(2));
                        RangeY = int.Parse(Range[j].name.Substring(0, 1));
                        if (Data.Instance.pan[RangeY, RangeX] != null)
                        {
                            Range[j].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                        }
                        else
                        {
                            Range[j].GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        }
                        if (j == Range.Count - 1)
                        {
                            return;
                        }
                    }
                    if (i == Range.Count - 1)
                    {
                        Debug.Log("이동범위가 아닙니다.");
                        Debug.Log("유닛부터 다시 선택하여 주십시요");
                        for (int j = 0; j < Range.Count; j++)
                        {
                            Animation anim = Range[j].GetComponent<Animation>();
                            anim.Stop("LightUnit");
                            RangeX = int.Parse(Range[j].name.Substring(2));
                            RangeY = int.Parse(Range[j].name.Substring(0, 1));
                            if (Data.Instance.pan[RangeY, RangeX] != null)
                            {
                                Range[j].GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                            }
                            else
                            {
                                Range[j].GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                            }
                            if (j == Range.Count - 1)
                            {
                                return;
                            }
                        }
                    }

                }
            }
           
        }
        public string FindStateOfMonster(string ID)
        {
            //ID를 통해 몬스턴 전체 데이터를 가져옴
            switch (ID.Substring(0, 1))
            {
                case "0":
                    for (int i = 0; i < 12; i++)
                    {
                        if (StateOfMonster[0, i].Substring(0, 3) == ID)
                        {
                            return StateOfMonster[0, i];
                        }
                    }
                    break;
                case "1":
                    for (int i = 0; i < 6; i++)
                    {
                        if (StateOfMonster[1, i].Substring(0, 3) == ID)
                        {
                            return StateOfMonster[1, i];
                        }
                    }
                    break;
                case "2":
                    for (int i = 0; i < 3; i++)
                    {
                        if (StateOfMonster[2, i].Substring(0, 3) == ID)
                        {
                            return StateOfMonster[2, i];
                        }
                    }
                    break;

            }
            return "";
        }
        public void Move(GameObject Unit, GameObject EnemyUnit, int MoveX, int MoveY, int x, int y)
        {
            //움직일때
            //Move.Script 사용
            Unit.AddComponent<Move>();
            EnemyUnit.AddComponent<Move>();
            Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
            Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
            EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
            EnemyUnit.GetComponent<Move>().EndPosition = Unit.transform.position;
     
            Data.Instance.pan[MoveY, MoveX] = Data.Instance.pan[y, x];
            Data.Instance.pan[y, x] = null;
            string temp = Unit.name;
            Unit.name = EnemyUnit.name;
            EnemyUnit.name = temp;
            Unit.GetComponent<Move>().play = true;
            EnemyUnit.GetComponent<Move>().play = true;
        }
        public void KnockBack(GameObject Unit, GameObject EnemyUnit, int MoveX, int MoveY, int x, int y, int MoveDirection)
        {
            string temp;
            switch (MoveDirection)
            {
                case 0:
                    if (pan[MoveY, MoveX + 1] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find(MoveY + "," + (MoveX + 1));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY, MoveX + 1] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;
                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
                case 1:
                    if (pan[MoveY, MoveX - 1] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find(MoveY + "," + (MoveX - 1));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY, MoveX - 1] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;
                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
                case 2:
                    if (pan[MoveY-1, MoveX ] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find((MoveY-1) + "," + (MoveX));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY-1, MoveX] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;
                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
                case 3:
                    if (pan[MoveY + 1, MoveX] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find((MoveY + 1) + "," + (MoveX));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY + 1, MoveX] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;

                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
                case 4:
                    if (pan[MoveY + 1, MoveX+1] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find((MoveY + 1) + "," + (MoveX+1));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY +1, MoveX+1] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;
                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
                case 5:
                    if (pan[MoveY - 1, MoveX + 1] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find((MoveY - 1) + "," + (MoveX+1));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY - 1, MoveX+1] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;
                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
                case 6:
                    if (pan[MoveY - 1, MoveX - 1] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find((MoveY - 1) + "," + (MoveX-1));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY - 1, MoveX-1] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;
                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
                case 7:
                    if (pan[MoveY + 1, MoveX - 1] == null)
                    {
                        //넉백가능
                        GameObject KnockBackBoard = GameObject.Find((MoveY + 1) + "," + (MoveX-1));
                        Unit.AddComponent<Move>();
                        EnemyUnit.AddComponent<Move>();
                        KnockBackBoard.AddComponent<Move>();
                        Unit.GetComponent<Move>().StartPosition = Unit.transform.position;
                        Unit.GetComponent<Move>().EndPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().StartPosition = EnemyUnit.transform.position;
                        EnemyUnit.GetComponent<Move>().EndPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().StartPosition = KnockBackBoard.transform.position;
                        KnockBackBoard.GetComponent<Move>().EndPosition = Unit.transform.position;
                        Data.instance.pan[MoveY + 1, MoveX-1] = Data.instance.pan[MoveY, MoveX];
                        Data.instance.pan[MoveY, MoveX] = Data.instance.pan[y, x];
                        Data.instance.pan[y, x] = null;
                        temp = Unit.name;
                        Unit.name = EnemyUnit.name;
                        EnemyUnit.name = KnockBackBoard.name;
                        KnockBackBoard.name = temp;
                        Unit.GetComponent<Move>().play = true;
                        EnemyUnit.GetComponent<Move>().play = true;
                        KnockBackBoard.GetComponent<Move>().play = true;
                    }
                    else
                    {
                        //넉백불가
                    }
                    break;
            }
        }
    }
}


/*사용 방법
 
     쓰고싶은 cs파일안에 들어가 객체 선언을 합니다.
     static MonsterData test1=MonsterData.Instance;
     쓰고싶은 문자열을 지정합니다.
     string s1 = test1.stateMonster[0,0].Substring(0,3);
     Substring은 지정된 문자열을 빼오는 겁니다.
     위에는 몬스터 배열 0,0,칸에 있는 몬스터 ID를 빼오는 코드입니다. 
     
     */
