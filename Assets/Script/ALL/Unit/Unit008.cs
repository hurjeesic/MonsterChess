namespace UnitType
{
    public class Unit008 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "008";
            Distence = 1;
            Direction = 1;
            Cost = 3;
            fullHp = 3;
            ap = 2;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }

        public override bool Defence(int enemyAp, int enemyHp)
        {
            enemyHp -= ap;

            return base.Defence(enemyAp, enemyHp);
        }
    }
}