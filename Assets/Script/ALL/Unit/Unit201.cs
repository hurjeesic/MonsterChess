using UnityEngine;
using MonsterChessClient;
namespace UnitType
{
    public class Unit201 : Unit
    {
        protected override void Awake()
        {
            ID = "201";
            Distence = 3;
            Direction = 1;
            Cost = 1;
            fullHp = 4;
            hp = fullHp;
            ap = 4;
            dp = 3;

            base.Awake();
        }

   
    }
}