﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using System.Reflection;

namespace MonsterChessClient
{
   
    public class BoardButtonOfPlay : MonoBehaviour
    {
        // Use this for initialization
        public void OnBoard()
        {   
            if (GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present == SceneList.Play)
            {
                //플레이 에서의 기능
                if (Data.Instance.SommonOn == true && Data.Instance.Time <= 30)
                {
                    int x = int.Parse(gameObject.name.Substring(2));
                    int y = int.Parse(gameObject.name.Substring(0, 1));
                    int cost = int.Parse(Data.Instance.FindStateOfMonster(Data.Instance.SommonID).Substring(6,1));
                    if (Data.Instance.Mana - cost >= 0)
                    {
                        if (Data.Instance.pan[y, x] == null)
                        {
                            //소환은 즉각 적용 시키고 플레이 리스트에 제외 시킨다
                            x = int.Parse(gameObject.name.Substring(2));
                            y = int.Parse(gameObject.name.Substring(0, 1));
                            Texture UnitImage = Resources.Load("Image/UnitMy/" + Data.Instance.SommonID) as Texture;
                            gameObject.GetComponent<RawImage>().texture = UnitImage;
                            gameObject.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                            Data.Instance.pan[y, x] = Data.Instance.SommonID;
                            Data.Instance.SommonOn = false;
                            Data.Instance.Mana -= cost;
                            switch (Data.Instance.SommonID)
                            {
                                case "000":
                                    gameObject.AddComponent<Unit000>();
                                    gameObject.GetComponent<Unit000>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit000>().x = x;
                                    gameObject.GetComponent<Unit000>().y = y;
                                    gameObject.GetComponent<Unit000>().Status = 2;
                                    break;
                                case "001":
                                    gameObject.AddComponent<Unit001>();
                                    gameObject.GetComponent<Unit001>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit001>().x = x;
                                    gameObject.GetComponent<Unit001>().y = y;
                                    gameObject.GetComponent<Unit001>().Status = 2;
                                    break;
                                case "002":
                                    gameObject.AddComponent<Unit002>();
                                    gameObject.GetComponent<Unit002>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit002>().x = x;
                                    gameObject.GetComponent<Unit002>().y = y;
                                    gameObject.GetComponent<Unit002>().Status = 2;
                                    break;
                                case "003":
                                    gameObject.AddComponent<Unit003>();
                                    gameObject.GetComponent<Unit003>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit003>().x = x;
                                    gameObject.GetComponent<Unit003>().y = y;
                                    gameObject.GetComponent<Unit003>().Status = 2;
                                    break;
                                case "004":
                                    gameObject.AddComponent<Unit004>();
                                    gameObject.GetComponent<Unit004>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit004>().x = x;
                                    gameObject.GetComponent<Unit004>().y = y;
                                    gameObject.GetComponent<Unit004>().Status = 2;
                                    break;
                                case "005":
                                    gameObject.AddComponent<Unit005>();
                                    gameObject.GetComponent<Unit005>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit005>().x = x;
                                    gameObject.GetComponent<Unit005>().y = y;
                                    gameObject.GetComponent<Unit005>().Status = 2;
                                    break;
                                case "006":
                                    gameObject.AddComponent<Unit006>();
                                    gameObject.GetComponent<Unit006>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit006>().x = x;
                                    gameObject.GetComponent<Unit006>().y = y;
                                    gameObject.GetComponent<Unit006>().Status = 2;
                                    break;
                                case "007":
                                    gameObject.AddComponent<Unit007>();
                                    gameObject.GetComponent<Unit007>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit007>().x = x;
                                    gameObject.GetComponent<Unit007>().y = y;
                                    gameObject.GetComponent<Unit007>().Status = 2;
                                    break;
                                case "008":
                                    gameObject.AddComponent<Unit008>();
                                    gameObject.GetComponent<Unit008>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit008>().x = x;
                                    gameObject.GetComponent<Unit008>().y = y;
                                    gameObject.GetComponent<Unit008>().Status = 2;
                                    break;
                                case "009":
                                    gameObject.AddComponent<Unit009>();
                                    gameObject.GetComponent<Unit009>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit009>().x = x;
                                    gameObject.GetComponent<Unit009>().y = y;
                                    gameObject.GetComponent<Unit009>().Status = 2;
                                    break;
                                case "010":
                                    gameObject.AddComponent<Unit010>();
                                    gameObject.GetComponent<Unit010>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit010>().x = x;
                                    gameObject.GetComponent<Unit010>().y = y;
                                    gameObject.GetComponent<Unit010>().Status = 2;
                                    break;
                                case "011":
                                    gameObject.AddComponent<Unit011>();
                                    gameObject.GetComponent<Unit011>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit011>().x = x;
                                    gameObject.GetComponent<Unit011>().y = y;
                                    gameObject.GetComponent<Unit011>().Status = 2;
                                    break;
                                case "100":
                                    gameObject.AddComponent<Unit100>();
                                    gameObject.GetComponent<Unit100>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit100>().x = x;
                                    gameObject.GetComponent<Unit100>().y = y;
                                    gameObject.GetComponent<Unit100>().Status = 2;
                                    break;
                                case "101":
                                    gameObject.AddComponent<Unit101>();
                                    gameObject.GetComponent<Unit101>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit101>().x = x;
                                    gameObject.GetComponent<Unit101>().y = y;
                                    gameObject.GetComponent<Unit101>().Status = 2;
                                    break;
                                case "102":
                                    gameObject.AddComponent<Unit102>();
                                    gameObject.GetComponent<Unit102>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit102>().x = x;
                                    gameObject.GetComponent<Unit102>().y = y;
                                    gameObject.GetComponent<Unit102>().Status = 2;
                                    break;
                                case "103":
                                    gameObject.AddComponent<Unit103>();
                                    gameObject.GetComponent<Unit103>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit103>().x = x;
                                    gameObject.GetComponent<Unit103>().y = y;
                                    gameObject.GetComponent<Unit103>().Status = 2;
                                    break;
                                case "104":
                                    gameObject.AddComponent<Unit104>();
                                    gameObject.GetComponent<Unit104>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit104>().x = x;
                                    gameObject.GetComponent<Unit104>().y = y;
                                    gameObject.GetComponent<Unit104>().Status = 2;
                                    break;
                                case "105":
                                    gameObject.AddComponent<Unit105>();
                                    gameObject.GetComponent<Unit105>().Order = Data.Instance.Order;
                                    gameObject.GetComponent<Unit105>().x = x;
                                    gameObject.GetComponent<Unit105>().y = y;
                                    gameObject.GetComponent<Unit105>().Status = 2;
                                    break;

                            }
                   

                        }
                        else
                        {
                            Debug.Log("배치할려는 칸에 유닛존재");
                        }
                    }
                    else
                    {
                        Debug.Log("마나부족");
                    }
                }
                else if (Data.Instance.MoveOn == false && Data.Instance.Time <= 30)
                {
                    //터치한 유닛의 이동 범위를 가져옴
                    //터치한 유닛의 이동 범위를 가져옴
                    int x = int.Parse(gameObject.name.Substring(2));
                    int y = int.Parse(gameObject.name.Substring(0, 1));
                    switch (Data.Instance.pan[y, x])
                    {
                        case "000":
                            gameObject.GetComponent<Unit000>().MoveRange();
                            break;
                        case "001":
                            gameObject.GetComponent<Unit001>().MoveRange();
                            break;
                        case "002":
                            gameObject.GetComponent<Unit002>().MoveRange();
                            break;
                        case "003":
                            gameObject.GetComponent<Unit003>().MoveRange();
                            break;
                        case "004":
                            gameObject.GetComponent<Unit004>().MoveRange();
                            break;
                        case "005":
                            gameObject.GetComponent<Unit005>().MoveRange();
                            break;
                        case "006":
                            gameObject.GetComponent<Unit006>().MoveRange();
                            break;
                        case "007":
                            gameObject.GetComponent<Unit007>().MoveRange();
                            break;
                        case "008":
                            gameObject.GetComponent<Unit008>().MoveRange();
                            break;
                        case "009":
                            gameObject.GetComponent<Unit009>().MoveRange();
                            break;
                        case "010":
                            gameObject.GetComponent<Unit010>().MoveRange();
                            break;
                        case "011":
                            gameObject.GetComponent<Unit011>().MoveRange();
                            break;
                        case "100":
                            gameObject.GetComponent<Unit100>().MoveRange();
                            break;
                        case "101":
                            gameObject.GetComponent<Unit101>().MoveRange();
                            break;
                        case "102":
                            gameObject.GetComponent<Unit102>().MoveRange();
                            break;
                        case "103":
                            gameObject.GetComponent<Unit103>().MoveRange();
                            break;
                        case "104":
                            gameObject.GetComponent<Unit104>().MoveRange();
                            break;
                        case "105":
                            gameObject.GetComponent<Unit105>().MoveRange();
                            break;
                        case "200":
                            gameObject.GetComponent<Unit200>().MoveRange();
                            break;
                        case "201":
                            gameObject.GetComponent<Unit201>().MoveRange();
                            break;
                        case "202":
                            gameObject.GetComponent<Unit202>().MoveRange();
                            break;
                    }
                    Data.Instance.Origin = gameObject;
                    Data.Instance.MoveOn = true;
                }
                else
                {
                    int MoveX = int.Parse(gameObject.name.Substring(2));
                    int MoveY = int.Parse(gameObject.name.Substring(0, 1));
                    //이동범위 클릭시 무브리스트 변경
                    int x = int.Parse(Data.Instance.Origin.name.Substring(2));
                    int y = int.Parse(Data.Instance.Origin.name.Substring(0, 1));
                    switch (Data.Instance.pan[y, x])
                    {
                        case "000":
                            Data.Instance.Origin.GetComponent<Unit000>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit000>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit000>().SaveMove();
                            
                            break;
                        case "001":
                            Data.Instance.Origin.GetComponent<Unit001>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit001>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit001>().SaveMove();
                            
                            break;
                        case "002":
                            Data.Instance.Origin.GetComponent<Unit002>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit002>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit002>().SaveMove();
                            
                            break;
                        case "003":
                            Data.Instance.Origin.GetComponent<Unit003>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit003>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit003>().SaveMove();
                   
                            break;
                        case "004":
                            Data.Instance.Origin.GetComponent<Unit004>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit004>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit004>().SaveMove();
                          
                            break;
                        case "005":
                            Data.Instance.Origin.GetComponent<Unit005>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit005>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit005>().SaveMove();
                          
                            break;
                        case "006":
                            Data.Instance.Origin.GetComponent<Unit006>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit006>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit006>().SaveMove();
                     
                            break;
                        case "007":
                            Data.Instance.Origin.GetComponent<Unit007>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit007>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit007>().SaveMove();
                  
                            break;
                        case "008":
                            Data.Instance.Origin.GetComponent<Unit008>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit008>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit008>().SaveMove();
                   
                            break;
                        case "009":
                            Data.Instance.Origin.GetComponent<Unit009>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit009>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit009>().SaveMove();
                      
                            break;
                        case "010":
                            Data.Instance.Origin.GetComponent<Unit010>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit010>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit010>().SaveMove();
                
                            break;
                        case "011":
                            Data.Instance.Origin.GetComponent<Unit011>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit011>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit011>().SaveMove();
                     
                            break;
                        case "100":
                            Data.Instance.Origin.GetComponent<Unit100>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit100>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit100>().SaveMove();
                    
                            break;
                        case "101":
                            Data.Instance.Origin.GetComponent<Unit101>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit101>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit101>().SaveMove();
               
                            break;
                        case "102":
                            Data.Instance.Origin.GetComponent<Unit102>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit102>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit102>().SaveMove();
          
                            break;
                        case "103":
                            Data.Instance.Origin.GetComponent<Unit103>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit103>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit103>().SaveMove();
            
                            break;
                        case "104":
                            Data.Instance.Origin.GetComponent<Unit104>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit104>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit104>().SaveMove();
               
                            break;
                        case "105":
                            Data.Instance.Origin.GetComponent<Unit105>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit105>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit105>().SaveMove();
                  
                            break;
                        case "200":
                            Data.Instance.Origin.GetComponent<Unit200>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit200>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit200>().SaveMove();
                   

                            break;
                        case "201":
                            Data.Instance.Origin.GetComponent<Unit201>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit201>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit201>().SaveMove();
                
                            break;
                        case "202":
                            Data.Instance.Origin.GetComponent<Unit202>().MoveX = MoveX;
                            Data.Instance.Origin.GetComponent<Unit202>().MoveY = MoveY;
                            Data.Instance.Origin.GetComponent<Unit202>().SaveMove();
                    
                            break;
                    }
                    Data.Instance.MoveOn = false;
                    
                }
            }
        }
        
       
       
     
      
        

    }
}