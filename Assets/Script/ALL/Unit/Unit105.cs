namespace UnitType
{
    public class Unit105 : RemoteUnit
    {
        protected override void Awake()
        {
            ID = "105";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 4;
            ap = 2;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}