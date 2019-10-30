namespace UnitType
{
    public class Unit101 : Unit
    {
        protected override void Awake()
        {
            ID = "101";
            Distence = 1;
            Direction = 0;
            Cost = 1;
            fullHp = 2;
            hp = fullHp;
            ap = 1;
            dp = 2;
           

            base.Awake();
        }

       
    }
}