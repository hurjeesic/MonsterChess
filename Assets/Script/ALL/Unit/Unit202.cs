namespace UnitType
{
    public class Unit202 : Unit
    {
        protected override void Awake()
        {
            ID = "202";
            Distence = 2;
            Direction = 1;
            Cost = 1;
            fullHp =5;
            ap = 4;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}