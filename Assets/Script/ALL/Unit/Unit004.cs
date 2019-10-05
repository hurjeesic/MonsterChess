namespace UnitType
{
    public class Unit004 : Unit
    {
        protected override void Awake()
        {
            ID = "004";
            Distence = 1;
            Direction = 1;
            Cost = 2;
            fullHp = 3;
            hp = 3;
            ap = 2;

            base.Awake();
        }
        public override void Move()
        {
            recovery();
            base.Move();
        }
        private void recovery(){if (hp < fullHp) hp++;}
        public override void Wait(int playCount)
        {
            recovery();
        }
        public override void Attack(int playCount)
        {
            recovery();
            base.Attack(playCount);
        }
    }
}