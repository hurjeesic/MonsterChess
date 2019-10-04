namespace UnitType
{
    public class Unit010 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "010";
            Distence = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 3;
            hp = 3;
            ap = 4;

            base.Awake();
        }
        int damageCount = 2;

        public override void Attack(int playCount)
        {
            damageCount = 2;
            base.Attack(playCount);
        }

        public override bool Defence(int enemyAp, int enemyHp)
        {
            if (damageCount > 0) return false;
            else return base.Defence(enemyAp, enemyHp);
        }
    }
}