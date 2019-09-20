using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MonsterChessClient
{
    public class BoardButtonOfPlace : MonoBehaviour
    {

        public void PlaceBoardOn()
        {
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Place)
            {
                //플레이스 에서의 소환
                if (Data.Instance.SommonOn == true)
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
                            CheckPan();
                            Texture UnitImage = Resources.Load("Image/UnitMy/" + Data.Instance.SommonID) as Texture;
                            gameObject.GetComponent<RawImage>().texture = UnitImage;
                            gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                            gameObject.AddComponent<Move>();
                            //문자열을 사용하여 스크립트 불러오기
                            switch (Data.Instance.SommonID)
                            {
                                case "000":
                                    gameObject.AddComponent<Unit000>();
                                    gameObject.GetComponent<Unit000>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit000>().x = x;
                                    gameObject.GetComponent<Unit000>().y = y;
                                    gameObject.GetComponent<Unit000>().Status = 0;
                                    break;
                                case "001":
                                    gameObject.AddComponent<Unit001>();
                                    gameObject.GetComponent<Unit001>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit001>().x = x;
                                    gameObject.GetComponent<Unit001>().y = y;
                                    gameObject.GetComponent<Unit001>().Status = 0;
                                    break;
                                case "002":
                                    gameObject.AddComponent<Unit002>();
                                    gameObject.GetComponent<Unit002>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit002>().x = x;
                                    gameObject.GetComponent<Unit002>().y = y;
                                    gameObject.GetComponent<Unit002>().Status = 0;
                                    break;
                                case "003":
                                    gameObject.AddComponent<Unit003>();
                                    gameObject.GetComponent<Unit003>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit003>().x = x;
                                    gameObject.GetComponent<Unit003>().y = y;
                                    gameObject.GetComponent<Unit003>().Status = 0;
                                    break;
                                case "004":
                                    gameObject.AddComponent<Unit004>();
                                    gameObject.GetComponent<Unit004>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit004>().x = x;
                                    gameObject.GetComponent<Unit004>().y = y;
                                    gameObject.GetComponent<Unit004>().Status = 0;
                                    break;
                                case "005":
                                    gameObject.AddComponent<Unit005>();
                                    gameObject.GetComponent<Unit005>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit005>().x = x;
                                    gameObject.GetComponent<Unit005>().y = y;
                                    gameObject.GetComponent<Unit005>().Status = 0;
                                    break;
                                case "006":
                                    gameObject.AddComponent<Unit006>();
                                    gameObject.GetComponent<Unit006>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit006>().x = x;
                                    gameObject.GetComponent<Unit006>().y = y;
                                    gameObject.GetComponent<Unit006>().Status = 0;
                                    break;
                                case "007":
                                    gameObject.AddComponent<Unit007>();
                                    gameObject.GetComponent<Unit007>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit007>().x = x;
                                    gameObject.GetComponent<Unit007>().y = y;
                                    gameObject.GetComponent<Unit007>().Status = 0;
                                    break;
                                case "008":
                                    gameObject.AddComponent<Unit008>();
                                    gameObject.GetComponent<Unit008>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit008>().x = x;
                                    gameObject.GetComponent<Unit008>().y = y;
                                    gameObject.GetComponent<Unit008>().Status = 0;
                                    break;
                                case "009":
                                    gameObject.AddComponent<Unit009>();
                                    gameObject.GetComponent<Unit009>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit009>().x = x;
                                    gameObject.GetComponent<Unit009>().y = y;
                                    gameObject.GetComponent<Unit009>().Status = 0;
                                    break;
                                case "010":
                                    gameObject.AddComponent<Unit010>();
                                    gameObject.GetComponent<Unit010>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit010>().x = x;
                                    gameObject.GetComponent<Unit010>().y = y;
                                    gameObject.GetComponent<Unit010>().Status = 0;
                                    break;
                                case "011":
                                    gameObject.AddComponent<Unit011>();
                                    gameObject.GetComponent<Unit011>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit011>().x = x;
                                    gameObject.GetComponent<Unit011>().y = y;
                                    gameObject.GetComponent<Unit011>().Status = 0;
                                    break;
                                case "100":
                                    gameObject.AddComponent<Unit100>();
                                    gameObject.GetComponent<Unit100>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit100>().x = x;
                                    gameObject.GetComponent<Unit100>().y = y;
                                    gameObject.GetComponent<Unit100>().Status = 0;
                                    break;
                                case "101":
                                    gameObject.AddComponent<Unit101>();
                                    gameObject.GetComponent<Unit101>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit101>().x = x;
                                    gameObject.GetComponent<Unit101>().y = y;
                                    gameObject.GetComponent<Unit101>().Status = 0;
                                    break;
                                case "102":
                                    gameObject.AddComponent<Unit102>();
                                    gameObject.GetComponent<Unit102>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit102>().x = x;
                                    gameObject.GetComponent<Unit102>().y = y;
                                    gameObject.GetComponent<Unit102>().Status = 0;
                                    break;
                                case "103":
                                    gameObject.AddComponent<Unit103>();
                                    gameObject.GetComponent<Unit103>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit103>().x = x;
                                    gameObject.GetComponent<Unit103>().y = y;
                                    gameObject.GetComponent<Unit103>().Status = 0;
                                    break;
                                case "104":
                                    gameObject.AddComponent<Unit104>();
                                    gameObject.GetComponent<Unit104>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit104>().x = x;
                                    gameObject.GetComponent<Unit104>().y = y;
                                    gameObject.GetComponent<Unit104>().Status = 0;
                                    break;
                                case "105":
                                    gameObject.AddComponent<Unit105>();
                                    gameObject.GetComponent<Unit105>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit105>().x = x;
                                    gameObject.GetComponent<Unit105>().y = y;
                                    gameObject.GetComponent<Unit105>().Status = 0;
                                    break;

                            }
                            Data.Instance.pan[y, x] = Data.Instance.SommonID;
                            Data.Instance.SommonOn = false;
                        }
                    }

                }
            }

        }
        void CheckPan()
        {
            //소환했던 거라면 취소하고 정한위치에 재소환
            for (int i = 0; i < 3; i++)
            {
                //y값 설정
                for (int j = 0; j < 7; j++)
                {
                    //x값 설정
                    if (Data.Instance.pan[i, j] == Data.Instance.SommonID)
                    {
                        GameObject temp = GameObject.Find("" + i + "," + j);
                        temp.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                        Data.Instance.pan[i, j] = null;
                    }
                }
            }

        }
    }

}

