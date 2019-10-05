
using UnityEngine;
using MonsterChessClient;

namespace UnitType
{
    public class Unit006 : Unit
    {
        protected override void Awake()
        {
            ID = "006";
            Distence = 2;
            Direction = 1;
            Cost = 1;
            fullHp = 4;
            hp = 4;
            ap = 2;

            base.Awake();
        }
        
        public override void Attack(int playCount)
        {
            Debug.Log("어택~");
            GameObject target = GameObject.Find(moveX + "," + moveY);
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap, hp))
            {
               
                //디스트로이 = 데이터를 삭제하고 단순이동
                
                RemoveUnit(target, playCount);
                if (Data.Instance.board[moveX + 1, moveY] != null)
                {
                    GameObject.Find((moveX + 1) + "," + moveY).GetComponent<Unit>().hp--;
                    if(GameObject.Find((moveX + 1) + "," + moveY).GetComponent<Unit>().hp<=0)
                        RemoveUnit(GameObject.Find((moveX + 1) + "," + moveY), playCount);
                }
                if (Data.Instance.board[moveX - 1, moveY] != null)
                {
                    GameObject.Find((moveX - 1) + "," + moveY).GetComponent<Unit>().hp--;
                    if (GameObject.Find((moveX - 1) + "," + moveY).GetComponent<Unit>().hp <= 0)
                        RemoveUnit(GameObject.Find((moveX - 1) + "," + moveY), playCount);
                }
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
    }
}