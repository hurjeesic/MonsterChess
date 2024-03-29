﻿using System;
using System.Collections.Generic;
using UnitType;

namespace MonsterChessServer
{
    public static class Helper
    {
        /// <summary>
        /// x축을 기준으로 회전된 반대 좌표를 반환
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static Vector2 GetReversePosition(Vector2 pos, int row)
        {
            return new Vector2(pos.x, row - pos.y - 1);
        }

        /// <summary>
        /// 이동 좌표가 해당 Unit이 이동가능한 범위인지 확인
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static bool CheckMoving(Unit unit, Vector2 pos1, Vector2 pos2, int column, int row)
        {
            bool answer = false;
            Vector2 moving = pos1 - pos2;

            short x = (short)Math.Abs(moving.x);
            short y = (short)Math.Abs(moving.y);
            
            if (unit.Direction < 2)
            {
                if (unit.Direction >= 0)
                {
                    if ((unit.Distance >= moving.x && moving.x >= -unit.Distance && moving.y == 0) || // 서 -> 동
                        (moving.x == 0 && unit.Distance >= moving.y && moving.y >= -unit.Distance)) // 북 -> 남
                    {
                        answer = true;
                    }
                }

                if (unit.Direction >= 1)
                {
                    if (Math.Abs(moving.x) == Math.Abs(moving.y) &&
                       (0 > moving.x && moving.x >= -unit.Distance && moving.y > 0) || // 북서
                       (unit.Distance >= moving.x && moving.x > 0 && moving.y > 0) || // 북동
                       (0 > moving.x && moving.x >= -unit.Distance && moving.y < 0) || // 남서
                       (unit.Distance >= moving.x && moving.x > 0 && moving.y < 0)) // 남동
                    {
                        answer = true;
                    }
                }
            }

            return answer;
        }

        public static List<Vector2> GetMoving(Vector2 pos, int distance, int direction, int column, int row)
        {
            List<Vector2> answer = new List<Vector2>();

            int x = pos.x;
            int y = pos.y;

            for (int i = 1; i <= distance; i++)
            {
                if (direction < 2)
                {
                    if (direction >= 0)
                    {
                        // 동, 서, 남, 북
                        if (x + distance < column) answer.Add(new Vector2(x + distance, y));
                        if (x - distance >= 0) answer.Add(new Vector2(x - distance, y));
                        if (y - distance >= 0) answer.Add(new Vector2(x, y - distance));
                        if (y + distance < column) answer.Add(new Vector2(x, y + distance));
                    }

                    if (direction >= 1)
                    {
                        // 북서, 북동, 남서, 남동
                        if (x + distance < column && y - distance >= 0) answer.Add(new Vector2(x + distance, y - distance));
                        if (x + distance < column && y + distance < column) answer.Add(new Vector2(x + distance, y + distance));
                        if (x - distance >= 0 && y - distance >= 0) answer.Add(new Vector2(x - distance, y - distance));
                        if (x - distance >= 0 && y + distance < column) answer.Add(new Vector2(x - distance, y + distance));
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// 0 = 동, 1 = 서, 2 = 남, 3 = 북, 4 = 북동, 5 = 남동, 6 = 남서, 7 = 북서
        /// 움직이는 방향이 어디인지 확인
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public static Vector2 GetDirection(Vector2 begin, Vector2 after)
        {
            Vector2 answer = after - begin;

            if (answer.x > 0) answer.x = 1;
            else if (answer.x < 0) answer.x = -1;

            if (answer.y > 0) answer.y = 1;
            else if (answer.y < 0) answer.y = -1;

            return answer;
        }

        /// <summary>
        /// 0 = 동, 1 = 서, 2 = 남, 3 = 북, 4 = 북동, 5 = 남동, 6 = 남서, 7 = 북서
        /// 움직이는 방향이 어디인지 확인
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public static int GetDistance(Vector2 begin, Vector2 after)
        {
            int answer = 0;
            Vector2 cal = after - begin;

            Console.WriteLine(cal.x+","+cal.y + ": cal");

            int x = cal.x >= 0 ? cal.x : -cal.x;
            int y = cal.y >= 0 ? cal.y : -cal.y;
            answer = x >= y ? x : y;

            return answer;
        }

        /// <summary>
        /// Game을 지속 할 수 있는지 확인
        /// </summary>
        /// <param name="board"></param>
        /// <param name="players"></param>
        /// <param name="current_player_index"></param>
        /// <returns></returns>
        public static int GetPlayResult(Player[] allPlayer)
        {
            bool[] liveBoss = new bool[2];

            foreach (Player player in allPlayer)
            {
                liveBoss[player.playerIndex] = false;
                foreach (Unit unit in player.units)
                {
                    if (unit.ID[0] == '2')
                    {
                        liveBoss[player.playerIndex] = true;
                        break;
                    }
                }
            }

            if (!liveBoss[0] && !liveBoss[1])
            {
                return 2;
            }
            else if (!liveBoss[0] || !liveBoss[1])
            {
                return !liveBoss[0] ? 1 : 0;
            }
            else if (allPlayer[0].GetUnitCount() == 1 && allPlayer[1].GetUnitCount() == 1)
            {
                return 2;
            }
            else if (allPlayer[0].GetUnitCount() == 1 || allPlayer[1].GetUnitCount() == 1)
            {
                return allPlayer[0].GetUnitCount() == 1 ? 1 : 0;
            }

            return -1;
        }
    }
}