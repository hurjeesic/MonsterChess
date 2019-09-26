namespace UnitType
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
            ap = 1;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }

        public override bool Defence(int enemyAp, int enemyHp)
        {
            bool answer = base.Defence(enemyAp, enemyHp);

            if (answer)
            {
                NewUnit();
            }

            return answer;
        }

        public void NewUnit()
        {

        }
    }
}