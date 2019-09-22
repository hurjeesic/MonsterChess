using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MonsterChessClient;
using UnityEngine.UI;
public class Unit103 : MonoBehaviour {

    public string ID = "103";
    public int Distence = 1;
    public int AttackDistence = 2;
    public int Direction = 1;
    public int cost = 3;
    public int FullHP = 2;
    public int AP = 2;

    public int x;
    public int y;
    public int Order;
    public int Status;
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
        Data.Instance.Move(gameObject, Target, MoveX, MoveY, x, y);//일반 이동
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
