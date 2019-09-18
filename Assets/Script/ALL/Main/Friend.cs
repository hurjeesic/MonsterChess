#define DEBUG
using FreeNet;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UserData;

namespace MonsterChessClient
{
    public class Friend : MonoBehaviour
    {
        public struct UserInfo
        {
            public string Name { get; private set; }
            public int Score { get; private set; }
            public RANK Rank { get; private set; }

            public UserInfo(string name, int score, RANK rank)
            {
                Name = name;
                Score = score;
                Rank = rank;
            }
        }

        enum UserStage
        {
            NotState,
            NotConnected,
            Connected,
        }

        public NetworkManager networkManager;
        public Main main;
        public GameObject contents;
        public GameObject friendInfo;
        UserStage userState;

        public Button[] btns;

        float timer;

        List<UserInfo> friends;
        List<GameObject> friendsObj;

        public void Enter()
        {
            userState = UserStage.NotState;
            this.networkManager.messageReceiver = this;

            if (friendsObj != null)
            {
                foreach (GameObject gameObj in friendsObj)
                {
                    DestroyImmediate(gameObj);
                }

                friends.Clear();
                friendsObj.Clear();
            }
            else
            {
                friends = new List<UserInfo>();
                friendsObj = new List<GameObject>();
            }

            this.userState = UserStage.NotConnected;
        }

        public void MoveMain()
        {
            main.Enter();
            this.userState = UserStage.NotState;
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
        }

        public void Connect()
        {
            if (!this.networkManager.IsConnected())
            {
                this.networkManager.Connect();
            }
            else
            {
                timer = 0;

                Packet msg = Packet.Create((short)PROTOCOL.RequestFriendList);
                msg.Push(User.Instance.name);

                this.networkManager.Send(msg);
                this.userState = UserStage.Connected;
            }
        }

        public void EnableObjects(bool enable)
        {
            foreach (Button btn in btns) btn.GetComponent<Button>().enabled = enable;
        }

        void Update()
        {
            switch (this.userState)
            {
                case UserStage.NotConnected:
                    if (timer > 3)
                    {
                        Connect();
                        #if DEBUG
                            Debug.Log("연결 시도 중...");
                        #endif

                        timer = 0;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case UserStage.Connected:
                    break;
            }
        }

        void OnGUI()
        {
            switch (this.userState)
            {
                case UserStage.NotConnected:
                    if (timer >= 2) GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), "<color=#000>연결 시도 중...</color>");
                    else if (timer >= 1) GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), "<color=#000>연결 시도 중..</color>");
                    else if (timer >= 0) GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 100), "<color=#000>연결 시도 중.</color>");
                    break;
            }
        }

        /// <summary>
        /// Packet을 수신 했을 때 호출됨
        /// </summary>
        /// <param name="msg"></param>
        public void OnReceive(Packet msg)
        {
            // 제일 먼저 프로토콜 아이디를 꺼내온다.
            PROTOCOL protocolID = (PROTOCOL)msg.PopProtocolID();

            switch (protocolID)
            {
                case PROTOCOL.FailFriendList:
                    {
                        int result = msg.PopInt32();
                        #if DEBUG
                            switch (result)
                            {
                                case -3:
                                    Debug.Log("DB 연결 실패");
                                    break;
                                case -1:
                                    Debug.Log("DB Data 없음");
                                    break;
                            }
                        #endif

                        this.userState = UserStage.NotState;
                        main.Enter();
                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
                    }
                    break;
                case PROTOCOL.SuccessFriendList:
                    {
                        int count = msg.PopInt32();
                        int index = 0;
                        for (int i = 0; i < count; i++)
                        {
                            friends.Add(new UserInfo(msg.PopString(), msg.PopInt32(), (RANK)msg.PopInt32()));
                            friendsObj.Add(Instantiate(friendInfo, contents.transform));

                            Text[] texts = friendsObj[i].GetComponentsInChildren<Text>();
                            texts[0].text = friends[friends.Count - 1].Name;
                            texts[1].text = friends[friends.Count - 1].Rank.ToString();
                            texts[2].text = friends[friends.Count - 1].Score.ToString();
                        }
                    }
                    break;
            }
        }
    }
}