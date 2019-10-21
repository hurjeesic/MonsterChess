using UnityEngine;
using MonsterChessClient;
namespace UnitType
{
    public class Unit200 : Unit
    {
       
        protected override void Awake()
        {
            ID = "200";
            Distence = 3;
            Direction = 1;
            Cost = 0;
            fullHp = 5;
            hp = fullHp;
            ap = 3;
            dp = 3;

            base.Awake();
        }


    }

}