using MonsterChessClient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitType
{
    public class Unit009 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "009";
            Distence = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 4;
            hp = 4;
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
                MakeSkeleton();
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

        public override bool Defence(int enemyAp, int enemyHp)
        {
            return base.Defence(enemyAp, enemyHp);
        }

        private void MakeSkeleton()
        {

        }
    }
}