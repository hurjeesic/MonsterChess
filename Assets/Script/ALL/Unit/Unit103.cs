using UnityEngine;

namespace UnitType
{
    public class Unit103 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "103";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 3;
            fullHp = 2;
            ap = 2;

            base.Awake();
        }

        public override void Wait(int playCount)
        {
            GameObject target = GetTarget();
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap, hp)) RemoveUnit(target, playCount);
            SpreadAttack(target, playCount);
        }
        public void SpreadAttack(GameObject target, int playCount)
        {
            Unit unit = target.GetComponent<Unit>();
            int targetX = unit.x;
            int targetY = unit.y;
            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (i > -1 && i < 7 && j > -1 && j < 7)
                    {
                        GameObject tempTarget = GameObject.Find(i + "," + j);
                        Unit tempUnit = tempTarget.GetComponent<Unit>();
                        if (order != tempUnit.order)
                        {
                            tempUnit.hp--;
                            if (tempUnit.hp < 0) RemoveUnit(tempTarget, playCount);
                        }
                        
                    }
                }
            }
        }
    }
}