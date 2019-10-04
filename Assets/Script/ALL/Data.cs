using UnitType;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    /// <summary>
    /// 사용 방법
    /// 
    /// 쓰고 싶은 파일에서 객체를 선언
    /// ex) static MonsterData test1 = MonsterData.Instance;
    /// 쓰고 싶은 문자열을 지정
    /// ex) string s1 = test1.StateMonster[0, 0].Substring(0, 3);
    /// Substring은 지정된 문자열을 빼오는 메소드
    /// 위에는 몬스터 배열 0,0,칸에 있는 몬스터 ID를 빼오는 코드
    /// </summary>
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
        public string[,] StateOfMonster =
        {
            {
                "000100121고블린", "001110121벤시", "002100121놀", "003210222드레이크", "004110232오크",
                "005100232골렘", "006210342트윈헤드오우거", "007200333광전사", "008110332웜", "009210443데스나이트",
                "010210434발키리","011210443용사"
            },
            {
                "100112221사냥꾼", "101112222정령궁수", "102112332마법사", "103112322대포", "104112433히드라",
                "105112442주작", "000000000000","000000000000","000000000000","000000000000","000000000000","000000000000"
            },
            {
                "200310053제우스", "201310053아누비스", "202210054해태", "000000000000", "000000000000",
                "000000000000", "000000000000", "000000000000", "000000000000", "000000000000",
                "000000000000", "000000000000"
            }
        };
        // ID(3) , 이동거리(1), 이동방향(1), 공격거리(1), 코스트(1), HP(1), AP(1), 이름
        // 0 ~ 2 ,    3     ,     4     ,    5      ,    6    ,  7   ,  8   , 9 ~

        public const int COLUMN = 7, ROW = 7;
        public string[,] board = new string[COLUMN, ROW]; // 현재 판의 데이터 배열
        public string[] units = new string[6]; // 유닛 넣는곳(임시 데이터)
        
        public List<string> directionList = new List<string>(); // 유닛의 이동방향을 나타냄
        public string summonId; //소환할 ID를 가져옴

        public byte myIndex = 0;
        public byte currentPlayer = 0;

        public int mana = 10; // 소환에 필요한 마나
        public int turnNum = 0; // 턴 수
        public int order = 0; // 0 = 선공, 1 = 후공
        public int time = 50; // 턴의 시간

        public GameObject origin;
        public List<GameObject> playList = new List<GameObject>(); // 실제 게임 진행하는 리스트

        public bool bSummons; // 소환
        public bool bManaStone; // 마나스톤 생성
        public bool bMoving = false;
        public bool bPlaying = false;
        public bool changeListOn; // 준비시간에 -> 의도를 모르겠네

        // 여기서 부터는 SelectUnit 관련 변수들

        //선택되었는지 보는것
        public bool[,] In =
        {
            { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
        };

        public int kind = 0; // 현재 선택된 탭의 번호
        public string unitId; // 선택된 몬스터의 이름
        public int costSum = 0; // 선택된 몬스터의 코스트 합
        public bool bHero = false; // 히어로가 선택 되었는지
        public readonly int MaxCost = 15;

        //함수
        public void GetMoveRange(int x, int y, int direction, int distence, List<KeyValuePair<int, GameObject>> range)
        {
            if (direction < 2)
            {
                for (int i = 0; i < (direction == 0 ? 4 : 8); i++)
                {
                    // 0 = 동, 1 = 서, 2 = 남, 3 = 북, 4 = 북동, 5 = 남동, 6 = 남서, 7 = 북서
                    for (int j = 1; j < (distence + 1); j++)
                    {
                        GameObject posObj = null;
                        switch (i)
                        {
                            case 0: if (x + j < COLUMN) posObj = GameObject.Find((x + j) + "," + y); break;
                            case 1: if (x - j >= 0) posObj = GameObject.Find((x - j) + "," + y); break;
                            case 2: if (y - j >= 0) posObj = GameObject.Find(x + "," + (y - j)); break;
                            case 3: if (y + j < COLUMN) posObj = GameObject.Find(x + "," + (y + j)); break;
                            case 4: if (y + j < COLUMN && x + j < COLUMN) posObj = GameObject.Find((x + j) + "," + (y + j)); break;
                            case 5: if (y - j >= 0 && x + j < COLUMN) posObj = GameObject.Find((x + j) + "," + (y - j)); break;
                            case 6: if (y - j >= 0 && x - j >= 0) posObj = GameObject.Find((x - j) + "," + (y - j)); break;
                            case 7: if (y + j < COLUMN && x - j >= 0) posObj = GameObject.Find((x - j) + "," + (y + j)); break;
                        }

                        if (posObj != null)
                        {
                            range.Add(new KeyValuePair<int, GameObject>(i, posObj));
                        }
                    }
                }
            }
            
            Debug.Log("에니메이션");
            for (int i = 0; i < range.Count(); i++)
            {
                Animation anim = range[i].Value.GetComponent<Animation>();
                anim.Play("LightUnit");
            }
        }

        public void SaveMove(List<KeyValuePair<int, GameObject>> range, int direction, int moveX, int moveY, int status)
        {
            for (int i = 0; i < range.Count; i++)
            {
                int rangeX = range[i].Value.name[0] - '0';
                int rangeY = range[i].Value.name[2] - '0';
                int x = origin.name[0] - '0';
                int y = origin.name[2] - '0';
                if (range[i].Value.name == moveX + "," + moveY)
                {
                    origin.GetComponent<Unit>().status = 1;
                    origin.GetComponent<Unit>().moveDirection = range[i].Key;
                    moveX = rangeX;
                    moveY = rangeY;
                    for (int j = 0; j < range.Count; j++)
                    {
                        Animation anim = range[j].Value.GetComponent<Animation>();
                        anim.Stop("LightUnit");
                        rangeX = range[j].Value.name[0] - '0';
                        rangeY = range[j].Value.name[2] - '0';
                        range[j].Value.GetComponent<RawImage>().color = new Color(255, 255, 255, board[rangeX, rangeY] == null ? 0 : 255);
                        if (j == range.Count - 1)
                        {
                            return;
                        }
                    }
                    if (i == range.Count - 1)
                    {
                        Debug.Log("이동범위가 아닙니다.");
                        Debug.Log("유닛부터 다시 선택하여 주십시요");
                        for (int j = 0; j < range.Count; j++)
                        {
                            Animation anim = range[j].Value.GetComponent<Animation>();
                            anim.Stop("LightUnit");
                            rangeX = range[j].Value.name[0] - '0';
                            rangeY = range[j].Value.name[2] - '0';
                            range[j].Value.GetComponent<RawImage>().color = new Color(255, 255, 255, board[rangeY, rangeX] == null ? 0 : 255);
                            
                            if (j == range.Count - 1)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }

        public string FindStateOfMonster(string id)
        {
            // ID를 통해 몬스터 전체 데이터를 가져옴
            int x = int.Parse(id.Substring(0, 1));
            for (int i = 0; i < 24 / ((x + 1) * 2); i++)
            {
                if (StateOfMonster[x, i].Substring(0, 3) == id)
                {
                    return StateOfMonster[x, i];
                }
            }
            
            return "";
        }

        public void Move(GameObject unit, GameObject enemyUnit, int moveX, int moveY, int x, int y)
        {
            unit.GetComponent<Move>().startPos = unit.transform.position;
            unit.GetComponent<Move>().endPos = enemyUnit.transform.position;
            enemyUnit.GetComponent<Move>().startPos = enemyUnit.transform.position;
            enemyUnit.GetComponent<Move>().endPos = unit.transform.position;
     
            board[moveX, moveY] = board[x, y];
            board[x, y] = null;

            string temp = unit.name;
            unit.name = enemyUnit.name;
            enemyUnit.name = temp;

            unit.GetComponent<Move>().bPlay = true;
            enemyUnit.GetComponent<Move>().bPlay = true;

            unit.GetComponent<Unit>().x = moveX;
            unit.GetComponent<Unit>().y = moveY;
           
        }

        public void KnockBack(GameObject unit, GameObject enemyUnit, int moveX, int moveY, int x, int y, int moveDirection)
        {
            string temp;
            GameObject KnockBackUnit = null;
            int tempY = -1, tempX = -1;
            switch (moveDirection)
            {
                case 0: tempX = moveX + 1; tempY = moveY; break;
                case 1: tempX = moveX - 1; tempY = moveY; break;
                case 2: tempX = moveX; tempY = moveY - 1; break;
                case 3: tempX = moveX; tempY = moveY + 1; break;
                case 4: tempX = moveX + 1; tempY = moveY + 1; break;
                case 5: tempX = moveX + 1; tempY = moveY - 1; break;
                case 6: tempX = moveX - 1; tempY = moveY - 1; break;
                case 7: tempX = moveX - 1; tempY = moveY + 1; break;
            }

            if (tempY != -1 && board[tempX, tempY] == null)
            {
                KnockBackUnit = GameObject.Find(tempX + "," + tempY);

                unit.GetComponent<Move>().startPos = unit.transform.position;
                unit.GetComponent<Move>().endPos = enemyUnit.transform.position;
                enemyUnit.GetComponent<Move>().startPos = enemyUnit.transform.position;
                enemyUnit.GetComponent<Move>().endPos = KnockBackUnit.transform.position;
                KnockBackUnit.GetComponent<Move>().startPos = KnockBackUnit.transform.position;
                KnockBackUnit.GetComponent<Move>().endPos = unit.transform.position;

                board[tempX, tempY] = board[moveX, moveY];
                board[moveX, moveY] = board[x, y];
                board[x, y] = null;

                temp = unit.name;
                unit.name = enemyUnit.name;
                enemyUnit.name = KnockBackUnit.name;
                KnockBackUnit.name = temp;

                unit.GetComponent<Move>().bPlay = true;
                enemyUnit.GetComponent<Move>().bPlay = true;
                KnockBackUnit.GetComponent<Move>().bPlay = true;
            }
        }
    }
}