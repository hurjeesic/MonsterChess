#define DEBUG
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
            EmptyField,
            NotConnected,
            Connected,
            FailLogin,
            SuccessLogin,
            WaitingMatching
        }

        public Main main;
        public Account account;
        public Find find;

        public NetworkManager networkManager;
        UserStage userState;

        float timer, totalTimer;

        public InputField idTxt, pwdTxt;
        public Button[] btns;
        public GameObject process;

        void Start()
        {
            this.userState = UserStage.RequestLogin;

            Enter();
        }

        public void Enter()
        {
            this.networkManager.messageReceiver = this;
            process.SetActive(false);
        }

        public void Connect()
        {
            if (!this.networkManager.IsConnected())
            {
                this.networkManager.Connect();
            }
            else
            {
                EnableObjects(true);
                OnConnected();
                totalTimer = 0;
            }
        }

        void Update()
        {
            switch (this.userState)
            {
                case UserStage.RequestLogin:
                    break;

                case UserStage.EmptyField:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestLogin;
                    }
                    break;

                case UserStage.NotConnected:
                    if (totalTimer > 10)
                    {
                        process.GetComponentsInChildren<Text>()[0].text = "서버 무응답";
                        if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                        {
                            this.userState = UserStage.RequestLogin;
                            EnableObjects(true);
                            process.SetActive(false);
                            totalTimer = 0;
                        }
                    }
                    else
                    {
                        if (timer > 2)
                        {
                            process.GetComponentsInChildren<Text>()[0].text = "연결 중...";
                        }
                        else if (timer > 1)
                        {
                            process.GetComponentsInChildren<Text>()[0].text = "연결 중..";
                        }
                        else if (timer > 0)
                        {
                            process.GetComponentsInChildren<Text>()[0].text = "연결 중.";
                        }

                        if (timer > 3)
                        {
                            Connect();

                            timer = 0;
                        }
                        else
                        {
                            timer += Time.deltaTime;
                        }

                        totalTimer += Time.deltaTime;
                    }
                    break;

                case UserStage.Connected:
                    break;

                case UserStage.FailLogin:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestLogin;
                    }
                    break;

                case UserStage.SuccessLogin:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestLogin;

                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
                        main.Enter();
                    }
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

        public void EnableObjects(bool enable)
        {
            foreach (Button btn in btns) btn.GetComponent<Button>().enabled = enable;
        }

        public void RequestLogin()
        {
            process.SetActive(true);
            if (idTxt.text == "" || pwdTxt.text == "")
            {
                this.userState = UserStage.EmptyField;
                if (idTxt.text == "")
                {
                    process.GetComponentsInChildren<Text>()[0].text = "아이디를 입력해세요.";
                }
                else
                {
                    process.GetComponentsInChildren<Text>()[0].text = "비밀번호를 입력해세요.";
                }
            }
            else
            {
                this.userState = UserStage.NotConnected;
            }
            
            EnableObjects(false);
        }

        public void RequestMatching()
        {
            this.userState = UserStage.WaitingMatching;

            Packet msg = Packet.Create((short)PROTOCOL.RequestMatching);
            this.networkManager.Send(msg);
        }

        public void FindUser()
        {
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.FindUser;
            find.Enter();
            EnableObjects(false);
        }

        public void Account()
        {
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Account;
            account.Enter();
            EnableObjects(false);
        }

        /// <summary>
        /// Server에 접속이 완료되면 호출됨
        /// </summary>
        public void OnConnected()
        {
            timer = 0;
            #if DEBUG
                Debug.Log("Connected");
            #endif

            Packet msg = Packet.Create((short)PROTOCOL.RequestLogin);
            
            msg.Push(idTxt.GetComponent<InputField>().text);
            msg.Push(pwdTxt.GetComponent<InputField>().text);
            this.networkManager.Send(msg);
            this.userState = UserStage.Connected;
        }

        public void OnDisconnected()
        {
            this.userState = UserStage.RequestLogin;
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Login;
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
                case PROTOCOL.FailLogin:
                    {
                        process.SetActive(true);
                        int result = msg.PopInt32();
                        switch (result)
                        {
                            case -4:
                                process.GetComponentsInChildren<Text>()[0].text = "이미 접속 중";
                                break;
                            case -3:
                                process.GetComponentsInChildren<Text>()[0].text = "서버 DB 문제";
                                break;
                            case -2:
                                process.GetComponentsInChildren<Text>()[0].text = "없는 아이디입니다.";
                                break;
                            case -1:
                                process.GetComponentsInChildren<Text>()[0].text = "비밀번호가 틀렸습니다.";
                                break;
                        }

                        this.userState = UserStage.FailLogin;
                    }
                    break;
                case PROTOCOL.SuccessLogin:
                    {
                        UserData.User.Instance.Initialize(msg.PopString(), msg.PopInt32(), msg.PopInt32());
                        process.GetComponentsInChildren<Text>()[0].text = UserData.User.Instance.name + "님 환영합니다.";

                        this.userState = UserStage.SuccessLogin;
                    }
                    break;
                case PROTOCOL.StartLoading:
                    {
                        byte playerIndex = msg.PopByte();

                        //this.battleRoom.gameObject.SetActive(true);
                        //this.battleRoom.StartLoading(playerIndex);
                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Select;
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }
    }
}