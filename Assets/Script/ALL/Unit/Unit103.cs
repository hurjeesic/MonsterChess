namespace UnitType
{
    public class Unit103 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "103";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 3;
            fullHp = 2;
            ap = 2;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}