namespace UnitType
{
    public class Unit104 : Unit
    {
        protected override void Awake()
        {
            ID = "104";
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