using System.Collections.Generic;

namespace MonsterChessClient
{
    public struct CellInfo
    {
        public int score;
        public short fromCell;
        public short toCell;
    }

    public class PlayerAgent
    {
        public CellInfo Run(List<short> board, List<Player> players, List<short> attackerCells, List<short> victim_cells)
        {
            List<CellInfo> cellScores = new List<CellInfo>();
            int totalBestScore = 0;
            attackerCells.ForEach(cell =>
            {
                int bestScore = 0;
                short cellTheBest = 0;
                List<short> availableCells = Helper.find_available_cells(cell, board, players);
                availableCells.ForEach(toCell =>
                {
                    // simulate
                    int score = CalculateScore(cell, toCell, victim_cells);
                    if (bestScore < score)
                    {
                        cellTheBest = toCell;
                        bestScore = score;
                    }
                });

                if (totalBestScore < bestScore)
                {
                    totalBestScore = bestScore;
                }

                CellInfo info = new CellInfo();
                info.score = bestScore;
                info.fromCell = cell;
                info.toCell = cellTheBest;
                cellScores.Add(info);
            });

            List<CellInfo> topScores = cellScores.FindAll(info => info.score == totalBestScore);
            System.Random rnd = new System.Random();
            int index = rnd.Next(0, topScores.Count);
            return topScores[index];

            //cell_scores.Sort(delegate(CellInfo left, CellInfo right) { return right.score.CompareTo(left.score); });
            //return cell_scores[0];
        }

        int CalculateScore(short fromCell, short toCell, List<short> victimCells)
        {
            int score = 0;

            // 1. Calculate move score. clone = 1, move = 0
            short distance = Helper.get_distance(fromCell, toCell);
            if (distance <= 1)
            {
                score = 1;
            }

            // 2. Calculate fighting score.
            int fightingScore = CalculateCellCountToEat(toCell, victimCells);

            return score + fightingScore;
        }

        int CalculateCellCountToEat(short cell, List<short> victimCells)
        {
            List<short> cellsToEat = Helper.find_neighbor_cells(cell, victimCells, 1);
            return cellsToEat.Count;
        }
    }
}