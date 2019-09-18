using System.Linq;
using UnityEngine;
using UnityEngine.UI;


namespace MonsterChessClient
{

    public class TestButton : MonoBehaviour
    {
        
         
        public void TestOn()
        {
            ReversePan();
            int count = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Data.Instance.pan[i, j] != null)
                    {
                       
                        count++;
                    }
                }
            }
            if (count == 12)
            {
                Data.Instance.SommonOn = false;//다음 씬으로 넘어갈때 소환을 끈다.
                GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Play;
            }
            else
            {
              
                Debug.Log("리스트의 갯수가 모자랍니다.");
            }

        }

        void ReversePan()
        {
            Debug.Log("리버스 판");
            for (int i = 0; i < 3; i++)
            {
                //y값
                for (int j = 0; j < 7; j++)
                {
                    //x값
                    if (Data.Instance.pan[i, j] != null)
                    {
                        switch (i)
                        {
                            case 0:
                                Data.Instance.pan[6, j] = Data.Instance.pan[i, j];
                                GameObject TempUnit = GameObject.Find("" + 6 + "," + j);
                                Texture UnitImage = Resources.Load("Image/UnitEnemy/" + Data.Instance.pan[6,j]) as Texture;
                                switch (Data.Instance.pan[6,j])
                                {
                                    case "000":
                                        TempUnit.AddComponent<Unit000>();
                                        TempUnit.GetComponent<Unit000>().Order = 1;
                                        TempUnit.GetComponent<Unit000>().x = 6;
                                        TempUnit.GetComponent<Unit000>().y = j;
                                        TempUnit.GetComponent<Unit000>().Status = 0;
                                        break;
                                    case "001":
                                        TempUnit.AddComponent<Unit001>();
                                        TempUnit.GetComponent<Unit001>().Order = 1;
                                        TempUnit.GetComponent<Unit001>().x = 6;
                                        TempUnit.GetComponent<Unit001>().y = j;
                                        TempUnit.GetComponent<Unit001>().Status = 0;
                                        break;
                                    case "002":
                                        TempUnit.AddComponent<Unit002>();
                                        TempUnit.GetComponent<Unit002>().Order = 1;
                                        TempUnit.GetComponent<Unit002>().x = 6;
                                        TempUnit.GetComponent<Unit002>().y = j;
                                        TempUnit.GetComponent<Unit002>().Status = 0;
                                        break;
                                    case "003":
                                        TempUnit.AddComponent<Unit003>();
                                        TempUnit.GetComponent<Unit003>().Order = 1;
                                        TempUnit.GetComponent<Unit003>().x = 6;
                                        TempUnit.GetComponent<Unit003>().y = j;
                                        TempUnit.GetComponent<Unit003>().Status = 0;
                                        break;
                                    case "004":
                                        TempUnit.AddComponent<Unit004>();
                                        TempUnit.GetComponent<Unit004>().Order = 1;
                                        TempUnit.GetComponent<Unit004>().x = 6;
                                        TempUnit.GetComponent<Unit004>().y = j;
                                        TempUnit.GetComponent<Unit004>().Status = 0;
                                        break;
                                    case "005":
                                        TempUnit.AddComponent<Unit005>();
                                        TempUnit.GetComponent<Unit005>().Order = 1;
                                        TempUnit.GetComponent<Unit005>().x = 6;
                                        TempUnit.GetComponent<Unit005>().y = j;
                                        TempUnit.GetComponent<Unit005>().Status = 0;
                                        break;
                                    case "006":
                                        TempUnit.AddComponent<Unit006>();
                                        TempUnit.GetComponent<Unit006>().Order = 1;
                                        TempUnit.GetComponent<Unit006>().x = 6;
                                        TempUnit.GetComponent<Unit006>().y = j;
                                        TempUnit.GetComponent<Unit006>().Status = 0;
                                        break;
                                    case "007":
                                        TempUnit.AddComponent<Unit007>();
                                        TempUnit.GetComponent<Unit007>().Order = 1;
                                        TempUnit.GetComponent<Unit007>().x = 6;
                                        TempUnit.GetComponent<Unit007>().y = j;
                                        TempUnit.GetComponent<Unit007>().Status = 0;
                                        break;
                                    case "008":
                                        TempUnit.AddComponent<Unit008>();
                                        TempUnit.GetComponent<Unit008>().Order = 1;
                                        TempUnit.GetComponent<Unit008>().x = 6;
                                        TempUnit.GetComponent<Unit008>().y = j;
                                        TempUnit.GetComponent<Unit008>().Status = 0;
                                        break;
                                    case "009":
                                        TempUnit.AddComponent<Unit009>();
                                        TempUnit.GetComponent<Unit009>().Order = 1;
                                        TempUnit.GetComponent<Unit009>().x = 6;
                                        TempUnit.GetComponent<Unit009>().y = j;
                                        TempUnit.GetComponent<Unit009>().Status = 0;
                                        break;
                                    case "010":
                                        TempUnit.AddComponent<Unit010>();
                                        TempUnit.GetComponent<Unit010>().Order = 1;
                                        TempUnit.GetComponent<Unit010>().x = 6;
                                        TempUnit.GetComponent<Unit010>().y = j;
                                        TempUnit.GetComponent<Unit010>().Status = 0;
                                        break;
                                    case "011":
                                        TempUnit.AddComponent<Unit011>();
                                        TempUnit.GetComponent<Unit011>().Order = 1;
                                        TempUnit.GetComponent<Unit011>().x = 6;
                                        TempUnit.GetComponent<Unit011>().y = j;
                                        TempUnit.GetComponent<Unit011>().Status = 0;
                                        break;
                                    case "100":
                                        TempUnit.AddComponent<Unit100>();
                                        TempUnit.GetComponent<Unit100>().Order = 1;
                                        TempUnit.GetComponent<Unit100>().x = 6;
                                        TempUnit.GetComponent<Unit100>().y = j;
                                        TempUnit.GetComponent<Unit100>().Status = 0;
                                        break;
                                    case "101":
                                        TempUnit.AddComponent<Unit101>();
                                        TempUnit.GetComponent<Unit101>().Order = 1;
                                        TempUnit.GetComponent<Unit101>().x = 6;
                                        TempUnit.GetComponent<Unit101>().y = j;
                                        TempUnit.GetComponent<Unit101>().Status = 0;
                                        break;
                                    case "102":
                                        TempUnit.AddComponent<Unit102>();
                                        TempUnit.GetComponent<Unit102>().Order = 1;
                                        TempUnit.GetComponent<Unit102>().x = 6;
                                        TempUnit.GetComponent<Unit102>().y = j;
                                        TempUnit.GetComponent<Unit102>().Status = 0;
                                        break;
                                    case "103":
                                        TempUnit.AddComponent<Unit103>();
                                        TempUnit.GetComponent<Unit103>().Order = 1;
                                        TempUnit.GetComponent<Unit103>().x = 6;
                                        TempUnit.GetComponent<Unit103>().y = j;
                                        TempUnit.GetComponent<Unit103>().Status = 0;
                                        break;
                                    case "104":
                                        TempUnit.AddComponent<Unit104>();
                                        TempUnit.GetComponent<Unit104>().Order = 1;
                                        TempUnit.GetComponent<Unit104>().x = 6;
                                        TempUnit.GetComponent<Unit104>().y = j;
                                        TempUnit.GetComponent<Unit104>().Status = 0;
                                        break;
                                    case "105":
                                        TempUnit.AddComponent<Unit105>();
                                        TempUnit.GetComponent<Unit105>().Order = 1;
                                        TempUnit.GetComponent<Unit105>().x = 6;
                                        TempUnit.GetComponent<Unit105>().y = j;
                                        TempUnit.GetComponent<Unit105>().Status = 0;
                                        break;
                                    case "200":
                                        TempUnit.AddComponent<Unit200>();
                                        TempUnit.GetComponent<Unit200>().Order = 1;
                                        TempUnit.GetComponent<Unit200>().x = 6;
                                        TempUnit.GetComponent<Unit200>().y = j;
                                        TempUnit.GetComponent<Unit200>().Status = 0;
                                        break;
                                    case "201":
                                        TempUnit.AddComponent<Unit201>();
                                        TempUnit.GetComponent<Unit201>().Order = 1;
                                        TempUnit.GetComponent<Unit201>().x = 6;
                                        TempUnit.GetComponent<Unit201>().y = j;
                                        TempUnit.GetComponent<Unit201>().Status = 0;
                                        break;
                                    case "202":
                                        TempUnit.AddComponent<Unit202>();
                                        TempUnit.GetComponent<Unit202>().Order = 1;
                                        TempUnit.GetComponent<Unit202>().x = 6;
                                        TempUnit.GetComponent<Unit202>().y = j;
                                        TempUnit.GetComponent<Unit202>().Status = 0;
                                        break;


                                }
                                TempUnit.GetComponent<RawImage>().texture = UnitImage;
                                TempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                break;
                            case 1:
                                Data.Instance.pan[5, j] = Data.Instance.pan[i, j];
                                TempUnit = GameObject.Find("" + 5 + "," + j);
                                UnitImage = Resources.Load("Image/UnitEnemy/" + Data.Instance.pan[5, j]) as Texture;
                                TempUnit.GetComponent<RawImage>().texture = UnitImage;
                                TempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                switch (Data.Instance.pan[5, j])
                                {
                                    case "000":
                                        TempUnit.AddComponent<Unit000>();
                                        TempUnit.GetComponent<Unit000>().Order = 1;
                                        TempUnit.GetComponent<Unit000>().x = 5;
                                        TempUnit.GetComponent<Unit000>().y = j;
                                        TempUnit.GetComponent<Unit000>().Status = 0;
                                        break;
                                    case "001":
                                        TempUnit.AddComponent<Unit001>();
                                        TempUnit.GetComponent<Unit001>().Order = 1;
                                        TempUnit.GetComponent<Unit001>().x = 5;
                                        TempUnit.GetComponent<Unit001>().y = j;
                                        TempUnit.GetComponent<Unit001>().Status = 0;
                                        break;
                                    case "002":
                                        TempUnit.AddComponent<Unit002>();
                                        TempUnit.GetComponent<Unit002>().Order = 1;
                                        TempUnit.GetComponent<Unit002>().x = 5;
                                        TempUnit.GetComponent<Unit002>().y = j;
                                        TempUnit.GetComponent<Unit002>().Status = 0;
                                        break;
                                    case "003":
                                        TempUnit.AddComponent<Unit003>();
                                        TempUnit.GetComponent<Unit003>().Order = 1;
                                        TempUnit.GetComponent<Unit003>().x = 5;
                                        TempUnit.GetComponent<Unit003>().y = j;
                                        TempUnit.GetComponent<Unit003>().Status = 0;
                                        break;
                                    case "004":
                                        TempUnit.AddComponent<Unit004>();
                                        TempUnit.GetComponent<Unit004>().Order = 1;
                                        TempUnit.GetComponent<Unit004>().x = 5;
                                        TempUnit.GetComponent<Unit004>().y = j;
                                        TempUnit.GetComponent<Unit004>().Status = 0;
                                        break;
                                    case "005":
                                        TempUnit.AddComponent<Unit005>();
                                        TempUnit.GetComponent<Unit005>().Order = 1;
                                        TempUnit.GetComponent<Unit005>().x = 5;
                                        TempUnit.GetComponent<Unit005>().y = j;
                                        TempUnit.GetComponent<Unit005>().Status = 0;
                                        break;
                                    case "006":
                                        TempUnit.AddComponent<Unit006>();
                                        TempUnit.GetComponent<Unit006>().Order = 1;
                                        TempUnit.GetComponent<Unit006>().x = 5;
                                        TempUnit.GetComponent<Unit006>().y = j;
                                        TempUnit.GetComponent<Unit006>().Status = 0;
                                        break;
                                    case "007":
                                        TempUnit.AddComponent<Unit007>();
                                        TempUnit.GetComponent<Unit007>().Order = 1;
                                        TempUnit.GetComponent<Unit007>().x = 5;
                                        TempUnit.GetComponent<Unit007>().y = j;
                                        TempUnit.GetComponent<Unit007>().Status = 0;
                                        break;
                                    case "008":
                                        TempUnit.AddComponent<Unit008>();
                                        TempUnit.GetComponent<Unit008>().Order = 1;
                                        TempUnit.GetComponent<Unit008>().x = 5;
                                        TempUnit.GetComponent<Unit008>().y = j;
                                        TempUnit.GetComponent<Unit008>().Status = 0;
                                        break;
                                    case "009":
                                        TempUnit.AddComponent<Unit009>();
                                        TempUnit.GetComponent<Unit009>().Order = 1;
                                        TempUnit.GetComponent<Unit009>().x = 5;
                                        TempUnit.GetComponent<Unit009>().y = j;
                                        TempUnit.GetComponent<Unit009>().Status = 0;
                                        break;
                                    case "010":
                                        TempUnit.AddComponent<Unit010>();
                                        TempUnit.GetComponent<Unit010>().Order = 1;
                                        TempUnit.GetComponent<Unit010>().x = 5;
                                        TempUnit.GetComponent<Unit010>().y = j;
                                        TempUnit.GetComponent<Unit010>().Status = 0;
                                        break;
                                    case "011":
                                        TempUnit.AddComponent<Unit011>();
                                        TempUnit.GetComponent<Unit011>().Order = 1;
                                        TempUnit.GetComponent<Unit011>().x = 5;
                                        TempUnit.GetComponent<Unit011>().y = j;
                                        TempUnit.GetComponent<Unit011>().Status = 0;
                                        break;
                                    case "100":
                                        TempUnit.AddComponent<Unit100>();
                                        TempUnit.GetComponent<Unit100>().Order = 1;
                                        TempUnit.GetComponent<Unit100>().x = 5;
                                        TempUnit.GetComponent<Unit100>().y = j;
                                        TempUnit.GetComponent<Unit100>().Status = 0;
                                        break;
                                    case "101":
                                        TempUnit.AddComponent<Unit101>();
                                        TempUnit.GetComponent<Unit101>().Order = 1;
                                        TempUnit.GetComponent<Unit101>().x = 5;
                                        TempUnit.GetComponent<Unit101>().y = j;
                                        TempUnit.GetComponent<Unit101>().Status = 0;
                                        break;
                                    case "102":
                                        TempUnit.AddComponent<Unit102>();
                                        TempUnit.GetComponent<Unit102>().Order = 1;
                                        TempUnit.GetComponent<Unit102>().x = 5;
                                        TempUnit.GetComponent<Unit102>().y = j;
                                        TempUnit.GetComponent<Unit102>().Status = 0;
                                        break;
                                    case "103":
                                        TempUnit.AddComponent<Unit103>();
                                        TempUnit.GetComponent<Unit103>().Order = 1;
                                        TempUnit.GetComponent<Unit103>().x = 5;
                                        TempUnit.GetComponent<Unit103>().y = j;
                                        TempUnit.GetComponent<Unit103>().Status = 0;
                                        break;
                                    case "104":
                                        TempUnit.AddComponent<Unit104>();
                                        TempUnit.GetComponent<Unit104>().Order = 1;
                                        TempUnit.GetComponent<Unit104>().x = 5;
                                        TempUnit.GetComponent<Unit104>().y = j;
                                        TempUnit.GetComponent<Unit104>().Status = 0;
                                        break;
                                    case "105":
                                        TempUnit.AddComponent<Unit105>();
                                        TempUnit.GetComponent<Unit105>().Order = 1;
                                        TempUnit.GetComponent<Unit105>().x = 5;
                                        TempUnit.GetComponent<Unit105>().y = j;
                                        TempUnit.GetComponent<Unit105>().Status = 0;
                                        break;
                                    case "200":
                                        TempUnit.AddComponent<Unit200>();
                                        TempUnit.GetComponent<Unit200>().Order = 1;
                                        TempUnit.GetComponent<Unit200>().x = 5;
                                        TempUnit.GetComponent<Unit200>().y = j;
                                        TempUnit.GetComponent<Unit200>().Status = 0;
                                        break;
                                    case "201":
                                        TempUnit.AddComponent<Unit201>();
                                        TempUnit.GetComponent<Unit201>().Order = 1;
                                        TempUnit.GetComponent<Unit201>().x = 5;
                                        TempUnit.GetComponent<Unit201>().y = j;
                                        TempUnit.GetComponent<Unit201>().Status = 0;
                                        break;
                                    case "202":
                                        TempUnit.AddComponent<Unit202>();
                                        TempUnit.GetComponent<Unit202>().Order = 1;
                                        TempUnit.GetComponent<Unit202>().x = 5;
                                        TempUnit.GetComponent<Unit202>().y = j;
                                        TempUnit.GetComponent<Unit202>().Status = 0;
                                        break;


                                }
                                break;
                            case 2:
                                Data.Instance.pan[4, j] = Data.Instance.pan[i, j];
                                TempUnit = GameObject.Find("" + 4 + "," + j);
                                UnitImage = Resources.Load("Image/UnitEnemy/" + Data.Instance.pan[4, j]) as Texture;
                                TempUnit.GetComponent<RawImage>().texture = UnitImage;
                                switch (Data.Instance.pan[4, j])
                                {
                                    case "000":
                                        TempUnit.AddComponent<Unit000>();
                                        TempUnit.GetComponent<Unit000>().Order = 1;
                                        TempUnit.GetComponent<Unit000>().x = 4;
                                        TempUnit.GetComponent<Unit000>().y = j;
                                        TempUnit.GetComponent<Unit000>().Status = 0;
                                        break;
                                    case "001":
                                        TempUnit.AddComponent<Unit001>();
                                        TempUnit.GetComponent<Unit001>().Order = 1;
                                        TempUnit.GetComponent<Unit001>().x = 4;
                                        TempUnit.GetComponent<Unit001>().y = j;
                                        TempUnit.GetComponent<Unit001>().Status = 0;
                                        break;
                                    case "002":
                                        TempUnit.AddComponent<Unit002>();
                                        TempUnit.GetComponent<Unit002>().Order = 1;
                                        TempUnit.GetComponent<Unit002>().x = 4;
                                        TempUnit.GetComponent<Unit002>().y = j;
                                        TempUnit.GetComponent<Unit002>().Status = 0;
                                        break;
                                    case "003":
                                        TempUnit.AddComponent<Unit003>();
                                        TempUnit.GetComponent<Unit003>().Order = 1;
                                        TempUnit.GetComponent<Unit003>().x = 4;
                                        TempUnit.GetComponent<Unit003>().y = j;
                                        TempUnit.GetComponent<Unit003>().Status = 0;
                                        break;
                                    case "004":
                                        TempUnit.AddComponent<Unit004>();
                                        TempUnit.GetComponent<Unit004>().Order = 1;
                                        TempUnit.GetComponent<Unit004>().x = 4;
                                        TempUnit.GetComponent<Unit004>().y = j;
                                        TempUnit.GetComponent<Unit004>().Status = 0;
                                        break;
                                    case "005":
                                        TempUnit.AddComponent<Unit005>();
                                        TempUnit.GetComponent<Unit005>().Order = 1;
                                        TempUnit.GetComponent<Unit005>().x = 4;
                                        TempUnit.GetComponent<Unit005>().y = j;
                                        TempUnit.GetComponent<Unit005>().Status = 0;
                                        break;
                                    case "006":
                                        TempUnit.AddComponent<Unit006>();
                                        TempUnit.GetComponent<Unit006>().Order = 1;
                                        TempUnit.GetComponent<Unit006>().x = 4;
                                        TempUnit.GetComponent<Unit006>().y = j;
                                        TempUnit.GetComponent<Unit006>().Status = 0;
                                        break;
                                    case "007":
                                        TempUnit.AddComponent<Unit007>();
                                        TempUnit.GetComponent<Unit007>().Order = 1;
                                        TempUnit.GetComponent<Unit007>().x = 4;
                                        TempUnit.GetComponent<Unit007>().y = j;
                                        TempUnit.GetComponent<Unit007>().Status = 0;
                                        break;
                                    case "008":
                                        TempUnit.AddComponent<Unit008>();
                                        TempUnit.GetComponent<Unit008>().Order = 1;
                                        TempUnit.GetComponent<Unit008>().x = 4;
                                        TempUnit.GetComponent<Unit008>().y = j;
                                        TempUnit.GetComponent<Unit008>().Status = 0;
                                        break;
                                    case "009":
                                        TempUnit.AddComponent<Unit009>();
                                        TempUnit.GetComponent<Unit009>().Order = 1;
                                        TempUnit.GetComponent<Unit009>().x = 4;
                                        TempUnit.GetComponent<Unit009>().y = j;
                                        TempUnit.GetComponent<Unit009>().Status = 0;
                                        break;
                                    case "010":
                                        TempUnit.AddComponent<Unit010>();
                                        TempUnit.GetComponent<Unit010>().Order = 1;
                                        TempUnit.GetComponent<Unit010>().x = 4;
                                        TempUnit.GetComponent<Unit010>().y = j;
                                        TempUnit.GetComponent<Unit010>().Status = 0;
                                        break;
                                    case "011":
                                        TempUnit.AddComponent<Unit011>();
                                        TempUnit.GetComponent<Unit011>().Order = 1;
                                        TempUnit.GetComponent<Unit011>().x = 4;
                                        TempUnit.GetComponent<Unit011>().y = j;
                                        TempUnit.GetComponent<Unit011>().Status = 0;
                                        break;
                                    case "100":
                                        TempUnit.AddComponent<Unit100>();
                                        TempUnit.GetComponent<Unit100>().Order = 1;
                                        TempUnit.GetComponent<Unit100>().x = 4;
                                        TempUnit.GetComponent<Unit100>().y = j;
                                        TempUnit.GetComponent<Unit100>().Status = 0;
                                        break;
                                    case "101":
                                        TempUnit.AddComponent<Unit101>();
                                        TempUnit.GetComponent<Unit101>().Order = 1;
                                        TempUnit.GetComponent<Unit101>().x = 4;
                                        TempUnit.GetComponent<Unit101>().y = j;
                                        TempUnit.GetComponent<Unit101>().Status = 0;
                                        break;
                                    case "102":
                                        TempUnit.AddComponent<Unit102>();
                                        TempUnit.GetComponent<Unit102>().Order = 1;
                                        TempUnit.GetComponent<Unit102>().x = 4;
                                        TempUnit.GetComponent<Unit102>().y = j;
                                        TempUnit.GetComponent<Unit102>().Status = 0;
                                        break;
                                    case "103":
                                        TempUnit.AddComponent<Unit103>();
                                        TempUnit.GetComponent<Unit103>().Order = 1;
                                        TempUnit.GetComponent<Unit103>().x = 4;
                                        TempUnit.GetComponent<Unit103>().y = j;
                                        TempUnit.GetComponent<Unit103>().Status = 0;
                                        break;
                                    case "104":
                                        TempUnit.AddComponent<Unit104>();
                                        TempUnit.GetComponent<Unit104>().Order = 1;
                                        TempUnit.GetComponent<Unit104>().x = 4;
                                        TempUnit.GetComponent<Unit104>().y = j;
                                        TempUnit.GetComponent<Unit104>().Status = 0;
                                        break;
                                    case "105":
                                        TempUnit.AddComponent<Unit105>();
                                        TempUnit.GetComponent<Unit105>().Order = 1;
                                        TempUnit.GetComponent<Unit105>().x = 4;
                                        TempUnit.GetComponent<Unit105>().y = j;
                                        TempUnit.GetComponent<Unit105>().Status = 0;
                                        break;
                                    case "200":
                                        TempUnit.AddComponent<Unit200>();
                                        TempUnit.GetComponent<Unit200>().Order = 1;
                                        TempUnit.GetComponent<Unit200>().x = 4;
                                        TempUnit.GetComponent<Unit200>().y = j;
                                        TempUnit.GetComponent<Unit200>().Status = 0;
                                        break;
                                    case "201":
                                        TempUnit.AddComponent<Unit201>();
                                        TempUnit.GetComponent<Unit201>().Order = 1;
                                        TempUnit.GetComponent<Unit201>().x = 4;
                                        TempUnit.GetComponent<Unit201>().y = j;
                                        TempUnit.GetComponent<Unit201>().Status = 0;
                                        break;
                                    case "202":
                                        TempUnit.AddComponent<Unit202>();
                                        TempUnit.GetComponent<Unit202>().Order = 1;
                                        TempUnit.GetComponent<Unit202>().x = 4;
                                        TempUnit.GetComponent<Unit202>().y = j;
                                        TempUnit.GetComponent<Unit202>().Status = 0;
                                        break;


                                }
                                TempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                break;
                        }
                    }
                }

            }

        }
    }


}