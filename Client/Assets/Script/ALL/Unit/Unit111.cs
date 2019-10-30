namespace UnitType
{
    public class Unit111 : Unit
    {
        protected override void Awake()
        {
            ID = "111";
            Distence = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 3;
            hp = fullHp;

            ap = 2;
            dp = 2;
            
            base.Awake();
        }
       

    }
}