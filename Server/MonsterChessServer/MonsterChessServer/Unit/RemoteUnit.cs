namespace UnitType
{
    public abstract class RemoteUnit : Unit
    {
        public int attackDistance;

        public virtual void Wait(Unit enemyUnit)
        {
            if (enemyUnit != null)
            {
                Attack(enemyUnit);
            }
        }
    }
}