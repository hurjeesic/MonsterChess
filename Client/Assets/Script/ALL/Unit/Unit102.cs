using MonsterChessClient;
using UnityEngine;

namespace UnitType
{
    public class Unit102 : Unit
    {
        protected override void Awake()
        {
            ID = "102";
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