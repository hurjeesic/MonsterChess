using UnityEngine;
namespace UnitType
{
    public class Unit102 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "102";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 3;
            fullHp = 3;
            ap = 2;

            base.Awake();
        }

        public override void Wait(int playCount)
        {
            GameObject target = GetTarget();
            Unit unit = target.GetComponent<Unit>();
            if (unit.Defence(ap, hp)) RemoveUnit(target, playCount);
            else
            {
                unit.stateCount = 2;
                unit.stateAp = 1;
            }
        }
    }
}