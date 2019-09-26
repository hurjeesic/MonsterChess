namespace UnitType
{
    public class Unit102 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "102";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 3;
            fullHp = 3;
            ap = 2;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}