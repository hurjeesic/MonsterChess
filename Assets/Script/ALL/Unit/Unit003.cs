namespace UnitType
{
    public class Unit003 : Unit
    {
        protected override void Awake()
        {
            ID = "003";
            Distence = 2;
            Direction = 1;
            Cost = 2;
            fullHp = 3;
            hp = 3;
            ap = 2;

            base.Awake();
        }

        public override bool Defence(int enemyAp, int enemyHp)
        {
            return base.Defence(enemyAp - 1, enemyHp);
        }
    }
}