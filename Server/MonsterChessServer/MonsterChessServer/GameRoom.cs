using FreeNet;
using UnitType;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MonsterChessServer
{
    /// <summary>
	/// Game Logic이 처리되는 핵심 Class
	/// </summary>
    public class GameRoom
    {
        enum PlayerState : byte
        {
            EnteredRoom, // 방에 막 입장한 상태
            CompleteLoading, // Loading이 완료된 상태
            ReadyToTurn, // Turn 진행 준비 상태
            ProcessTurn, // Turn 연출 중인 상태
            FinishTurn // Turn 연출을 모두 완료한 상태
        }

        GameRoomManager manager;

        // Game을 진행하는 Player(1P, 2P)
        Player[] players;

        // Player들의 상태를 관리하는 변수
        Dictionary<byte, PlayerState> playerState;

        // 현재 Turn을 진행하고 있는 Player의 Index
        byte currentPlayer;
        int turnCount;
        KeyValuePair<byte, Unit>[,] gameBoard;

        bool bGame;

        List<KeyValuePair<Vector2, Vector2>>[] movingLst;

        const int MAX = 10;
        int timer = MAX;
        int waitingTime = 2000;

        public static readonly byte COLUMN = 7, ROW = 7;

        readonly KeyValuePair<byte, Unit> EMPTY;

        public GameRoom(GameRoomManager manager)
        {
            this.players = new Player[2];
            this.playerState = new Dictionary<byte, PlayerState>();
            this.currentPlayer = 0;

            movingLst = new List<KeyValuePair<Vector2, Vector2>>[2];
            movingLst[0] = new List<KeyValuePair<Vector2, Vector2>>();
            movingLst[1] = new List<KeyValuePair<Vector2, Vector2>>();

            this.EMPTY = new KeyValuePair<byte, Unit>(255, null);

            this.manager = manager;

            // 7 * 7(총 49칸)모양의 Board을 구성
            // 초기에는 모두 빈 공간이므로 EMPTY_SLOT으로 채움
            this.gameBoard = new KeyValuePair<byte, Unit>[COLUMN, ROW];
            ResetBoard();
        }

        /// <summary>
        /// 모든 User들에게 Msg를 전송
        /// </summary>
        /// <param name="msg"></param>
        void Broadcast(Packet msg)
        {
            this.players[0].SendForBroadcast(msg);
            this.players[1].SendForBroadcast(msg);
            Packet.Destroy(msg);
        }

        /// <summary>
        /// Player의 상태를 변경
        /// </summary>
        /// <param name="player"></param>
        /// <param name="state"></param>
        void ChangePlayerState(Player player, PlayerState state)
        {
            if (this.playerState.ContainsKey(player.playerIndex))
            {
                this.playerState[player.playerIndex] = state;
            }
            else
            {
                this.playerState.Add(player.playerIndex, state);
            }
        }

        /// <summary>
        /// 모든 Player가 특정 상태가 되었는지를 판단
        /// 모든 Player의 동기화를 위해 사용
        /// 모든 Player가 같은 상태에 있다면 true, 한명이라도 다른 상태에 있다면 false를 반환
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        bool ReadyAllPlayers(PlayerState state)
        {
            foreach (KeyValuePair<byte, PlayerState> kvp in this.playerState)
            {
                if (kvp.Value != state)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Matching이 성사된 Player들이 Game에 입장
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void EnterRoom(GameUser user1, GameUser user2)
        {
            // Player들을 생성하고 각각 0번, 1번 Index를 부여
            Player player1 = new Player(user1, 0); // 1P
            Player player2 = new Player(user2, 1); // 2P
            this.players[0] = player1;
            this.players[1] = player2;

            // Player들의 초기 상태를 지정
            this.playerState.Clear();
            ChangePlayerState(player1, PlayerState.EnteredRoom);
            ChangePlayerState(player2, PlayerState.EnteredRoom);

            // Loading 시작 Msg 전송.
            Packet msg = Packet.Create((Int16)PROTOCOL.StartLoading);
            msg.Push(players[0].playerIndex);  // 본인의 Player Index를 알려줌
            players[0].Send(msg);

            msg = Packet.Create((Int16)PROTOCOL.StartLoading);
            msg.Push(players[1].playerIndex);  // 본인의 Player Index를 알려줌
            players[1].Send(msg);

            user1.EnterRoom(player1, this);
            user2.EnterRoom(player2, this);
        }

        public void ResetBoard()
        {
            for (byte i = 0; i < COLUMN; i++)
            {
                for (byte j = 0; j < COLUMN; j++)
                {
                    this.gameBoard[i, j] = EMPTY;
                }
            }
        }

        /// <summary>
        /// Client에서 Loading을 완료한 후 요청
        /// 이 요청이 들어오면 Game을 시작해도 좋다는 의미
        /// 전송되는 Data
        ///     실패했을 경우 - 방 파괴 당사자면 1, 아니면 0
        ///     Matching 완료 - 모든 Unit에 관련된 배치 상황
        /// </summary>
        /// <param name="sender">요청한 유저</param>
        public void CompleteLoading(Player player, List<Unit> units)
        {
            bool bDeployment = true;
            if (units.Count == 6)
            {
                player.Reset();
                foreach (Unit unitInfo in units)
                {
                    if (unitInfo == null ||
                        unitInfo.Pos.x >= COLUMN || (player.playerIndex == 0 ? unitInfo.Pos.y : Helper.GetReversePosition(unitInfo.Pos, ROW).y) >= ROW / 2 ||
                        unitInfo.Pos.x < 0 || unitInfo.Pos.y < 0)
                    {
                        bDeployment = false;
                        break;
                    }

                    players[player.playerIndex].mana -= unitInfo.Cost;

                    player.AddUnit(unitInfo);
                }

                if (players[player.playerIndex].mana < 0)
                {
                    bDeployment = false;
                }
            }
            else
            {
                bDeployment = false;
            }

            if (bDeployment)
            {
                // 해당 Player를 CompletedLoading 상태로 변경
                ChangePlayerState(player, PlayerState.CompleteLoading);

                // 모든 User가 준비 상태인지 확인
                if (!ReadyAllPlayers(PlayerState.CompleteLoading))
                {
                    // 아직 준비가 안 된 User가 있다면 대기
                    return;
                }

                // 다 받은 Data를 기반으로 Game Board을 구성함
                for (byte index = 0; index < players.Length; index++)
                {
                    foreach (Unit unit in players[index].units)
                    {
                        gameBoard[unit.Pos.x, unit.Pos.y] = new KeyValuePair<byte, Unit>(players[index].playerIndex, unit);

                        players[index].mana -= unit.Cost;
                    }
                }

                bGame = true;
                Packet msg = Packet.Create((short)PROTOCOL.SetGame);
                for (byte i = 0; i < 2; i++)
                {
                    foreach (Unit unit in players[i].units)
                    {
                        msg.Push(players[i].playerIndex);
                        msg.Push(unit.ID);
                        msg.Push(unit.Pos.x);
                        msg.Push(unit.Pos.y);
                        Console.WriteLine(unit.Pos.x + "," + unit.Pos.y + " : 보내는 x, y값");
                    }
                }

                Broadcast(msg);
            }
            else
            {
                Packet msg = Packet.Create((short)PROTOCOL.FailDeploy);
                if (player.playerIndex == 0)
                {
                    msg.Push((byte)0);
                    msg.Push((byte)1);
                }
                else
                {
                    msg.Push((byte)1);
                    msg.Push((byte)0);
                }

                Broadcast(msg);
                bDeployment = false;

                manager.RemoveRoom(this);
            }
        }

        /// <summary>
        /// Game을 시작
        /// </summary>
        public void BattleStart(Player player)
        {
            // Turn 초기화
            this.currentPlayer = 0; // 1P부터 시작
            this.turnCount = 0;

            // Turn을 진행할 수 있도록 준비 상태로 만듬
            ChangePlayerState(player, PlayerState.ReadyToTurn);

            // 모든 User가 시작 대기 상태인지 확인
            if (!ReadyAllPlayers(PlayerState.ReadyToTurn))
            {
                // 아직 준비가 안 된 User가 있다면 대기
                return;
            }

            StartTurn();
        }

        /// <summary>
        /// Turn을 시작하라고 모든 Client에게 알림
        /// 전송되는 Data
        ///     선공하는 Player Index
        ///     각 Player의 Mana
        /// </summary>
        void StartTurn()
        {
            movingLst[0].Clear();
            movingLst[1].Clear();

            Packet msg = Packet.Create((short)PROTOCOL.StartedTurn);
            msg.Push(this.currentPlayer);
            msg.Push(this.players[0].mana);
            msg.Push(this.players[1].mana);
            Broadcast(msg);

            Thread timerThread = new Thread(new ThreadStart(FlowTimer));
            timerThread.Start();
        }

        /// <summary>
        /// 모든 Client에게 Turn의 남은 시간 전송
        /// 전송되는 Data
        ///     남은 시간
        /// </summary>
        void FlowTimer()
        {
            while (timer >= 0 && bGame)
            {
                Packet timerMsg = Packet.Create((short)PROTOCOL.Timer);
                timerMsg.Push(timer--);
                Broadcast(timerMsg);
                Thread.Sleep(1000);

                Console.WriteLine(timer + "초 남음");
            }

            ProcessAnimation();
        }

        /// <summary>
        /// Client의 이동 요청
        /// 전송되는 Data
        ///     올바른 움직임이면 1, 아니면 0
        /// </summary>
        /// <param name="sender">요청한 유저</param>
        /// <param name="beginPos">시작 위치</param>
        /// <param name="targetPos">이동하고자 하는 위치</param>
        /// <param name="first">선공 여부</param>
        public void RequestMoving(Player sender, KeyValuePair<Vector2, Vector2> moving)
        {
            bool answer = false;

            // 이동 범위 안에 있다면
            if (COLUMN > moving.Key.x && moving.Key.x >= 0 && ROW > moving.Key.y && moving.Key.y >= 0 &&
                COLUMN > moving.Value.x && moving.Value.x >= 0 && ROW > moving.Value.y && moving.Value.y >= 0)
            {
                Unit unit = gameBoard[moving.Key.x, moving.Key.y].Value;

                if (unit != null && gameBoard[moving.Key.x, moving.Key.y].Key == sender.playerIndex && Helper.CheckMoving(unit, moving.Key, moving.Value, COLUMN, ROW))
                {
                    int index = sender.playerIndex, posIndex = movingLst[index].FindIndex(currentMoving => currentMoving.Key == moving.Key);
                    if (posIndex == -1)
                    {
                        movingLst[index].Add(moving);
                    }
                    else
                    {
                        movingLst[index][posIndex] = moving;
                    }
                }

                answer = true;
            }

            // 이동 요청에 대한 처리 결과를 전송
            Packet msg = Packet.Create((short)PROTOCOL.RequestedMoving);
            msg.Push(answer ? 1 : 0);
            sender.Send(msg);
        }

        /// <summary>
        /// Client의 소환 요청
        /// 전송되는 Data
        ///     올바른 소환이면 소환된 Monster의 ID와 좌표
        ///     해당 Player의 Mana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="summons"></param>
        public void RequestSummons(Player sender, Unit summons)
        {
            bool answer = false;

            if (COLUMN > summons.Pos.x && summons.Pos.x >= 0 && ROW > summons.Pos.y && summons.Pos.y >= 0 &&
                gameBoard[summons.Pos.x, summons.Pos.y].Value == null && summons.Cost <= sender.mana)
            {
                answer = true;

                sender.AddUnit(summons);
                sender.units[sender.units.Count - 1].MovedPos = summons.Pos;
                sender.units[sender.units.Count - 1].Move();
                gameBoard[summons.Pos.x, summons.Pos.y] = new KeyValuePair<byte, Unit>(sender.playerIndex, summons);
                sender.mana -= summons.Cost;
            }

            // 소환 요청에 대한 처리 결과를 전송
            Packet msg = Packet.Create((short)PROTOCOL.RequestedSummons);
            if (answer)
            {
                msg.Push(0);
                msg.Push(summons.ID);
                msg.Push(summons.Pos.x);
                msg.Push(summons.Pos.y);
                msg.Push(sender.mana);
            }
            else
            {
                msg.Push(1);
            }
            Broadcast(msg);
        }

        /// <summary>
        /// Timer가 끝난 후 전송된 움직임들을 바탕으로 판을 조정하고
        /// 해당 결과를 모든 Client들에게 전송
        /// 전송되는 Data
        ///     -1(해당 Turn의 모든 진행이 완료되었음을 의미)
        ///     뒤바뀐 선공 Player Index
        /// </summary>
        public void ProcessAnimation()
        {
            ChangePlayerState(players[0], PlayerState.ProcessTurn);
            ChangePlayerState(players[1], PlayerState.ProcessTurn);

            // 움직일 순서 정렬
            for (byte index = 0; index < players.Length; index++)
            {
                movingLst[index].Sort(delegate (KeyValuePair<Vector2, Vector2> first, KeyValuePair<Vector2, Vector2> second)
                {
                    if (gameBoard[first.Key.x, first.Key.y].Value.ID[0] == gameBoard[first.Key.x, first.Key.y].Value.ID[0])
                    {
                        if (gameBoard[first.Key.x, first.Key.y].Value.Cost == gameBoard[first.Key.x, first.Key.y].Value.Cost)
                        {
                            return 0;
                        }
                        else
                        {
                            return gameBoard[first.Key.x, first.Key.y].Value.Cost > gameBoard[first.Key.x, first.Key.y].Value.Cost ? 1 : -1;
                        }
                    }
                    else
                    {
                        return gameBoard[first.Key.x, first.Key.y].Value.ID[0] > gameBoard[first.Key.x, first.Key.y].Value.ID[0] ? 1 : -1;
                    }
                });
            }

            Console.WriteLine("이동 요청 목록"); // 이동 요청 목록 출력
            for (byte index = 0; index < players.Length; index++)
            {
                for (int i = 0; i < movingLst[index].Count; i++)
                {
                    Console.WriteLine(gameBoard[movingLst[index][i].Key.x, movingLst[index][i].Key.y].Value.ID + " " +
                        movingLst[index][i].Key.x + ", " + movingLst[index][i].Key.y + " -> " +
                        movingLst[index][i].Value.x + ", " + movingLst[index][i].Value.y);
                }

                Console.WriteLine();
            }

            byte midTurn = currentPlayer;
            byte midTurn2 = currentPlayer;
            int count = 0;

            // 계산 후 결과 전송
            while (movingLst[0].Count > 0 && movingLst[1].Count > 0)
            {
                SendMoving(midTurn);

                midTurn = (byte)(midTurn == 0 ? 1 : 0);

                Thread.Sleep(waitingTime);
            }

            while (movingLst[0].Count > 0)
            {
                SendMoving(0);

                midTurn = 1;

                Thread.Sleep(waitingTime);
            }

            while (movingLst[1].Count > 0)
            {
                SendMoving(1);

                midTurn = 0;

                Thread.Sleep(waitingTime);
            }

            // 대기하고 있던 원거리 Unit들을 찾아서 정렬함 - 정렬 기준은 cost와 선공 여부
            List<Vector2>[] waitingUnitLst = new List<Vector2>[2];
            waitingUnitLst[0] = GetAttackingUnit(0);
            waitingUnitLst[1] = GetAttackingUnit(1);

            // 원거리 Unit 공격 순서 정렬
            for (int i = 0; i < 2; i++)
            {
                waitingUnitLst[i].Sort(delegate (Vector2 first, Vector2 second)
                {
                    if (gameBoard[first.x, first.y].Key == EMPTY.Key && gameBoard[second.x, second.y].Key == EMPTY.Key)
                    {
                        return 0;
                    }
                    else if (gameBoard[first.x, first.y].Key == EMPTY.Key || gameBoard[second.x, second.y].Key == EMPTY.Key)
                    {
                        return gameBoard[first.x, first.y].Key != EMPTY.Key ? 1 : -1;
                    }
                    else if (gameBoard[first.x, first.y].Value.Cost == gameBoard[second.x, second.y].Value.Cost)
                    {
                        if (gameBoard[first.x, first.y].Key == gameBoard[second.x, second.y].Key)
                        {
                            return 0;
                        }
                        else
                        {
                            return gameBoard[first.x, first.y].Key == currentPlayer ? 1 : -1;
                        }
                    }
                    else
                    {
                        return gameBoard[first.x, first.y].Value.Cost > gameBoard[second.x, second.y].Value.Cost ? 1 : -1;
                    }
                });
            }

            // 정렬된 순서로 공격을 진행
            while (waitingUnitLst[0].Count > 0 && waitingUnitLst[1].Count > 0)
            {
                SendWaiting(waitingUnitLst[midTurn2]);

                count++;

                waitingUnitLst[midTurn2].RemoveAt(0);

                midTurn2 = (byte)(midTurn2 == 0 ? 1 : 0);
            }

            while (waitingUnitLst[0].Count > 0)
            {
                SendWaiting(waitingUnitLst[0]);

                waitingUnitLst[0].RemoveAt(0);

                count++;

                midTurn2 = 1;
            }

            while (waitingUnitLst[1].Count > 0)
            {
                SendWaiting(waitingUnitLst[1]);

                waitingUnitLst[1].RemoveAt(0);

                count++;

                midTurn2 = 0;
            }

            if (count == 0)
            {
                currentPlayer = midTurn;
            }
            else
            {
                currentPlayer = midTurn2;
            }

            // game board 검사
            for (int y = ROW - 1; y >= 0; y--)
            {
                for (int x = 0; x < COLUMN; x++)
                {
                    if (gameBoard[x, y].Key != EMPTY.Key)
                    {
                        Console.Write(gameBoard[x, y].Key + "," + gameBoard[x, y].Value.ID + "\t");
                    }
                    else
                    {
                        Console.Write(gameBoard[x, y].Key + "\t");
                    }
                }
                Console.WriteLine();
            }

            Thread.Sleep(waitingTime * 2);

            // 계산 결과 전송 코드
            Packet msg = Packet.Create((short)PROTOCOL.MovedUnit);
            msg.Push(-1);
            Broadcast(msg);
        }

        /// <summary>
        /// Unit을 움직이고 나온 결과를 모든 Player에게 전송
        /// HP가 0이하가 되어 파괴되는 Unit은 파괴됨
        /// 전송되는 Data
        ///     0(아직 해당 Turn이 끝나지 않았음을 의미)
        ///     이동한 Unit의 원 좌표와 이동된 좌표, 그리고 HP
        ///     공격당한 적 Unit이 있으면 그 Unit의 원래 좌표와 이동된 좌표, 그리고 HP
        /// </summary>
        /// <param name="index"></param>
        private void SendMoving(byte index)
        {
            Packet msg = Packet.Create((short)PROTOCOL.MovedUnit);
            msg.Push(0);

            KeyValuePair<byte, Unit> myUnit, enemyUnit;

            MoveUnit(movingLst[index][0], out myUnit, out enemyUnit);

            Console.WriteLine("index : " + index);
            Console.WriteLine(myUnit.Value.Pos.x + " : 샌드 x값");
            Console.WriteLine(myUnit.Value.Pos.y + " : 샌드 y값");
            Console.WriteLine(myUnit.Value.MovedPos.x + " : 샌드 movex값");
            Console.WriteLine(myUnit.Value.MovedPos.y + " : 샌드 movex값");

            // 이동 또는 공격한 유닛의 현재 좌표와 이동된 좌표, 그리고 HP
            msg.Push(myUnit.Value.Pos.x);
            msg.Push(myUnit.Value.Pos.y);
            msg.Push(myUnit.Value.MovedPos.x);
            msg.Push(myUnit.Value.MovedPos.y);
            msg.Push(myUnit.Value.HP);
            Console.WriteLine(myUnit.Value.HP + " : hp값");
            // Unit의 위치가 변경 되었으므로 해당 좌표의 Unit 정보 삭제
            if (myUnit.Value.HP <= 0)
            {
                gameBoard[myUnit.Value.Pos.x, myUnit.Value.Pos.y] = EMPTY; // 해당 좌표의 Unit 삭제
                players[index].DestoyUnit(myUnit.Value.Pos);
            }
            else
            {
                myUnit.Value.Move();
            }

            // 공격당한 유닛(없을 수도 있음)
            if (enemyUnit.Key == EMPTY.Key)
            {
                msg.Push(-1);
            }
            else
            {
                msg.Push(0);

                // 공격 당한 유닛의 현재 좌표와 이동된 좌표, 그리고 HP
                msg.Push(enemyUnit.Value.Pos.x);
                msg.Push(enemyUnit.Value.Pos.y);
                msg.Push(enemyUnit.Value.MovedPos.x);
                msg.Push(enemyUnit.Value.MovedPos.y);
                msg.Push(enemyUnit.Value.HP);
                Console.WriteLine("ID : " + enemyUnit.Value.ID);
                Console.WriteLine(enemyUnit.Value.Pos.x + " : 샌드 적x값");
                Console.WriteLine(enemyUnit.Value.Pos.y + " : 샌드적y값");
                Console.WriteLine(enemyUnit.Value.MovedPos.x + " :샌드 적movex값");
                Console.WriteLine(enemyUnit.Value.MovedPos.y + " : 샌드적movex값");
                Console.WriteLine(enemyUnit.Value.HP + " : 샌드적 HP값");

                if (enemyUnit.Value.HP <= 0)
                {
                    gameBoard[enemyUnit.Value.Pos.x, enemyUnit.Value.Pos.y] = EMPTY;
                    players[enemyUnit.Key].DestoyUnit(enemyUnit.Value.Pos);
                }
                else
                {
                    enemyUnit.Value.Move();
                }
            }

            Broadcast(msg);

            movingLst[index].RemoveAt(0);
        }

        /// <summary>
        /// Unit을 직접적으로 움직임
        /// </summary>
        /// <param name="moving"></param>
        /// <param name="myUnit"></param>
        /// <param name="enemyUnit"></param>
        private void MoveUnit(KeyValuePair<Vector2, Vector2> moving, out KeyValuePair<byte, Unit> myUnit, out KeyValuePair<byte, Unit> enemyUnit)
        {
            Console.WriteLine("Move다!!");
            Vector2 direction = Helper.GetDirection(moving.Key, moving.Value);
            Console.WriteLine(direction.x + "," + direction.y + " : 디렉션");
            // 만약을 위해 한번 더 저장 - 삭제 가능
            myUnit = gameBoard[moving.Key.x, moving.Key.y];
            myUnit.Value.MovedPos = moving.Key;
            Console.WriteLine(moving.Key.x + "," + moving.Key.y + ": 내 유닛 좌표");
            // 이동 또는 공격한 유닛의 현재 좌표와 이동된 좌표, 그리고 HP

            //moving.Key, moving.Value의 거리
            enemyUnit = EMPTY;
            int temp = Helper.GetDistance(moving.Key, moving.Value);
            int ditance = temp > 0 ? temp : -temp;
            Console.WriteLine(temp + ": 디스텐스");
            int result = -1;
            Vector2 savePos = new Vector2();
            for (int i = 1; i <= temp; i++)
            {
                Vector2 movingPos = moving.Key + direction * i, knockbackPos = movingPos + direction;
                Console.WriteLine(knockbackPos.x + "," + knockbackPos.y + ": 넉백좌표");
                Console.WriteLine(movingPos.x + "," + movingPos.y + ": 무빙좌표");

                // 이동 중간 위치가 비어있으면 넘기고 이동 위치가 비어있으면 그 위치에 이동
                if (gameBoard[movingPos.x, movingPos.y].Key == EMPTY.Key)
                {
                    //비어있는경우(다음 좌표는 비어있어)
                    Console.WriteLine("1번 if");
                    savePos = movingPos;
                    result = 0;
                }
                // 이동 중간 위치에 아군이 있거나 해당 Unit이 원거리 Unit인 경우
                else if (myUnit.Key == gameBoard[movingPos.x, movingPos.y].Key || myUnit.Value.ID[0] == '0')
                {
                    //아군인경우
                    Console.WriteLine("2번 if");
                    movingPos -= direction;
                    savePos = movingPos;
                    if (movingPos != moving.Key) // 시작값이 아닌경우
                    {
                        gameBoard[movingPos.x, movingPos.y] = myUnit;
                        result = 1;
                    }
                    else // 시작값인경우
                    {
                        result = 3;
                    }

                    break;
                }
                else
                {
                    Console.WriteLine("3번 if");

                    //적군인경우
                    if (COLUMN > knockbackPos.x && knockbackPos.x >= 0 && ROW > knockbackPos.y && knockbackPos.y >= 0 && gameBoard[knockbackPos.x, knockbackPos.y].Key == EMPTY.Key)
                    {
                        Console.WriteLine("넉백 가능");
                        List<KeyValuePair<Vector2, Vector2>> enemyMovingLst = movingLst[gameBoard[moving.Key.x, moving.Key.y].Key == 0 ? 1 : 0];
                        for (int j = 0; j < enemyMovingLst.Count; j++)
                        {
                            if (enemyMovingLst[j].Key == movingPos)
                            {
                                Console.WriteLine("넉백 전 유닛의 좌표 = " + enemyMovingLst[j].Key.x + ", " + enemyMovingLst[j].Key.y + " -> " + enemyMovingLst[j].Value.x + ", " + enemyMovingLst[j].Value.y);
                                enemyMovingLst[j] = new KeyValuePair<Vector2, Vector2>(knockbackPos, enemyMovingLst[j].Value - direction);
                                Console.WriteLine("넉백 후 유닛의 좌표 = " + enemyMovingLst[j].Key.x + ", " + enemyMovingLst[j].Key.y + " -> " + enemyMovingLst[j].Value.x + ", " + enemyMovingLst[j].Value.y);
                            }
                        }

                        enemyUnit = gameBoard[movingPos.x, movingPos.y];
                        enemyUnit.Value.MovedPos = new Vector2(knockbackPos.x, knockbackPos.y);
                        gameBoard[knockbackPos.x, knockbackPos.y] = enemyUnit;

                        if (movingPos != moving.Key)//시작위치가 아니면
                        {
                            savePos = movingPos;
                            gameBoard[movingPos.x, movingPos.y] = myUnit;
                            gameBoard[moving.Key.x, moving.Key.y] = EMPTY;
                        }
                    }
                    else// 넉백이 안되는 경우
                    {
                        Console.WriteLine("넉백 불가능");
                        enemyUnit = gameBoard[movingPos.x, movingPos.y];
                        movingPos -= direction;
                        savePos = movingPos;
                    }

                    myUnit.Value.Attack(enemyUnit.Value);

                    result = 2;
                    break;
                }
            }

            Console.WriteLine("결과값은" + result);
            switch (result)
            {
                case 0://단순이동
                    {
                        myUnit.Value.bMoving = true;
                        myUnit.Value.MovedPos = savePos;
                        gameBoard[moving.Value.x, moving.Value.y] = myUnit;
                        gameBoard[moving.Key.x, moving.Key.y] = EMPTY;
                    }
                    break;
                case 1://아군뒤로 이동
                    myUnit.Value.bMoving = true;
                    myUnit.Value.MovedPos = savePos;
                    //gameBoard[moving.Value.x, moving.Value.y] = myUnit;
                    gameBoard[moving.Key.x, moving.Key.y] = EMPTY;
                    break;
                case 2://공격
                    myUnit.Value.bMoving = true;
                    myUnit.Value.MovedPos = savePos;
                    break;
                case 3://이동을 하지못함
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 남은 Unit 중 움직이지 않은 Unit들을 반환
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private List<Vector2> GetAttackingUnit(byte index)
        {
            List<Vector2> answer = new List<Vector2>();
            foreach (Unit unit in players[index].units)
            {
                if (!unit.bMoving && unit.ID[0] == '0')
                {
                    RemoteUnit remoteUnit = (RemoteUnit)unit;

                    List<Vector2> waitedLst = Helper.GetMoving(remoteUnit.Pos, remoteUnit.attackDistance, remoteUnit.Direction, COLUMN, ROW);
                    List<int> validatedLst = new List<int>();
                    for (int i = 0; i < waitedLst.Count; i++)
                    {
                        if (gameBoard[waitedLst[i].x, waitedLst[i].y].Key != index)
                        {
                            validatedLst.Add(i);
                        }
                    }

                    if (validatedLst.Count != 0)
                    {
                        answer.Add(unit.Pos);
                    }
                }
            }

            return answer;
        }

        /// <summary>
        /// 움직이지 않음 원거리 Unit들의 공격
        /// 전송되는 Data
        ///     공격당한 Unit의 Data
        /// </summary>
        /// <param name="waitingUnitLst"></param>
        private void SendWaiting(List<Vector2> waitingUnitLst)
        {
            Console.WriteLine("Waiting Unit 좌표 = " + waitingUnitLst[0].x + ", " + waitingUnitLst[0].y);
            RemoteUnit attackingUnit = (RemoteUnit)gameBoard[waitingUnitLst[0].x, waitingUnitLst[0].y].Value;

            if (attackingUnit != null)
            {
                List<Vector2> waitedLst = Helper.GetMoving(attackingUnit.Pos, attackingUnit.attackDistance, attackingUnit.Direction, COLUMN, ROW);
                List<Vector2> validatedLst = new List<Vector2>();
                for (int i = 0; i < waitedLst.Count; i++)
                {
                    if (gameBoard[waitedLst[i].x, waitedLst[i].y].Key == (gameBoard[waitingUnitLst[0].x, waitingUnitLst[0].y].Key == 0 ? 1 : 0))
                    {
                        validatedLst.Add(waitedLst[i]);
                    }
                }

                // 공격할 적이 있는지 검사
                if (validatedLst.Count > 0)
                {
                    Random rnd = new Random(DateTime.Now.Millisecond);

                    int targetIndex = rnd.Next(validatedLst.Count);

                    Unit targetUnit = gameBoard[validatedLst[targetIndex].x, validatedLst[targetIndex].y].Value;
                    attackingUnit.Wait(targetUnit);

                    Packet waitedMsg = Packet.Create((short)PROTOCOL.WaitedUnit);

                    waitedMsg.Push(attackingUnit.Pos.x);
                    waitedMsg.Push(attackingUnit.Pos.y);
                    waitedMsg.Push(attackingUnit.HP);
                    waitedMsg.Push(targetUnit.Pos.x);
                    waitedMsg.Push(targetUnit.Pos.y);
                    waitedMsg.Push(targetUnit.HP);

                    Broadcast(waitedMsg);

                    Thread.Sleep(waitingTime);

                    if (targetUnit.HP <= 0)
                    {
                        players[0].DestoyUnit(targetUnit.Pos);
                        players[1].DestoyUnit(targetUnit.Pos);
                        gameBoard[targetUnit.Pos.x, targetUnit.Pos.y] = EMPTY;
                    }
                }
            }
        }

        /// <summary>
        /// Client에서 Turn 연출이 모두 완료 되었을 때 호출됨
        /// </summary>
        public void FinishTurn(Player player)
        {
            ChangePlayerState(player, PlayerState.FinishTurn);

            if (ReadyAllPlayers(PlayerState.FinishTurn)) return;

            movingLst[0].Clear();
            movingLst[1].Clear();

            foreach (Player playerTmp in players)
            {
                foreach (Unit unit in playerTmp.units)
                {
                    unit.bMoving = false;
                }
            }

            // Turn 종료
            EndTurn();
        }

        /// <summary>
        /// Turn을 종료, Gane이 끝났는지 확인하는 과정을 수행
        /// 전송되는 Data
        ///     변동되는 모든 Player의 Mana
        ///     진행된 Turn 수
        /// </summary>
        void EndTurn()
        {
            // Board 상태를 확인하여 게임이 끝났는지 검사
            int result = Helper.GetPlayResult(this.players);
            if (result != -1)
            {
                GameOver((byte)result);
                return;
            }

            timer = MAX;

            Packet msg = Packet.Create((short)PROTOCOL.FinishedTurn);
            msg.Push(++this.turnCount);
            Broadcast(msg);

            // Turn을 시작
            StartTurn();
        }

        /// <summary>
        /// 게임이 끝나고 이긴 Player Index를 모든 Client에게 전송하고
        /// Rank와 Score를 Update함
        /// 전송되는 Data
        ///     이긴 Player Index
        /// </summary>
        /// <param name="winner"></param>
        void GameOver(byte winner)
        {
            // 승리자 가리기
            Packet msg = Packet.Create((short)PROTOCOL.GameOver);
            msg.Push(winner);
            Broadcast(msg);

            DatabaseManager db = DatabaseManager.Instance;
            if (winner == 2)
            {
                db.UpdateRank(players[0].Name, false);
                db.UpdateRank(players[1].Name, false);
            }
            else
            {
                db.UpdateRank(players[0].Name, winner == 0 ? true : false);
                db.UpdateRank(players[1].Name, winner == 1 ? false : true);
            }

            // 방 제거
            Program.gameServer.roomManager.RemoveRoom(this);
        }

        /// <summary>
        /// 해당 방을 파괴함
        /// 단 bGame이 false일 경우 정상적인 파괴가 아님을 의미
        /// </summary>
        public void DestroyRoom()
        {
            bGame = false;
            Packet msg = Packet.Create((short)PROTOCOL.RemovedGame);
            Broadcast(msg);
        }
    }
}