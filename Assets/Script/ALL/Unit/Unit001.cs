﻿namespace UnitType
{
    public class Unit001 : Unit
    {
        protected override void Awake()
        {
            ID = "001";
            Distence = 1;
            Direction = 1;
            Cost = 1;
            fullHp = 2;
            hp = 2;
            ap = 1;
           

            base.Awake();
        }

        bool reincarnation = true;
        public override bool Defence(int enemyAp, int enemyHp)
        {
            bool answer = base.Defence(enemyAp, enemyHp);

            if (answer&& reincarnation)
            {
                NewUnit();
            }

            return answer;
        }

        public void NewUnit()
        {
            //새로 생성
            reincarnation = false;
        }
    }
}