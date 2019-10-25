using System.Data.Odbc;
using System;
using System.Data;
using System.Collections.Generic;

namespace MonsterChessServer
{
    public enum RANK
    {
        UnRank = 0,

        Private, PrivateFirstClass, Corporal, Sergeant, // 훈련병=이병, 일병, 상병, 병장
        StaffSergeant, SergeantFirstClass, FirstSergeant, CommandSergeantMajor, // 하사, 중사, 상사, 원사
        WarrantOfficer, SecondLieutenant, FirstLieutenant, Captain, // 준위, 소위, 중위, 대위
        Major, LieutenantColonel, Colonel, // 소령, 중령, 대령
        BrigadierGeneral, MajorGeneral, LieutenantGeneral, General, // 준장, 소장, 중장, 대장
        President
    }

    public struct UserInfo
    {
        public int Index { get; private set; }
        public string Name { get; private set; }
        public int Score { get; private set; }
        public RANK Rank { get; private set; }

        public UserInfo(int index, string name, int score, RANK rank)
        {
            Index = index;
            Name = name;
            Score = score;
            Rank = rank;
        }
    }

    public class DatabaseManager
    {
        OdbcConnection conn;
        OdbcCommand cmd;
        OdbcDataReader reader;

        int[] rankBoundary = { 9, 13, 16, 20 };

        private static DatabaseManager instance = null;
        public static DatabaseManager Instance { get { return instance == null ? new DatabaseManager() : instance; } }

        private DatabaseManager()
        {
            conn = null;
            cmd = null;
            reader = null;
        }

        /// <summary>
        /// Database에 접속하는 함수
        /// </summary>
        /// <returns>접속에 성공하면 true, 실패하면 false를 반환</returns>
        public bool Connect()
        {
            conn = null;
            cmd = null;

            try
            {
                conn = new OdbcConnection("Server=localhost;UID=root;PWD=JSHUR2015108211;Dsn=monsterchess;Port=3306"); // Connection
                conn.Open();

                if (conn.State == ConnectionState.Open) return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false;
        }

        public void Close()
        {
            if (conn != null) try { conn.Close(); } catch (Exception) { };
            if (cmd != null) try { cmd.Dispose(); } catch (Exception) { };
            if (reader != null) try { reader.Close(); } catch (Exception) { };
        }

        private void ShowUser() // 예제 학습을 위해 잠시 만든 함수
        {
            cmd = new OdbcCommand("select score, rank from user;", conn);

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetValue(i).ToString() + "\t");
                }
                Console.WriteLine();
            }

            string cmdStr = "SHOW tables";
            OdbcDataAdapter adapter = new OdbcDataAdapter(cmdStr, conn);
            DataTable table = new DataTable();
            adapter.Fill(table);

