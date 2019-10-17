using UnityEngine;
namespace UnitType
{
    public class Unit104 : RemoteUnit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "104";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 3;
            ap = 3;

            base.Awake();
        }
        public override void Wait(int playCount)
        {
            GameObject target = GetTarget();
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap, hp)) RemoveUnit(target, playCount);
            SpreadState(target, playCount);
        }
        public void SpreadState(GameObject target, int playCount)
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
                            tempUnit.stateCount = 3;
                            tempUnit.stateAp = 1;
                        }
                            
                    }
                }
            }
        }

    }
}