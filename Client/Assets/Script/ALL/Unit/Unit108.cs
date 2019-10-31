namespace UnitType
{
    public class Unit108 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "108";
            Distence = 1;
            Direction = 1;
            Cost = 3;
            fullHp = 3;
            hp = fullHp;
            ap = 1;
            dp = 2;

            base.Awake();
        }
    }
}