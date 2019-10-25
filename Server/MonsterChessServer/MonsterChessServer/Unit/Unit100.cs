namespace UnitType
{
    public class Unit100 : RemoteUnit
    {
        public Unit100()
        {
            ID = "100";
            Distance = 1;
            attackDistance = 2;
            Direction = 1;
            Cost = 2;
            fullHp = 1;
            HP = fullHp;
            AP = 2;
            DP = 1;
        }
    }
}