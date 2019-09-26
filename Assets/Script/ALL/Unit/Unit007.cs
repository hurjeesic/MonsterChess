namespace UnitType
{
    public class Unit007 : Unit
    {
        protected override void Awake()
        {
            ID = "007";
            Distence = 2;
            Direction = 0;
            Cost = 3;
            fullHp = 3;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}