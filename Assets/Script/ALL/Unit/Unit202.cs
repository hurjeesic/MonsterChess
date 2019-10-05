using UnityEngine;
using MonsterChessClient;
using System.Collections.Generic;

namespace UnitType
{
    public class Unit202 : Unit
    {
        protected override void Awake()
        {
            ID = "202";
            Distence = 2;
            Direction = 1;
            Cost = 1;
            fullHp =5;
            ap = 4;

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
                Heal();
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
        public void Heal()
        {
            
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y +2; j++)
                {
                    if (i > -1 && i < 7 && j > -1 && j < 7)
                    {
                        GameObject target = GameObject.Find(i + "," + j);
                        Unit unit = target.GetComponent<Unit>();
                        if (unit != null && gameObject.GetComponent<Unit>().order == unit.order)unit.hp++ ;//오더 값이 같은 애면 저장
                    }

                }
            }
           
        }
    }
}