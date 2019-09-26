namespace UnitType
{
    public class Unit010 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "010";
            Distence = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 3;
            ap = 4;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}