using MonsterChessServer;
using System;

namespace UnitType
{
    public abstract class Unit
    {
        public string ID { get; protected set; }
        public int Distance { get; protected set; }
        public int Direction { get; protected set; }
        public int Cost { get; protected set; }
        public int fullHp { get; protected set; }
        public int HP { get; protected set; }
        public int AP { get; protected set; } // Attack Point
        public int DP { get; protected set; } // Defence Point

        protected Vector2 pos;
        public Vector2 Pos { get { return pos; } }
        protected Vector2 movedPos;
        public Vector2 MovedPos { get { return movedPos; } set { if (value.x > -1 && value.y > -1) movedPos = value; } }
        public int status;
        public virtual void SavePos(Vector2 tempPos)
        {
            pos = tempPos;
        }
        public virtual void SaveMovePos(Vector2 tempPos)
        {
            MovedPos = tempPos;
        }

        public virtual void Move()
        {
            pos = movedPos;
        }

        public virtual void Attack(Unit enemyUnit)
        {
            enemyUnit.Defence(AP);
        }

        protected virtual void Defence(int enemyAp)
        {
            // 방어할 때
            // true -> Destroy, false -> Damage
            int damage = enemyAp - DP;
            if (damage > 0) HP -= damage;
        }

        public bool CheckDeath()
        {
            return HP < 0;
        }
    }
}