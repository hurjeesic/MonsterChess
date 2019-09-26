namespace UnitType
{
    public class Unit002 : Unit
    {
        protected override void Awake()
        {
            ID = "002";
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
    }
}