using UnityEngine;
using MonsterChessClient;
using System.Collections.Generic;

namespace UnitType
{
    public class Unit202 : Unit
    {
        protected override void Awake()
        {
            ID = "202";
            Distence = 2;
            Direction = 1;
            Cost = 1;
            fullHp =3;
            hp = fullHp;
            ap = 5;
            dp = 3;

            base.Awake();
        }

      
    }
}