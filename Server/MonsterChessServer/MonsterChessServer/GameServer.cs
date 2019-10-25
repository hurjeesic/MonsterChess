using FreeNet;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MonsterChessServer
{
    class GameServer
    {
        object operationLock;
        Queue<Packet> userOperations;

        // Logic은 하나의 Thread로만 처리
        Thread logicThread;
        AutoResetEvent loopEvent;

        //----------------------------------------------------------------
        // Game Logic 처리 관련 변수들
        //----------------------------------------------------------------
        // Game 방들을 관리하는 Manager
        public GameRoomManager roomManager { get; private set; }

        // Matching 대기 목록
        List<GameUser> matchingUserLst;
        //----------------------------------------------------------------

        public GameServer()
        {
            this.operationLock = new object();
            this.loopEvent = new AutoResetEvent(false);
            this.userOperations = new Queue<Packet>();

            // Game Logic 관련
            this.roomManager = new GameRoomManager();
            this.matchingUserLst = new List<GameUser>();

            this.logicThread = new Thread(GameLoop);
            this.logicThread.Start();
        }

        /// <summary>
        /// Game Logic을 수행하는 Loop
        /// User Packet 처리를 담당한다.
        /// </summary>
        void GameLoop()
        {
            while (true)
            {
                Packet packet = null;
                lock (this.operationLock)
                {
                    if (this.userOperations.Count > 0)
                    {
                        packet = this.userOperations.Dequeue();
                    }
                }

                if (packet != null)
                {
                    // Packet 처리
                    ProcessReceive(packet);
                }

                // 더 이상 처리할 Packet이 없으면 Thread 대기
                if (this.userOperations.Count <= 0)
                {
                    this.loopEvent.WaitOne();
                }
            }
        }

        public void EnqueuePacket(Packet packet, GameUser user)
        {
            lock (this.operationLock)
            {
                this.userOperations.Enqueue(packet);
                this.loopEvent.Set();
            }
        }

        void ProcessReceive(Packet msg)
        {
            // todo : user msg filter 확인

            msg.owner.ProcessUserOperation(msg);
        }

        /// <summary>
        /// User로부터 Matching 요청이 왔을 때 호출됨
        /// </summary>
        /// <param name="user">Matching을 신청한 User 객체</param>
        public void RequestMatching(GameUser user)
        {
            // Matching 대기 목록에 중복 추가 여부 확인
            if (this.matchingUserLst.Contains(user))
            {
                Console.WriteLine("Matching 중복 요청");
                return;
            }

            // Matching 대기 목록에 추가
            this.matchingUserLst.Add(user);

            // 2명이 모이면 Matching 성공
            if (this.matchingUserLst.Count == 2)
            {
                // Game 방 생성
                this.roomManager.CreateRoom(this.matchingUserLst[0], this.matchingUserLst[1]);

                // Matching 대기 목록 초기화
                this.matchingUserLst.Clear();
            }
        }

        public void DisconnectUser(GameUser user)
        {
            if (this.matchingUserLst.Contains(user))
            {
                this.matchingUserLst.Remove(user);
            }
        }
    }
}