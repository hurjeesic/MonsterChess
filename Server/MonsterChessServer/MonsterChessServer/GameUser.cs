using FreeNet;
using UnitType;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MonsterChessServer
{
    /// <summary>
	/// 하나의 session객체를 나타냄
	/// </summary>
	public class GameUser : IPeer
    {
        UserToken token;

        public GameRoom battleRoom { get; private set; }

        Player player;

        int index;
        public string Name { get; private set; }

        public GameUser(UserToken token)
        {
            this.token = token;
            this.token.SetPeer(this);
        }

        void IPeer.OnMessage(Const<byte[]> buffer)
        {
            byte[] clone = new byte[1024];
            Array.Copy(buffer.Value, clone, buffer.Value.Length);
            Packet msg = new Packet(clone, this);
            Program.gameServer.EnqueuePacket(msg, this);
        }

        void IPeer.OnRemoved()
        {
            Console.WriteLine("The client disconnected.");

            Program.RemoveUser(this);
        }

        public void Send(Packet msg)
        {
            this.token.Send(msg);
        }

        void IPeer.Disconnect()
        {
            this.token.socket.Disconnect(false);
        }

        void IPeer.ProcessUserOperation(Packet msg)
        {
            PROTOCOL protocol = (PROTOCOL)msg.PopProtocolID();
            Console.WriteLine("protocol id " + protocol);
            switch (protocol)
            {
                case PROTOCOL.RequestLogin:
                    Login(msg);
                    break;
                case PROTOCOL.RequestRegistering:
                    Regist(msg);
                    break;
                case PROTOCOL.RequestFinding:
                    Find(msg);
                    break;
                case PROTOCOL.RequestFriendList:
                    SendFriendList(msg);
                    break;
                case PROTOCOL.RequestMatching:
                    Program.gameServer.RequestMatching(this);
                    break;
                case PROTOCOL.CancleMatching:
                    Program.gameServer.DisconnectUser(this);
                    break;
                case PROTOCOL.CompleteLoading:
                    this.battleRoom.CompleteLoading(this.player, GetUnitDic(msg));
                    break;
                case PROTOCOL.StartedGame:
                    this.battleRoom.BattleStart(this.player);
                    break;
                case PROTOCOL.RequestedMoving:
                    this.battleRoom.RequestMoving(this.player, GetMoving(msg));
                    break;
                case PROTOCOL.RequestedSummons:
                    this.battleRoom.RequestSummons(this.player, GetUnit(msg));
                    break;
                case PROTOCOL.FinishedTurn:
                    this.battleRoom.FinishTurn(this.player);
                    break;
                case PROTOCOL.RemovedGame:
                    Program.gameServer.roomManager.RemoveRoom(this.battleRoom);
                    this.battleRoom = null;
                    Program.RemoveUser(this);
                    break;
            }

            Packet.Destroy(msg);
        }

        #region User Service
        private void Login(Packet msg)
        {
            // -3은 데이터베이스 접속 실패, -2는 아이디 없음, -1은 비밀번호가 틀림, 그 외는 성공
            // 성공했을 때 이름과 랭크를 같이 보냄
            Packet loginMsg;
            DatabaseManager db = DatabaseManager.Instance;

            if (db.Connect())
            {
                string id = msg.PopString(), pwd = msg.PopString();
                // string id = AES.Decrypt(Program.key, msg.PopString()), pwd = AES.Decrypt(Program.key, msg.PopString());
                //Console.WriteLine("받아서 복호화한 아이디와 비밀번호 : " + id + ", " + pwd);
                UserInfo info = db.GetUser(id, pwd);
                switch (info.Score)
                {
                    case -2:
                        {
                            // Console.WriteLine("아이디가 존재하지 않습니다.");
                            loginMsg = Packet.Create((short)PROTOCOL.FailLogin);
                        }
                        break;
                    case -1:
                        {
                            // Console.WriteLine("비밀번호가 일치하지 않습니다.");
                            loginMsg = Packet.Create((short)PROTOCOL.FailLogin);
                        }
                        break;
                    default:
                        {
                            // Console.WriteLine("로그인에 성공하였습니다.");
                            bool flag = false;
                            foreach (GameUser user in Program.userList)
                            {
                                if (user.index == info.Index)
                                {
                                    flag = true;
                                    break;
                                }
                            }

                            if (flag)
                            {
                                Console.WriteLine("현재 접속되어 있습니다.");
                                loginMsg = Packet.Create((short)PROTOCOL.FailLogin);

                                loginMsg.Push(-4);
                            }
                            else
                            {
                                Console.WriteLine("Index = " + index + ", Name = " + info.Name + ", Score = " + info.Score + ", Rank = " + info.Rank);
                                index = info.Index;
                                Name = info.Name;
                                loginMsg = Packet.Create((short)PROTOCOL.SuccessLogin);

                                loginMsg.Push(info.Name);
                                loginMsg.Push(info.Score);
                                loginMsg.Push((int)info.Rank);
                            }
                        }
                        break;
                }

                if (info.Score < 0)
                {
                    loginMsg.Push(info.Score);
                }
            }
            else
            {
                Console.WriteLine("데이터베이스 접속에 실패하였습니다.");
                loginMsg = Packet.Create((short)PROTOCOL.FailLogin);
                loginMsg.Push(-3);
            }

            db.Close();

            Send(loginMsg);
            Packet.Destroy(loginMsg);
        }

        private void Regist(Packet msg)
        {
            Packet registMsg;
            DatabaseManager db = DatabaseManager.Instance;

            if (db.Connect())
            {
                // string id = AES.Decrypt(Program.key, msg.PopString()), pwd = AES.Decrypt(Program.key, msg.PopString()), name = AES.Decrypt(Program.key, msg.PopString());
                string id = msg.PopString(), pwd = msg.PopString(), name = msg.PopString();
                int result = db.AddUser(id, pwd, name);
                switch (result)
                {
                    case 0:
                        {
                            Console.WriteLine("가입하지 못했습니다.");
                            registMsg = Packet.Create((short)PROTOCOL.FailRegistering);
                            registMsg.Push(-1);
                        }
                        break;
                    case 1:
                        {
                            Console.WriteLine("가입되었습니다.");
                            registMsg = Packet.Create((short)PROTOCOL.SuccessRegistering);
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("가입하지 못했습니다.");
                            registMsg = Packet.Create((short)PROTOCOL.FailRegistering);
                            registMsg.Push(-2);
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("데이터베이스 접속에 실패하였습니다.");
                registMsg = Packet.Create((short)PROTOCOL.FailRegistering);
                registMsg.Push(-3);
            }

            db.Close();

            Send(registMsg);
            Packet.Destroy(registMsg);
        }

        private void Find(Packet msg)
        {
            Packet findMsg;
            DatabaseManager db = DatabaseManager.Instance;

            if (db.Connect())
            {
                int type = msg.PopInt32();
                string result;
                switch (type)
                {
                    case 0:
                        // string name = AES.Decrypt(Program.key, msg.PopString());
                        string name = msg.PopString();
                        result = db.FindUserID(name);
                        if (result == null)
                        {
                            findMsg = Packet.Create((short)PROTOCOL.FailFinding);
                            findMsg.Push(-1);
                        }
                        else
                        {
                            findMsg = Packet.Create((short)PROTOCOL.SuccessFinding);
                            // findMsg.Push(AES.Encrypt(Program.key, result));
                            findMsg.Push(result);
                        }
                        break;
                    case 1:
                        // string id = AES.Decrypt(Program.key, msg.PopString());
                        string id = msg.PopString();
                        result = db.FindUserPWD(id);
                        if (result == null)
                        {
                            findMsg = Packet.Create((short)PROTOCOL.FailFinding);
                            findMsg.Push(-1);
                        }
                        else
                        {
                            findMsg = Packet.Create((short)PROTOCOL.SuccessFinding);
                            // findMsg.Push(AES.Encrypt(Program.key, result));
                            findMsg.Push(result);
                        }
                        break;
                    default:
                        Console.WriteLine("전송 신호가 잘못됐습니다.");
                        result = null;
                        findMsg = Packet.Create((short)PROTOCOL.FailFinding);
                        findMsg.Push(-2);
                        break;
                }
            }
            else
            {
                Console.WriteLine("데이터베이스 접속에 실패하였습니다.");
                findMsg = Packet.Create((short)PROTOCOL.FailFinding);
                findMsg.Push(-3);
            }

            db.Close();

            Send(findMsg);
            Packet.Destroy(findMsg);
        }

        private void SendFriendList(Packet msg)
        {
            Packet friendMsg;
            DatabaseManager db = DatabaseManager.Instance;

            if (db.Connect())
            {
                // string name = AES.Decrypt(Program.key, msg.PopString());
                string name = msg.PopString();
                List<UserInfo> result = db.GetFriend(name);
                if (result == null)
                {
                    friendMsg = Packet.Create((short)PROTOCOL.FailFriendList);
                    friendMsg.Push(-1);
                }
                else
                {
                    friendMsg = Packet.Create((short)PROTOCOL.SuccessFriendList);
                    friendMsg.Push(result.Count);
                    for (int i = 0; i < result.Count; i++)
                    {
                        // friendMsg.Push(AES.Encrypt(Program.key, result[i].Name));
                        friendMsg.Push(result[i].Name);
                        friendMsg.Push((int)result[i].Rank);
                        friendMsg.Push(result[i].Score);
                    }
                }

                Console.WriteLine("전송되었습니다.");
            }
            else
            {
                Console.WriteLine("데이터베이스 접속에 실패하였습니다.");
                friendMsg = Packet.Create((short)PROTOCOL.FailFinding);
                friendMsg.Push(-3);
            }

            db.Close();

            Send(friendMsg);
            Packet.Destroy(friendMsg);
        }
        #endregion User Service

        #region In Game
        private Unit GetUnitType(string unitId)
        {
            Assembly creator = Assembly.GetExecutingAssembly();
            object unitObj = creator.CreateInstance("UnitType.Unit" + unitId);
            Unit unit = unitObj as Unit;

            return unit;
        }

        private Dictionary<Vector2, Unit> GetUnitDic(Packet msg)
        {
            Dictionary<Vector2, Unit> unitDic = new Dictionary<Vector2, Unit>();
            for (int i = 0; i < 6; i++)
            {
                Unit unit = GetUnitType(msg.PopString());
                int x = msg.PopInt32(), y = msg.PopInt32();
                Vector2 initPos = player.playerIndex == 0 ? new Vector2(x, y) : Helper.GetReversePosition(new Vector2(x, y), GameRoom.ROW);

                unitDic.Add(initPos, unit);
            }

            return unitDic;
        }

        private KeyValuePair<Vector2, Vector2> GetMoving(Packet msg)
        {
            Vector2 beginPos = new Vector2(msg.PopInt32(), msg.PopInt32());
            Vector2 targetPos = new Vector2(msg.PopInt32(), msg.PopInt32());
            if (player.playerIndex != 0)
            {
                beginPos = Helper.GetReversePosition(beginPos, GameRoom.ROW);
                targetPos = Helper.GetReversePosition(targetPos, GameRoom.ROW);
            }
            KeyValuePair<Vector2, Vector2> moving = new KeyValuePair<Vector2, Vector2>(beginPos, targetPos);

            return moving;
        }

        private KeyValuePair<Vector2, Unit> GetUnit(Packet msg)
        {
            KeyValuePair<Vector2, Unit> summonsUnit;

            int x = msg.PopInt32(), y = msg.PopInt32();
            Vector2 vector = player.playerIndex == 0 ? new Vector2(x, y) : Helper.GetReversePosition(new Vector2(x, y), GameRoom.ROW);

            Unit unit = GetUnitType(msg.PopString());

            summonsUnit = new KeyValuePair<Vector2, Unit>(vector, unit);

            return summonsUnit;
        }
        #endregion

        public void EnterRoom(Player player, GameRoom room)
        {
            this.player = player;
            this.battleRoom = room;
        }
    }
}