using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using MonsterChessClient;

public class Unit000 : MonoBehaviour
{

    public string ID = "000";
    public int Distence = 1;
    public int Direction = 0;
    public int cost = 1;
    public int FullHP = 2;
    public int AP = 1;

    public int x;
    public int y;
    public int Order;
    public int Status=0;
    public int MoveX;
    public int MoveY;
    public int HP;
    public int MoveDirection;
    public string EnemyID;
    public List<GameObject> Range = new List<GameObject>();
    public List<int> TempMoveDirection = new List<int>();

    public void Move()
    {
        GameObject Target = GameObject.Find(MoveY + "," + MoveX);
        Data.Instance.Move(gameObject,Target,MoveX,MoveY,x,y);//일반 이동
    }

    public void Attack(int PlayCount)
    {
        GameObject Target = GameObject.Find(MoveY + "," + MoveX);
        string EenemyID = Data.Instance.pan[MoveY, MoveX];
        switch (EnemyID)
        {
            case "000":
                if (Target.GetComponent<Unit000>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit000>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target,MoveX,MoveY,x,y,MoveDirection);
                }
                break;
            case "001":
                if (Target.GetComponent<Unit001>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit001>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "002":
                if (Target.GetComponent<Unit002>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit002>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "003":
                if (Target.GetComponent<Unit003>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit003>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "004":
                if (Target.GetComponent<Unit004>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit004>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "005":
                if (Target.GetComponent<Unit005>().Defence(AP,HP))
                {
                    if (HP == 0) { }
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit005>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "006":
                if (Target.GetComponent<Unit006>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit006>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "007":
                if (Target.GetComponent<Unit007>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit007>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "008":
                if (Target.GetComponent<Unit008>().Defence(AP,HP))
                {
                    if (HP <= 0) { }
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit008>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "009":
                if (Target.GetComponent<Unit009>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit009>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "010":
                if (Target.GetComponent<Unit010>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit010>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "011":
                if (Target.GetComponent<Unit011>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit011>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "100":
                if (Target.GetComponent<Unit100>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit100>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "101":
                if (Target.GetComponent<Unit101>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit101>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "102":
                if (Target.GetComponent<Unit102>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit102>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "103":
                if (Target.GetComponent<Unit103>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit103>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "104":
                if (Target.GetComponent<Unit104>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit104>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "105":
                if (Target.GetComponent<Unit105>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit105>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "200":
                if (Target.GetComponent<Unit200>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit200>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "201":
                if (Target.GetComponent<Unit201>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit201>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;
            case "202":
                if (Target.GetComponent<Unit202>().Defence(AP))
                {
                    //destroy된경우
                    if (HP == 1) { HP = 2; }
                    Destroy(Target.GetComponent<Unit202>());
                    Destroy(Target.GetComponent<Move>());
                    for (int i = 0; i < Data.Instance.PlayList.Count; i++)
                    {
                        if (Target = Data.Instance.PlayList[i])
                        {
                            if (PlayCount > i)
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                                PlayCount--;
                            }
                            else
                            {
                                Data.Instance.PlayList.RemoveAt(i);
                            }
                        }
                    }//플레이 리스트 제거
                    Target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);//이미지 투명화
                    Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
                }
                else
                {
                    Data.Instance.KnockBack(gameObject, Target, MoveX, MoveY, x, y, MoveDirection);
                }
                break;

        }
    }
    public bool Defence(int EnemyAP)
    {
        //방어할때
        // true= 디스트로이 false= 데미지
        HP -= EnemyAP;
        if (HP <= 0)
        {
            return true;
        }
       return false;
        
    }
    public void MoveRange()
    {
        //이동범위 표시
        Data.Instance.MoveRange(x, y, Direction, Distence, Range, TempMoveDirection);
    }
    public void SaveMove()
    {
        //이동범위 내 이면 저장
        //이동범위 밖이면 다시선택하게함
        Data.Instance.SaveMove(Range, MoveDirection, MoveX, MoveY, Status);
        
    }
}


