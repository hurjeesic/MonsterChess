using MonsterChessClient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitType
{
    public abstract class Unit : MonoBehaviour
    {
        public string ID { get; protected set; }
        public int Distence { get; protected set; }
        public int Direction { get; protected set; }
        public int Cost { get; protected set; }
        public int fullHp { get; protected set; }
        public int ap { get; protected set; }
        public int dp { get; protected set; }
       

        public int x, y;
        public int order;
        public int status;
        public int moveX, moveY;
        public int hp;
        public int moveDirection;
        public string enemyID;

        //상태이상 지속 데미지
        public int stateCount;
        public int stateAp;
        public bool bmove = true;//이동불가
        public List<KeyValuePair<int, GameObject>> range;

        protected virtual void Awake()
        {
            range = new List<KeyValuePair<int, GameObject>>();
        }

        public virtual void KnockBack(int X, int Y, int EnemyX, int EnemyY, int EnemyMoveX,int EnemyMoveY)
        {
            Data.Instance.KnockBack(X,Y,EnemyX, EnemyY, EnemyMoveX, EnemyMoveY);
        }
        public virtual void Move(int movex, int movey, int X, int Y)
        {
            // 일반 이동
            Debug.Log("이동~");
            if(bmove)Data.Instance.Move(gameObject, GameObject.Find(movex + "," + movey), movex, movey, X, Y);
        }

       

        public void RemoveUnit(GameObject target)
        {
            Debug.Log(target.name+" - 죽은놈");
            // Unit을 List에서 제거
            GameObject fire = Instantiate(Resources.Load("Prefab/fx_fire_ball_bb") as GameObject, target.transform);
            fire.transform.position = new Vector3(fire.transform.position.x - 0.5f, fire.transform.position.y - 0.5f, fire.transform.position.z);
            Destroy(fire, 1);
            Data.Instance.board[target.GetComponent<Unit>().x, target.GetComponent<Unit>().x] = Data.Instance.Empty;// 보드에서 제거
            DestroyImmediate(target.GetComponent<Unit>());//스크립트 제거
            target.GetComponent<RawImage>().texture = null;
            target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0); // Image 투명화
        }

      

        public virtual void MoveRange()
        {
            //이동범위 표시
            range.Clear();
            Data.Instance.GetMoveRange(x, y, Direction, Distence, range);
        }

        public virtual void SaveMove()
        {
            // 이동범위 안이면 저장, 밖이면 다시 선택
            Data.Instance.SaveMove(range, moveDirection, moveX, moveY, status);
        }

       
        //상태이상 공격, 턴이 끝날때 작동하게 해야함

       
    }
}
