namespace UnitType
{
    public class Unit007 : Unit
    {
        protected override void Awake()
        {
            ID = "007";
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