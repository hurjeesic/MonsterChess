﻿namespace UnitType
{
    public class Unit011 : Unit
    {
        protected override void Awake()
        {
            ID = "011";
            Distence = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 4;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}