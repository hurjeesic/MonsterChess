
using UnityEngine;
using MonsterChessClient;

namespace UnitType
{
    public class Unit006 : Unit
    {
        protected override void Awake()
        {
            ID = "006";
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