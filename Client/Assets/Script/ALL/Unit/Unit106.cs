
using UnityEngine;
using MonsterChessClient;

namespace UnitType
{
    public class Unit106 : Unit
    {
        protected override void Awake()
        {
            ID = "106";
            Distence = 2;
            Direction = 1;
            Cost = 3;
            fullHp = 3;
            hp = fullHp;
            ap = 3;
            dp = 0;

            base.Awake();
        }
       
    }
}