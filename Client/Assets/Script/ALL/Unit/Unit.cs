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

       
        public virtual void Move()
        {
            // 일반 이동
            Debug.Log("이동~");
            if(bmove)Data.Instance.Move(gameObject, GameObject.Find(moveX + "," + moveY), moveX, moveY, x, y);
        }

        public virtual void Attack()
        {
            Debug.Log("어택~");
            GameObject target = GameObject.Find(moveX + "," + moveY);
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap))
            {
                //디스트로이 = 데이터를 삭제하고 단순이동
                RemoveUnit(target);
                if (hp <= 0) RemoveUnit(gameObject);//반격을 당하여 hp가 0이될경우
                else Data.Instance.Move(gameObject, target, moveX, moveY, x, y); // 일반 이동

            }
            else
            {
                //넉백확인후 이동(넉백함수)
                if (hp <= 0) RemoveUnit(gameObject);//반격을 당하여 hp가 0이될경우
                else Data.Instance.KnockBack(gameObject, target, moveX, moveY, x, y, moveDirection); // 넉백
            }
        }

        public void RemoveUnit(GameObject target)
        {
            Destroy(target.GetComponent<Move>());

            // Unit을 List에서 제거
            Data.Instance.board[moveY, moveX] = Data.Instance.Empty;// 보드에서 제거
            Destroy(target.GetComponent<Unit>());//스크립트 제거
            target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0); // Image 투명화
        
        }

        public virtual bool Defence(int enemyAp)
        {
            // 방어할 때
            // true -> Destroy, false -> Damage
            Debug.Log("디펜스~");
            if (enemyAp - dp > 0) hp -= enemyAp - dp;

            return hp <= 0;
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
