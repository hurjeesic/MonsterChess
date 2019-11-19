using FreeNet;
using UnitType;
using System.Collections.Generic;

namespace MonsterChessServer
{
    public class Player
    {
        private GameUser owner;
        public string Name { get { return owner.Name; } }
        public readonly byte playerIndex;
        public readonly List<Unit> units;
        public int mana;

        public Player(GameUser user, byte player_index)
        {
            this.owner = user;
            this.playerIndex = player_index;
            this.units = new List<Unit>();
            this.mana = 10;
        }

        public void Reset()
        {
            this.units.Clear();
        }

        public void AddUnit(Unit unit)
        {
            this.units.Add(unit);
        }

        public bool DestoyUnit(Vector2 pos)
        {
            foreach (Unit unit in units)
            {
                if (pos == unit.Pos)
                {
                    units.Remove(unit);
                    return true;
                }
            }

            return false;
        }

        public void Send(Packet msg)
        {
            this.owner.Send(msg);
            Packet.Destroy(msg);
        }

        public void SendForBroadcast(Packet msg)
        {
            this.owner.Send(msg);
        }

        public int GetUnitCount()
        {
            return this.units.Count;
        }
    }
}