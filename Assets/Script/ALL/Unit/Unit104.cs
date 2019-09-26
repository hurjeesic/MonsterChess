namespace UnitType
{
    public class Unit104 : RemoteUnit
    {
        int RangeNum;

        protected override void Awake()
        {
            ID = "104";
            Distence = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 4;
            fullHp = 3;
            ap = 3;

            base.Awake();
        }

        public override void Attack(int playCount)
        {

        }
    }
}