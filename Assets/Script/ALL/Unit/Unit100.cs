using UnityEngine;
namespace UnitType
{
    public class Unit100 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "100";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 2;
            fullHp = 2;
            ap = 1;

            base.Awake();
        }
        bool battack = true; //특성상 존재
        int plusAp = 0;
        public override void Wait(int playCount)
        {
            GameObject target = GetTarget();
            Unit unit = target.GetComponent<Unit>();
            if (plusAp < 2) plusAp++;
            if (unit != null && battack && unit.Defence(ap + plusAp, hp))
            {
                RemoveUnit(target, playCount);
                battack = false;
            }
            else battack = true;
           
            
        }
        public override void Move()
        {
            plusAp = 0;
            base.Move();
        }

    }
}