using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class BoardButtonOfPlace : MonoBehaviour
    {
        public void SommonUnit()
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Place)
            {
                // 플레이스에서의 소환
                if (Data.Instance.bSommons == true)
                {
                    int x = int.Parse(gameObject.name.Substring(2));
                    int y = int.Parse(gameObject.name.Substring(0, 1));
                    if (y < 3)
                    {
                        if (x == 3 && y == 0)
                        {
                            Debug.Log("히어로 칸 입니다.");
                        }
                        else
                        {
                            UpdateBoard();
                            gameObject.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.sommonId) as Texture;
                            gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                            gameObject.AddComponent<Move>();

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
                            }

                            if (unit != null)
                            {
                                unit.order = Data.Instance.order;
                                unit.x = x;
                                unit.y = y;
                                unit.status = 0;
                            }
                            
                            Debug.Log("소환");
                            Data.Instance.board[y, x] = Data.Instance.sommonId;
                            Data.Instance.bSommons = false;
                        }
                    }
                }
            }
        }

        void UpdateBoard()
        {
            // 소환했던 거라면 취소하고 정한위치에 재소환
            for (int i = 0; i < 3; i++)
            {
                // y값 설정
                for (int j = 0; j < 7; j++)
                {
                    // x값 설정
                    if (Data.Instance.board[i, j] == Data.Instance.sommonId)
                    {
                        GameObject.Find(i + "," + j).GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        Data.Instance.board[i, j] = null;
                    }
                }
            }
        }
    }
}

