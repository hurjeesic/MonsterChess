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
            fullHp = 2;
            hp = fullHp;
            ap = 3;
            dp = 1;

            base.Awake();
        }

      
    }
}