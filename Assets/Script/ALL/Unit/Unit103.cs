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
            fullHp = 3;
            hp = fullHp;
            ap = 2;
            dp = 1;

            base.Awake();
        }

       
    }
}