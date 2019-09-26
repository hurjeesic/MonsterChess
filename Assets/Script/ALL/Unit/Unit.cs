using MonsterChessClient;
using System.Collections.Generic;
using UnityEngine;

namespace UnitType
{
    public abstract class Unit : MonoBehaviour
    {
        public string ID { get; protected set; }
        public int Distence { get; protected set; }
        public int Direction { get; protected set; }
        public int Cost { get; protected set; }
        public int fullHp;
        public int ap;

        public int x, y;
        public int order;
        public int status;
        public int moveX, moveY;
        public int hp;
        public int moveDirection;
        public string enemyID;
        public List<KeyValuePair<int, GameObject>> range;

        protected virtual void Awake()
        {
            range = new List<KeyValuePair<int, GameObject>>();
        }

        public virtual void Move()
        {
            // 일반 이동
            Data.Instance.Move(gameObject, GameObject.Find(moveY + "," + moveX), moveX, moveY, x, y);
        }

        public abstract void Attack(int playCount);

        public virtual bool Defence(int enemyAp, int enemyHp)
        {
            // 방어할 때
            // true -> Destroy, false -> Damage
            hp -= enemyAp;

            return hp <= 0;
        }

        public virtual void MoveRange()
        {
            //이동범위 표시
            Data.Instance.GetMoveRange(x, y, Direction, Distence, range);
        }

        public virtual void SaveMove()
        {
            // 이동범위 안이면 저장, 밖이면 다시 선택
            Data.Instance.SaveMove(range, moveDirection, moveX, moveY, status);
        }
    }
}
