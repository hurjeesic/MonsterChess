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
        public int attackDistance { get; protected set; }
       

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
            gameObject.AddComponent<Move>();
        }

        public virtual void Wait(int playCount)
        {
            if (ID.Substring(0, 1) == "1")//원거리 일경우
            {
                GameObject target = GetTarget();
                Unit unit = target.GetComponent<Unit>();
                if (unit.Defence(ap)) RemoveUnit(target, playCount);

            }
        }
        public virtual void Move()
        {
            // 일반 이동
            Debug.Log("이동~");
            if(bmove)Data.Instance.Move(gameObject, GameObject.Find(moveX + "," + moveY), moveX, moveY, x, y);
        }

        public virtual void Attack(int playCount)
        {
            Debug.Log("어택~");
            GameObject target = GameObject.Find(moveX + "," + moveY);
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap))
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

        public void HaveState(int playCount)
        {
            if (stateCount > 0) hp -= stateAp;
            if (hp > 0) RemoveUnit(gameObject, playCount);
        }
        //상태이상 공격, 턴이 끝날때 작동하게 해야함

        public GameObject GetTarget()
        {
            List<GameObject> targets = new List<GameObject>();
            targets.Clear();
            for (int i = x - attackDistance; i < x + attackDistance + 1; i++)
            {
                for (int j = y - attackDistance; j < y - attackDistance + 1; j++)
                {
                    if (i > -1 && i < 7 && j > -1 && j < 7)
                    {
                        GameObject target = GameObject.Find(i + "," + j);
                        Unit unit = target.GetComponent<Unit>();
                        if (unit != null && gameObject.GetComponent<Unit>().order != unit.order)
                        {
                            targets.Add(target);//오더 값이 다른 애면 저장
                        }
                    }
                }
            }
            //저장한 리스트에서 한놈만 가져옴
            int tempNum = Random.Range(0, targets.Count-1);
            return targets[tempNum];

        }
    }
}
