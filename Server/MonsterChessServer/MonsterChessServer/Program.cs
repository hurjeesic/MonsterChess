using FreeNet;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MonsterChessServer
{
    /* 각 Class 별 역할 설명
     * DatabaseManager : Game Server의 전체 DB를 관리하는 Class
     * 
     * Unit : 모든 Unit의 공통 Member 변수와 Method가 정의된 Class
     * Unitxxx : 각 Unit의 고유 능력치가 있는 Class
     * 
     * Protocol : Client와의 통신을 위한 Protocol을 정의해둔 Class
     * Vector2 : 2차원 Vector를 나타내는 Class, 각 말들의 거리 계산에 사용됨
     
     * DatabaseManager : User 등의 정보를 저장하는 DB를 관리함

     * Program : Game Server의 시작 부분
     * GameServer : Game Server 객체, Server는 하나만 존재
     * GameRoomManager : Game 방을 관리하는 Manager Class
     * GameRoom : Game Logic이 구현되어 있는 Class - 아직 Virus War 코드
     * Helper : Game Logic 구현 시 도움이 되는 Method를 모아 놓은 Class - 아직 Virus War 코드
     * GameUser : Game에 접속한 User를 나타내는 Class, User의 요청을 받아들이고 현재 들어가 있는 Game 방을 Member 변수로 갖고 있음 - 아직 Virus War 코드
     * Player : Game의 Player에 관련된 내용을 담은 Class, Player Index와 현재 보유하고 있는 말의 갯수와 종류를 관리함 - 아직 Virus War 코드
    */

    class Program
    {
        public static List<GameUser> userList;
        public static GameServer gameServer = new GameServer();
        public readonly static string ipAddress = "192.168.1.84";
        public readonly static string key = "Gmen";

        static void Main(string[] args)
        {
            PacketBufferManager.Initialize(2000);
            userList = new List<GameUser>();

            NetworkService service = new NetworkService();

            // Callback Delegate 설정
            service.sessionCreatedCallback += OnSessionCreated;

            // 초기화
            service.Initialize();
            service.Listen(ipAddress, 7979, 100);

            Console.WriteLine("Started!");
            while (true)
            {
                string input = Console.ReadLine();
                Console.Write(".");
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Client가 접속 완료 하였을 때 호출됨
        /// n개의 Worker Thread에서 호출될 수 있으므로 공유 자원 접근시 동기화 처리가 요구됨
        /// </summary>
        /// <returns></returns>
        static void OnSessionCreated(UserToken token)
        {
            GameUser user = new GameUser(token);

            lock (userList)
            {
                userList.Add(user);
            }
        }

        public static void RemoveUser(GameUser user)
        {
            lock (userList)
            {
                userList.Remove(user);
                gameServer.DisconnectUser(user);

                GameRoom room = user.battleRoom;
                if (room != null)
                {
                    gameServer.roomManager.RemoveRoom(user.battleRoom);
                }
            }
        }
    }
}