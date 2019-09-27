using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class BoardButtonOfPlay : MonoBehaviour
    {
        public void OnBoard()
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Play)
            {
                // 플레이에서의 기능
                if (Data.Instance.bSommons == true && Data.Instance.time <= 30)
                {
                    int x = int.Parse(gameObject.name.Substring(2));
                    int y = int.Parse(gameObject.name.Substring(0, 1));
                    int cost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.sommonId).Substring(6, 1));
                    if (Data.Instance.mana - cost >= 0)
                    {
                        if (Data.Instance.board[y, x] == null)
                        {
                            // 소환은 즉각 적용 시키고 플레이 리스트에 제외 시킨다.
                            x = int.Parse(gameObject.name.Substring(2));
                            y = int.Parse(gameObject.name.Substring(0, 1));
                            gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.sommonId) as Texture;
                            gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);

                            Data.Instance.board[y, x] = Data.Instance.sommonId;
                            Data.Instance.bSommons = false;
                            Data.Instance.mana -= cost;

                            Unit unit = null;
                            switch (Data.Instance.sommonId)
                            {
                                case "000": unit = gameObject.AddComponent<Unit000>(); break;
                                case "001": unit = gameObject.AddComponent<Unit001>(); break;
                                case "002": unit = gameObject.AddComponent<Unit002>(); break;
                                case "003": unit = gameObject.AddComponent<Unit003>(); break;
                                case "004": unit = gameObject.AddComponent<Unit004>(); break;
                                case "005": unit = gameObject.AddComponent<Unit005>(); break;
                                case "006": unit = gameObject.AddComponent<Unit006>(); break;
                                case "007": unit = gameObject.AddComponent<Unit007>(); break;
                                case "008": unit = gameObject.AddComponent<Unit008>(); break;
                                case "009": unit = gameObject.AddComponent<Unit009>(); break;
                                case "010": unit = gameObject.AddComponent<Unit010>(); break;
                                case "011": unit = gameObject.AddComponent<Unit011>(); break;
                                case "100": unit = gameObject.AddComponent<Unit100>(); break;
                                case "101": unit = gameObject.AddComponent<Unit101>(); break;
                                case "102": unit = gameObject.AddComponent<Unit102>(); break;
                                case "103": unit = gameObject.AddComponent<Unit103>(); break;
                                case "104": unit = gameObject.AddComponent<Unit104>(); break;
                                case "105": unit = gameObject.AddComponent<Unit105>(); break;
                                case "200": unit = gameObject.AddComponent<Unit200>(); break;
                                case "201": unit = gameObject.AddComponent<Unit201>(); break;
                                case "202": unit = gameObject.AddComponent<Unit202>(); break;
                            }

                            if (unit != null)
                            {
                                unit.order = Data.Instance.order;
                                unit.x = x;
                                unit.y = y;
                                unit.status = 2;
                            }
                        }
                        else
                        {
                            Debug.Log("배치할려는 칸에 유닛 존재");
                        }
                    }
                    else
                    {
                        Debug.Log("마나부족");
                    }
                }
                else if (Data.Instance.bMoving == false && Data.Instance.time <= 30)
                {
                    // 터치한 유닛의 이동 범위를 가져옴
                    int x = int.Parse(gameObject.name.Substring(2));
                    int y = int.Parse(gameObject.name.Substring(0, 1));
                    if (Data.Instance.board[y, x] != null)
                    {
                        gameObject.GetComponent<Unit>().MoveRange();
                    }
                    
                    Data.Instance.origin = gameObject;
                    Data.Instance.bMoving = true;
                }
                else
                {
                    int MoveX = int.Parse(gameObject.name.Substring(2));
                    int MoveY = int.Parse(gameObject.name.Substring(0, 1));

                    // 이동범위 클릭시 무브 리스트 변경
                    int x = int.Parse(Data.Instance.origin.name.Substring(2));
                    int y = int.Parse(Data.Instance.origin.name.Substring(0, 1));
                    if (Data.Instance.board[y, x] != null)
                    {
                        Unit unit = Data.Instance.origin.GetComponent<Unit>();
                        unit.moveX = MoveX;
                        unit.moveY = MoveY;
                        unit.SaveMove();
                    }
                    
                    Data.Instance.bMoving = false;
                }
            }
        }
    }
}
