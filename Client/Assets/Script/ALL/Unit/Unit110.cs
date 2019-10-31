namespace UnitType
{
    public class Unit110 : Unit
    {

        protected override void Awake()
        {
            ID = "110";
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