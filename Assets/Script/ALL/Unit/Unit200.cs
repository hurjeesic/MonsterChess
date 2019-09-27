namespace UnitType
{
    public class Unit200 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "200";
            Distence = 3;
            Direction = 1;
            Cost = 0;
            fullHp = 5;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}