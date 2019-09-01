using FreeNet;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class Login : MonoBehaviour
    {
        enum UserStage
        {
            RequestLogin,
            NotConnected,
            Connected,
            WaitingMatching
        }

        BattleRoom battleRoom;

        public GameObject networkManagerObject;
        NetworkManager networkManager;
        UserStage userState;

        float timer, totalTimer;

        public InputField id, pwd;

        // Use this for initialization
        void Start()
        {
            this.userState = UserStage.RequestLogin;

            if (networkManagerObject != null) networkManager = networkManagerObject.GetComponent<NetworkManager>();
        }

        public void Enter()
        {
            this.networkManager.messageReceiver = this;

            if (!this.networkManager.IsConnected())
            {
                this.networkManager.Connect();
            }
            else
            {
                OnConnected();
                totalTimer = 0;
            }
        }

        void OnGUI()
        {
            switch (this.userState)
            {
                case UserStage.RequestLogin:
                    gameObject.GetComponent<Button>().enabled = true;
                    break;

                case UserStage.NotConnected:
                    GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Connecting...");
                    gameObject.GetComponent<Button>().enabled = false;
                    if (timer > 3)
                    {
                        Enter();
                        // Debug.Log("Connecting...");
                        timer = 0;
                    }

                    if (totalTimer > 10)
                    {
                        Debug.Log("서버가 응답하지 않습니다.");
                        this.userState = UserStage.RequestLogin;
                    }

                    timer += Time.deltaTime;
                    totalTimer += Time.deltaTime;
                    break;

                case UserStage.Connected:
                    gameObject.GetComponent<Button>().enabled = true;
                    break;

                case UserStage.WaitingMatching:
                    if (GUI.Button(new Rect(Screen.width - 100, Screen.height / 2 - 25, 100, 50), "cancle"))
                    {
                        this.userState = UserStage.Connected;

                        Packet msg = Packet.Create((short)PROTOCOL.CancleMatching);
                        this.networkManager.Send(msg);
                    }
                    break;
            }
        }

        public void RequestLogin()
        {
            this.networkManagerObject.SetActive(true);

            this.userState = UserStage.NotConnected;
        }

        public void RequestMatching()
        {
            this.userState = UserStage.WaitingMatching;

            Packet msg = Packet.Create((short)PROTOCOL.RequestMatching);
            this.networkManager.Send(msg);
        }

        /// <summary>
        /// Server에 접속이 완료되면 호출됨
        /// </summary>
        public void OnConnected()
        {
            timer = 0;
            // Debug.Log("Connected");

            Packet msg = Packet.Create((short)PROTOCOL.RequestLogin);
            
            msg.Push(id.GetComponent<InputField>().text);
            msg.Push(pwd.GetComponent<InputField>().text);
            this.networkManager.Send(msg);
            this.userState = UserStage.Connected;
        }

        public void OnDisconnected()
        {
            this.userState = UserStage.RequestLogin;
            GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneManager.SceneList.Login;
        }

        /// <summary>
        /// Packet을 수신 했을 때 호출됨
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="msg"></param>
        public void OnReceive(Packet msg)
        {
            // 제일 먼저 프로토콜 아이디를 꺼내온다.
            PROTOCOL protocolID = (PROTOCOL)msg.PopProtocolID();

            switch (protocolID)
            {
                case PROTOCOL.FailLogin:
                    {
                        int result = msg.PopInt32();
                        switch (result)
                        {
                            case -4:
                                Debug.Log("이미 접속되어 있습니다.");
                                break;
                            case -3:
                                Debug.Log("서버의 데이터베이스에 문제가 생겼습니다.");
                                break;
                            case -2:
                                Debug.Log("아이디가 존재하지 않습니다.");
                                break;
                            case -1:
                                Debug.Log("비밀번호가 일치하지 않습니다.");
                                break;
                        }
                    }

                    this.userState = UserStage.RequestLogin;
                    break;
                case PROTOCOL.SuccessLogin:
                    {
                        Debug.Log("로그인에 성공하였습니다.");
                        UserData.User.Instance.Initialize(msg.PopString(), msg.PopInt32(), msg.PopInt32());
                        GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneManager.SceneList.Main;
                    }
                    break;
                case PROTOCOL.StartLoading:
                    {
                        byte playerIndex = msg.PopByte();

                        //this.battleRoom.gameObject.SetActive(true);
                        //this.battleRoom.StartLoading(playerIndex);
                        GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneManager.SceneList.Select;
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}