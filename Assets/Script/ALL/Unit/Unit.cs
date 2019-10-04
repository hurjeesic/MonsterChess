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

        public virtual void Wait()
        {
            //
        }
        public virtual void Move()
        {
            // 일반 이동
            Debug.Log("이동~");
            Data.Instance.Move(gameObject, GameObject.Find(moveX + "," + moveY), moveX, moveY, x, y);
        }

        public virtual void Attack(int playCount)
        {
            Debug.Log("어택~");
            GameObject target = GameObject.Find(moveX + "," + moveY);
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap, hp))
            {
                //디스트로이 = 데이터를 삭제하고 단순이
                RemoveUnit(target, playCount);
                if (hp <= 0) RemoveUnit(gameObject, playCount);//반격을 당하여 hp가 0이될경우
                else Data.Instance.Move(gameObject, target, moveX, moveY, x, y); // 일반 이동

            }
            else
            {
                //넉백확인후 이동(넉백함수)
                if (hp <= 0) RemoveUnit(gameObject, playCount);//반격을 당하여 hp가 0이될경우
                else Data.Instance.KnockBack(gameObject, target, moveX, moveY, x, y, moveDirection); // 넉백
            }
        }

        public void RemoveUnit(GameObject target, int playCount)
        {
            Destroy(target.GetComponent<Move>());

            // Unit을 List에서 제거
            for (int i = 0; i < Data.Instance.playList.Count; i++)
            {
                if (target == Data.Instance.playList[i])
                {
                    Data.Instance.playList.RemoveAt(i);
                    if (playCount >= i)//자신을 지울수 있음
                    {
                        playCount--;
                    }
                }
            }

            Data.Instance.board[moveY, moveX] = null;// 보드에서 제거
            Destroy(target.GetComponent<Unit>());//스크립트 제거
            target.GetComponent<RawImage>().color = new Color(255, 255, 255, 0); // Image 투명화
        
        }

        public virtual bool Defence(int enemyAp, int enemyHp)
        {
            // 방어할 때
            // true -> Destroy, false -> Damage
            Debug.Log("디펜스~");
            hp -= enemyAp;

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
       
    }
}
