using System.Collections.Generic;
using UnityEngine;

namespace MonterChessClient
{
    public enum PlayerState
    {
        HUMAN,
        AI
    }

    public class Player : MonoBehaviour
    {
        public List<short> cellIndexes { get; private set; }
        public byte playerIndex { get; private set; }
        public PlayerState state { get; private set; }
        PlayerAgent agent;

        void Awake()
        {
            this.cellIndexes = new List<short>();
            this.agent = new PlayerAgent();
        }


        public void Clear()
        {
            this.cellIndexes.Clear();
        }

        public void Initialize(byte playerIndex)
        {
            this.playerIndex = playerIndex;
        }

        public void Add(short cell)
        {
            if (this.cellIndexes.Contains(cell))
            {
                Debug.LogError(string.Format("Already have a cell. {0}", cell));
                return;
            }

            this.cellIndexes.Add(cell);
        }

        public void Remove(short cell)
        {
            this.cellIndexes.Remove(cell);
        }

        public void ChangeToAgent()
        {
            this.state = PlayerState.AI;
        }

        public void ChangeToHuman()
        {
            this.state = PlayerState.HUMAN;
        }

        public CellInfo RunAgent(List<short> board, List<Player> players, List<short> victim_cells)
        {
            return this.agent.Run(board, players, this.cellIndexes, victim_cells);
        }

        public int GetVirusCount()
        {
            return this.cellIndexes.Count;
        }
    }
}