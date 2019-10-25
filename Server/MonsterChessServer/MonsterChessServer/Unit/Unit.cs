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
        public Vector2 MovedPos { get { return movedPos; } set { if (value.x > 0 && value.y > 0) movedPos = value; } }
        public int status;

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

        public bool CheckMoving(Vector2 movingPos)
        {
            bool answer = false;
            if (Direction < 2)
            {
                if (Direction >= 0)
                {
                    if ((Distance >= movingPos.x && movingPos.x >= -Distance && movingPos.y == 0) || // 서 -> 동
                        (movingPos.x == 0 && Distance >= movingPos.y && movingPos.y >= -Distance)) // 북 -> 남
                    {
                        answer = true;
                    }
                }

                if (Direction >= 1)
                {
                    if (Math.Abs(movingPos.x) == Math.Abs(movingPos.y) &&
                       (0 > movingPos.x && movingPos.x >= -Distance && movingPos.y > 0) || // 북서
                       (Distance >= movingPos.x && movingPos.x > 0 && movingPos.y > 0) || // 북동
                       (0 > movingPos.x && movingPos.x >= -Distance && movingPos.y < 0) || // 남서
                       (Distance >= movingPos.x && movingPos.x > 0 && movingPos.y < 0)) // 남동
                    {
                        answer = true;
                    }
                }
            }

            return answer;
        }
    }
}