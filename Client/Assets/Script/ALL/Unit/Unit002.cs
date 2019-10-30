using UnityEngine;
namespace UnitType
{
    public class Unit002 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "002";
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