namespace UnitType
{
    public class Unit009 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "009";
            Distence = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 4;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}