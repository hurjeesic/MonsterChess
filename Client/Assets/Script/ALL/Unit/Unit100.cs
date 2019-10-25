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
            fullHp = 1;
            hp = fullHp;
            ap = 2;
            dp = 1;

            base.Awake();
        }

    }
}