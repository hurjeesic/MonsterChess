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
            hp = fullHp;
            ap = 3;
            dp = 1;

            base.Awake();
        }
       
    }
}