using System.Collections.Generic;
using UnityEngine;
namespace UnitType
{
    public class Unit001 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "001";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 2;
            fullHp = 2;
            hp = fullHp;
            ap = 2;
            dp = 0;
            base.Awake();
        }

    }
}