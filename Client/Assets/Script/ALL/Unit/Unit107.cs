namespace UnitType
{
    public class Unit107 : Unit
    {
        protected override void Awake()
        {
            ID = "107";
            Distence = 2;
            Direction = 0;
            Cost = 3;
            fullHp = 2;
            hp = fullHp; 
            ap = 4;
            dp = 0;
            base.Awake();
        }

    }
}