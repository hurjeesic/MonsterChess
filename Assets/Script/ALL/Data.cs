using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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
        public List<string> PlayList = new List<string>();//실제 게임진행하는 리스트
        public List<string> MoveDirection= new List<string>(); // 유닛의 이동방향을 나타냄
        public string SommonID;//소환할 ID를 가져옴


        public int Mana = 10;//소환에 필요한 마나
        public int Turn = 0;//턴수
        public int Order = 0;// 0=선공, 1=후공
        public float Time = 50f; // 턴의 시간

        public GameObject Origin;

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
