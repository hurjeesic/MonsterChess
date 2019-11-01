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
                "0001122121사냥꾼",
                "0011122220정령궁수",
                "0021123231마법사",
                "0031123321대포",
                "0041124241히드라",
                "0051124331주작",
                "300000000000","300000000000","300000000000","300000000000","300000000000","300000000000"
               
            },
            {
                "1001101211고블린",
                "1011001212벤시",
                "1021001220놀",
                "1032102221드레이크",
                "1042102320오크",
                "1051002312골렘",
                "1062103330트윈헤드오우거",
                "1072003240광전사",
                "1081103312웜",
                "1092104223데스나이트",
                "1102104331발키리",
                "1112104322용사"
            },
            {
                "2003100533제우스",
                "2013100443아누비스",
                "2022100353해태",
                "300000000000", "300000000000","300000000000", "300000000000", "300000000000", "300000000000", "300000000000",
                "300000000000", "300000000000"
            }
        };
        // ID(3) , 이동거리(1), 이동방향(1), 공격거리(1), 코스트(1), HP(1), AP(1), 이름
        // 0 ~ 2 ,    3     ,      4     ,       5      ,    6    ,    7   ,  8   , 9 ~

        public const int COLUMN = 7, ROW = 7;
        public KeyValuePair<byte, Unit>[,] board = new KeyValuePair<byte, Unit>[COLUMN, ROW]; // 현재 판의 데이터 배열
        public KeyValuePair<byte, Unit> Empty = new KeyValuePair<byte, Unit>(255, null);

        public string[] units = new string[6]; // 유닛 넣는곳(임시 데이터)

        public List<GameObject> playList = new List<GameObject>();
        public List<string> directionList = new List<string>(); // 유닛의 이동방향을 나타냄
        public string summonId; //소환할 ID를 가져옴

        public byte myIndex = 0;
        public byte currentPlayer = 0;

        public int mana = 10; // 소환에 필요한 마나
        public int turnNum = 0; // 턴 수
        public int order = 0; // 0 = 선공, 1 = 후공
        public int time = 50; // 턴의 시간

        public GameObject origin;

        public bool bSummons; // 소환
        public bool bManaStone; // 마나스톤 생성
        public bool bMoving = false;
        public bool bPlaying = false;


        // 여기서 부터는 SelectUnit 관련 변수들

        //선택되었는지 보는것
        public bool[,] In =
        {
            { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false },
            { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false }
        };

        public int kind = 0; // 현재 선택된 탭의 번호
        public int MonsterCount = 0;//선택된 몬스터의 수
        public string unitId; // 선택된 몬스터의 이름
        public int costSum = 0; // 선택된 몬스터의 코스트 합
        public bool bHero = false; // 히어로가 선택 되었는지
        public readonly int MaxCost = 10;

        public void AnimStop()
        {
            for (int i = 0; i < COLUMN; i++)
            {
                for (int j = 0; j < ROW; j++)
                {
                 
                    GameObject temp = GameObject.Find(i+","+j);
                    Animation anim = temp.GetComponent<Animation>();
                    anim.Stop("LightUnit");
                    Unit unit = temp.GetComponent<Unit>();
                    if (unit != null) temp.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                    else temp.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                }
            }

        }
        //함수
        public void GetMoveRange(int x, int y, int direction, int distence, List<KeyValuePair<int, GameObject>> range)
        {
            AnimStop();
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
                            Debug.Log(range[range.Count() - 1]);
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
                        range[j].Value.GetComponent<RawImage>().color = new Color(255, 255, 255, board[rangeX, rangeY].Value == null ? 0 : 255);
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
                            range[j].Value.GetComponent<RawImage>().color = new Color(255, 255, 255, board[rangeY, rangeX].Value == null ? 0 : 255);
                            
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
            for (int i = 0; i < 12; i++)
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
            Vector3 startPos= unit.transform.position;
            Vector3 endPos = enemyUnit.transform.position;

            Move moveUnit = unit.AddComponent<Move>();
            Move moveEnemy = enemyUnit.AddComponent<Move>();

            moveUnit.startPos = startPos;
            moveUnit.endPos = endPos;
            moveEnemy.startPos = endPos;
            moveEnemy.endPos = startPos;

            moveUnit.bPlay = true;
            moveEnemy.bPlay = true;

            if (moveX == x && moveY == y)//같은좌표일경우
            {
                
            }
            else
            {
                Data.instance.board[moveX, moveY] = Data.instance.board[x, y];
                Data.instance.board[x, y] = Data.instance.Empty;
                Data.instance.board[moveX, moveY].Value.x = moveX;
                Data.instance.board[moveX, moveY].Value.y = moveY;
                Debug.Log("이동보드값" + x + "," + y + "\t" + Data.instance.board[moveX, moveY]);
                Debug.Log("이동보드값" + moveX + "," + moveY + "\t" + Data.instance.board[x, y]);
            }
           
            string temp = unit.name;
            unit.name = enemyUnit.name;
            enemyUnit.name = temp;
        }

        public void KnockBack(int X, int Y, int EnemyX, int EnemyY, int EnemyMoveX, int EnemyMoveY)
        {
            string temp;
            GameObject KnockBackUnit = null;
            Debug.Log("넉백");

            //넉백좌표
            int tempX = EnemyMoveX;
            int tempY = EnemyMoveY;
            //넉백유닛
            KnockBackUnit = GameObject.Find(tempX + "," + tempY);
            GameObject unit = GameObject.Find(X + "," + Y);
            GameObject enemyUnit = GameObject.Find(EnemyX + "," + EnemyY);


            unit.AddComponent<Move>();
            enemyUnit.AddComponent<Move>();
            KnockBackUnit.AddComponent<Move>();

            unit.GetComponent<Move>().startPos = unit.transform.position;
            unit.GetComponent<Move>().endPos = enemyUnit.transform.position;
            enemyUnit.GetComponent<Move>().startPos = enemyUnit.transform.position;
            enemyUnit.GetComponent<Move>().endPos = KnockBackUnit.transform.position;
            KnockBackUnit.GetComponent<Move>().startPos = KnockBackUnit.transform.position;
            KnockBackUnit.GetComponent<Move>().endPos = unit.transform.position;

          


         

            unit.GetComponent<Move>().bPlay = true;
            enemyUnit.GetComponent<Move>().bPlay = true;
            KnockBackUnit.GetComponent<Move>().bPlay = true;

            temp = unit.name;
            unit.name = enemyUnit.name;
            enemyUnit.name = KnockBackUnit.name;
            KnockBackUnit.name = temp;
           

        }
    }
}