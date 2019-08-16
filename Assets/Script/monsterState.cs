using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MonsterData
{

    private static MonsterData instance = null;
    public static MonsterData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MonsterData();
            }
            return instance;
        }
    }

    private MonsterData()
    {
    }
    public int Cost = 0;
    public string[,] StateOfMonster = {{"000100121고블린","001110121벤시","002100121놀","003210222드레이크","004110232오크"
,"005100232골렘","006210342트윈헤드오우거","007200333광전사","008110332웜","009210443데스나이트"
,"010210434발키리","011210443용사" },
        {"100112221사냥꾼","101112222정령궁수","102112332마법사","103112322대포"
,"104112433히드라","105112442주작","","","","","",""},
        {"200320053제우스","201310053아누비스","202210054해태","","","","","","","","",""}};
    public string[,] pan = new string [7,7];
    public string[] Unit = { "002","005", "008","101","102","201"};//유닛넣는곳
    public bool SommonON;
    public bool MoveON;
    public int ManaStone = 0;
    public int Kind = 0;
    public string MonsterID;
    public List<string> MonsterList = new List<string>();
    public string SommonID;
    public int Mana=0;
    public float Time = 30f;
    public List<string> MoveList = new List<string>();
    public int trun; // 0=선공, 1=후공
    public int[] CountUnit = {0,0,0};
}


/*사용 방법
 
     쓰고싶은 cs파일안에 들어가 객체 선언을 합니다.
     static MonsterData test1=MonsterData.Instance;
     쓰고싶은 문자열을 지정합니다.
     string s1 = test1.stateMonster[0,0].Substring(0,3);
     Substring은 지정된 문자열을 빼오는 겁니다.
     위에는 몬스터 배열 0,0,칸에 있는 몬스터 ID를 빼오는 코드입니다. 
     
     */
