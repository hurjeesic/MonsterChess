namespace UnitType
{
    public class Unit005 : Unit
    {
        protected override void Awake()
        {
            ID = "005";
            Distence = 1;
            Direction = 0;
            Cost = 2;
            fullHp = 3;
            hp = 3;
            ap = 2;

            base.Awake();
        }

      
        public override bool Defence(int enemyAp, int enemyHp)
        {
            return base.Defence(enemyAp, --enemyHp);
        }
    }
}