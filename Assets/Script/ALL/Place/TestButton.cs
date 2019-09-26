using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class TestButton : MonoBehaviour
    {
        public void StartTest()
        {
            ReverseBoard();
            int count = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Data.Instance.board[i, j] != null)
                    {
                        if (i < 3)
                        {

                        }
                        count++;
                    }
                }
            }

            if (count == 12)
            {
                Data.Instance.bSommons = false; //다음 씬으로 넘어갈때 소환을 끈다.
                GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Play;
            }
            else
            {
                Debug.Log("리스트의 갯수가 모자랍니다.");
            }
        }

        void ReverseBoard()
        {
            Debug.Log("리버스 판");
            for (int i = 0; i < 3; i++)
            {
                // y값
                for (int j = 0; j < 7; j++)
                {
                    // x값
                    if (Data.Instance.board[i, j] != null)
                    {
                        if (i <= 2)
                        {
                            int index = 6 - i;
                            Data.Instance.board[index, j] = Data.Instance.board[i, j];
                            GameObject TempUnit = GameObject.Find(index + "," + j);
                            Texture UnitImage = Resources.Load("Image/UnitEnemy/" + Data.Instance.board[index, j]) as Texture;
                            Unit unit = null;
                            switch (Data.Instance.board[index, j])
                            {
                                case "000": unit = TempUnit.AddComponent<Unit000>(); break;
                                case "001": unit = TempUnit.AddComponent<Unit001>(); break;
                                case "002": unit = TempUnit.AddComponent<Unit002>(); break;
                                case "003": unit = TempUnit.AddComponent<Unit003>(); break;
                                case "004": unit = TempUnit.AddComponent<Unit004>(); break;
                                case "005": unit = TempUnit.AddComponent<Unit005>(); break;
                                case "006": unit = TempUnit.AddComponent<Unit006>(); break;
                                case "007": unit = TempUnit.AddComponent<Unit007>(); break;
                                case "008": unit = TempUnit.AddComponent<Unit008>(); break;
                                case "009": unit = TempUnit.AddComponent<Unit009>(); break;
                                case "010": unit = TempUnit.AddComponent<Unit010>(); break;
                                case "011": unit = TempUnit.AddComponent<Unit011>(); break;
                                case "100": unit = TempUnit.AddComponent<Unit100>(); break;
                                case "101": unit = TempUnit.AddComponent<Unit101>(); break;
                                case "102": unit = TempUnit.AddComponent<Unit102>(); break;
                                case "103": unit = TempUnit.AddComponent<Unit103>(); break;
                                case "104": unit = TempUnit.AddComponent<Unit104>(); break;
                                case "105": unit = TempUnit.AddComponent<Unit105>(); break;
                                case "200": unit = TempUnit.AddComponent<Unit200>(); break;
                                case "201": unit = TempUnit.AddComponent<Unit201>(); break;
                                case "202": unit = TempUnit.AddComponent<Unit202>(); break;
                            }

                            if (unit != null)
                            {
                                unit.order = 1;
                                unit.x = index;
                                unit.y = j;
                                unit.status = 0;
                            }

                            TempUnit.GetComponent<RawImage>().texture = UnitImage;
                            TempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                        }
                    }
                }
            }
        }
    }
}