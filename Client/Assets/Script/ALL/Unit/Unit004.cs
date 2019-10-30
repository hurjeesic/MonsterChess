using UnityEngine;
namespace UnitType
{
    public class Unit004 : RemoteUnit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "004";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 2;
            hp = fullHp;
            ap = 4;
            dp = 1;

            base.Awake();
        }

    }
}