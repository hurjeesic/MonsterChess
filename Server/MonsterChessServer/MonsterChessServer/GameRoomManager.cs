using System.Collections.Generic;

namespace MonsterChessServer
{
    public class GameRoomManager
    {
        List<GameRoom> roomLst;

        public GameRoomManager()
        {
            this.roomLst = new List<GameRoom>();
        }

        /// <summary>
        /// Matching을 요청한 User들을 넘겨 받아 Game 방을 생성
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        public void CreateRoom(GameUser user1, GameUser user2)
        {
            // Game 방을 생성하여 입장 시킴
            GameRoom battleRoom = new GameRoom(this);
            battleRoom.EnterRoom(user1, user2);

            // Game 방 목록에 추가하여 관리
            this.roomLst.Add(battleRoom);
        }

        public void RemoveRoom(GameRoom room)
        {
            room.DestroyRoom();
            this.roomLst.Remove(room);
        }
    }
}