            // dataGridView.DataSource = table;
        }

        #region User 관리
        #region ID 또는 PWD 찾기
        public string FindUserID(string nick)
        {
            string result = null;

            try
            {
                string cmdStr = string.Format("select id from user where nick=\"{0}\";", nick);
                cmd = new OdbcCommand(cmdStr, conn);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetString(0);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public string FindUserPWD(string id)
        {
            string result = null;

            try
            {
                string cmdStr = string.Format("select pwd from user where id=\"{0}\";", id);
                cmd = new OdbcCommand(cmdStr, conn);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetString(0);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }
        #endregion ID 또는 PWD 찾기

        /// <summary>
        /// Id와 Pwd를 받아 User를 찾는 함수
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns>검색이 성공하면 검색된 객체를 반환, id가 존재하지 않으면 score = -2인 객체 반환, 아니면 score = -1인 객체 반환</returns>
        public UserInfo GetUser(string id, string pwd)
        {
            UserInfo result;

            try
            {
                string cmdStr = string.Format("select num from user where id=\"{0}\";", id);
                cmd = new OdbcCommand(cmdStr, conn);
                reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    result = new UserInfo(0, "", -2, RANK.UnRank);

                    return result;
                }

                cmdStr = string.Format("select num, nick, score, rank from user where id=\"{0}\" and pwd=\"{1}\";", id, pwd);
                cmd = new OdbcCommand(cmdStr, conn);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int index = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int score = reader.GetInt32(2);
                    RANK rank = (RANK)reader.GetInt32(3);
                    result = new UserInfo(index, name, score, rank);

                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            result = new UserInfo(0, "", -1, RANK.UnRank);

            return result;
        }

        /// <summary>
        /// User를 추가하는 함수
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns>중복된 id가 있으면 -1로 실패, 아니면 추가 후 1이면 성공 0이면 실패</returns>
        public int AddUser(string id, string pwd, string name)
        {
            try
            {
                string cmdStr = string.Format("insert into user (id, pwd, nick)" + "VALUES(\"{0}\", \"{1}\", \"{2}\")", id, pwd, name);
                cmd = new OdbcCommand(cmdStr, conn);
                cmd.ExecuteNonQuery();

                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 0;
        }

        /// <summary>
        /// User를 삭제하는 함수
        /// 사용되지 않음
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true면 성공 false면 실패</returns>
        public bool RemoveUser(string id)
        {
            try
            {
                string cmdStr = string.Format("delete from user where id=\"{0}\"", id);
                cmd = new OdbcCommand(cmdStr, conn);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

        /// <summary>
        /// User의 Rank를 반환하는 함수
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Rank를 구하면 Rank, 구하지 못하면 -1를 반환</returns>
        public int GetRank(string name)
        {
            int result;

            try
            {
                string cmdStr = string.Format("select rank from user where name=\"{0}\";", name);
                cmd = new OdbcCommand(cmdStr, conn);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    result = reader.GetInt32(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            result = -1;

            return result;
        }

        /// <summary>
        /// Game 결과에 따라 Rank와 Score를 Update하는 함수
        /// </summary>
        /// <param name="name"></param>
        /// <param name="win"></param>
        /// <returns>Rank Update에 성공하면 true, 실패하면 false를 반환</returns>
        public bool UpdateRank(string name, bool win)
        {
            int score, rank, winNum;

            try
            {
                string cmdStr = string.Format("select score, rank, win from user where name=\"{0}\"", name);
                cmd = new OdbcCommand(cmdStr, conn);
                reader = cmd.ExecuteReader();

                score = reader.GetInt32(0);
                rank = reader.GetInt32(1);
                winNum = reader.GetInt32(2);

                if (win)
                {
                    score += 5;
                    if (winNum < 0)
                    {
                        winNum = 1;
                    }
                    else
                    {
                        int index = rankBoundary[rankBoundary.Length - 1];
                        for (int i = 0; i < rankBoundary.Length; i++)
                        {
                            if (rank < rankBoundary[i])
                            {
                                index = rankBoundary[i];
                                break;
                            }
                        }

                        if (winNum == 2 + index)
                        {
                            rank++;
                            if (rank >= rankBoundary[rankBoundary.Length - 1])
                            {
                                rank = rankBoundary[rankBoundary.Length - 1];
                            }

                            winNum = 0;
                        }
                        else
                        {
                            winNum++;
                        }
                    }
                }
                else
                {
                    score -= 3;
                    if (winNum > 0)
                    {
                        winNum = -1;
                    }
                    else
                    {
                        if (winNum == -2)
                        {
                            rank--;
                            winNum = 0;
                        }
                        else
                        {
                            winNum--;
                        }
                    }
                }

                if (rank < 0)
                {
                    rank = 0;
                }

                if (score < 0)
                {
                    score = 0;
                }

                cmdStr = string.Format("update user set score = {0}, rank = {1}, win = {2} where name=\"{3}\"", score, rank, win, name);
                cmd = new OdbcCommand(cmdStr, conn);
                int result = cmd.ExecuteNonQuery();

                if (result == -1)
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }
        #endregion User 관리

        #region 친구 관리
        public List<UserInfo> GetFriend(string name)
        {
            List<UserInfo> result;

            try
            {
                string cmdStr = string.Format("select friendNick, score, rank from user, friend where friend.userNick=user.nick and friend.userNick=\"{0}\";", name);
                cmd = new OdbcCommand(cmdStr, conn);
                reader = cmd.ExecuteReader();

                result = new List<UserInfo>();
                int index = 0;
                while (reader.Read())
                {
                    string nick = reader.GetString(0);
                    int score = reader.GetInt32(1);
                    RANK rank = (RANK)reader.GetInt32(2);
                    result.Add(new UserInfo(index++, nick, score, rank));
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
        #endregion 친구 관리
    }
}