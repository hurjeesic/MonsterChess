﻿namespace UnitType
{
    public class Unit103 : Unit
    {
        protected override void Awake()
        {
            ID = "103";
            Distence = 2;
            Direction = 1;
            Cost = 2;
            fullHp = 2;
            hp = fullHp;
            ap = 2;
            dp = 1;

            base.Awake();
        }
    }
}