using UnityEngine;
using MonsterChessClient;
namespace UnitType
{
    public class Unit201 : Unit
    {
        protected override void Awake()
        {
            ID = "201";
            Distence = 3;
            Direction = 1;
            Cost = 1;
            fullHp = 5;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {
            Debug.Log("어택~");
            GameObject target = GameObject.Find(moveX + "," + moveY);
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap, hp))
            {

                //디스트로이 = 데이터를 삭제하고 단순이
                RemoveUnit(target, playCount);
                ap++;
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