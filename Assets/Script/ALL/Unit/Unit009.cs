using MonsterChessClient;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnitType
{
    public class Unit009 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "009";
            Distence = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 2;
            hp = fullHp;
            ap = 3;
            dp = 3;

            base.Awake();
        }

      
    }
}