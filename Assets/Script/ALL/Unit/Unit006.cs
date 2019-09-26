namespace UnitType
{
    public class Unit006 : Unit
    {
        protected override void Awake()
        {
            ID = "006";
            Distence = 2;
            Direction = 1;
            Cost = 1;
            fullHp = 4;
            ap = 2;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}