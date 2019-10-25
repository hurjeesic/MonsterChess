namespace UnitType
{
    public class Unit008 : Unit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "008";
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