namespace UnitType
{
    public class Unit105 : Unit
    {
        protected override void Awake()
        {
            ID = "105";
            Distence = 1;
            Direction = 0;
            Cost = 2;
            fullHp = 3;
            hp = fullHp;
            ap = 1;
            dp = 2;

            base.Awake();
        }
    }
}