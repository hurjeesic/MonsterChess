namespace UnitType
{
    public class Unit201 : Unit
    {
        protected override void Awake()
        {
            ID = "201";
            Distence = 3;
            Direction = 1;
            Cost = 1;
            fullHp = 5;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}