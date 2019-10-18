namespace UnitType
{
    public class Unit004 : Unit
    {
        protected override void Awake()
        {
            ID = "004";
            Distence = 2;
            Direction = 1;
            Cost = 2;
            fullHp = 3;
            hp = fullHp;
            ap = 2;
            dp = 0;

            base.Awake();
        }
     
    }
}