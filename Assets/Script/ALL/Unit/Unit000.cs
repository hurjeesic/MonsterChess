using MonsterChessClient;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace UnitType
{
    public class Unit000 : Unit
    {
        protected override void Awake()
        {
            ID = "000";
            Distence = 1;
            Direction = 1;
            Cost = 1;
            fullHp = 2;
            hp = fullHp;
            ap = 1;
            dp = 1;
            base.Awake();
        }
    }
}