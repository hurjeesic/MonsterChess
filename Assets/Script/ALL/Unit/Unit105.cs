using UnityEngine;
namespace UnitType
{
    public class Unit105 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "105";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 3;
            hp = fullHp;
            ap = 3;
            dp = 1;

            base.Awake();
        }

    }
}