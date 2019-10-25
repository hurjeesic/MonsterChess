using System;
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
            bool answer;
            Vector2 moving = pos1 - pos2;

            short x = (short)Math.Abs(moving.x);
            short y = (short)Math.Abs(moving.y);

            answer = unit.CheckMoving(moving);

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
            Vector2 answer = begin - after;

            if (answer.x > 0) answer.x = 1;
            else if (answer.x < 0) answer.x = -1;

            if (answer.y > 0) answer.y = 1;
            else if (answer.y < 0) answer.y = -1;

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
            // 아직 완전한 처리가 안되서 이것은 상의한 후에 작성할 것
            if (allPlayer[0].GetUnitCount() == 1 || allPlayer[1].GetUnitCount() == 1)
            {
                return 2;
            }
            else if (allPlayer[0].GetUnitCount() == 1)
            {
                return 1;
            }
            else if (allPlayer[1].GetUnitCount() == 1)
            {
                return 0;
            }

            foreach (Player player in allPlayer)
            {

            }

            return -1;
        }
    }
}