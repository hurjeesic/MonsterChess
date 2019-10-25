using MonsterChessClient;
using UnityEngine;

namespace UnitType
{
    public class Unit002 : Unit
    {
        protected override void Awake()
        {
            ID = "002";
            Distence = 1;
            Direction = 1;
            Cost = 1;
            fullHp = 2;
            hp = fullHp;
            ap = 2;
            dp = 0;

            base.Awake();
        }
    }
}