using FreeNet;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class Find : MonoBehaviour
    {
        enum UserStage
        {
            RequestFinding,
            EmptyField,
            NotConnected,
            Connected,
            FailFinding,
            SuccessFinding
        }

        public NetworkManager networkManager;
        public Login login;
        UserStage userState;

        public InputField nick, id;
        public Button[] btns;
        public GameObject process;

        float timer, totalTimer;
        int signal;

        // Use this for initialization
        void Start()
        {
            signal = 0;
        }

        public void Enter()
        {
            this.networkManager.messageReceiver = this;
            this.userState = UserStage.RequestFinding;
            id.text = "";
            nick.text = "";
            process.SetActive(false);
        }

        public void Cancle()
        {
            login.Enter();
            login.EnableObjects(true);
            this.userState = UserStage.RequestFinding;
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Login;
        }

        public void RequestFindID()
        {
            process.SetActive(true);
            if (nick.text == "")
            {
                this.userState = UserStage.EmptyField;
                process.GetComponentsInChildren<Text>()[0].text = "닉네임을 입력해세요.";
            }
            else
            {
                signal = 0;
                this.userState = UserStage.NotConnected;
            }

            EnableObjects(false);
        }

        public void RequestFindPWD()
        {
            process.SetActive(true);
            if (id.text == "")
            {
                this.userState = UserStage.EmptyField;
                process.GetComponentsInChildren<Text>()[0].text = "아이디를 채워주세요.";
            }
            else
            {
                signal = 1;
                this.userState = UserStage.NotConnected;
            }

            EnableObjects(false);
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
                process.SetActive(false);
            }
        }

        public void OnConnected()
        {
            timer = 0;
            totalTimer = 0;

            Packet msg = Packet.Create((short)PROTOCOL.RequestFinding);
            msg.Push(signal);
            switch (signal)
            {
                case 0:
                    msg.Push(nick.text);
                    break;
                case 1:
                    msg.Push(id.text);
                    break;
            }
            this.networkManager.Send(msg);
            this.userState = UserStage.Connected;
        }

        public void EnableObjects(bool enable)
        {
            foreach (Button btn in btns) btn.GetComponent<Button>().enabled = enable;
        }

        void Update()
        {
            switch (this.userState)
            {
                case UserStage.RequestFinding:
                    break;
                case UserStage.EmptyField:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestFinding;
                    }
                    break;
                case UserStage.NotConnected:
                    if (totalTimer > 10)
                    {
                        process.GetComponentsInChildren<Text>()[0].text = "서버 무응답";
                        if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                        {
                            this.userState = UserStage.RequestFinding;
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
                case UserStage.FailFinding:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestFinding;
                    }
                    break;
                case UserStage.SuccessFinding:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestFinding;
                        Cancle();
                    }
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
                case PROTOCOL.FailFinding:
                    {
                        process.SetActive(true);
                        int result = msg.PopInt32();
                        switch (result)
                        {
                            case -3:
                                process.GetComponentsInChildren<Text>()[0].text = "DB 연결 실패";
                                break;
                            case -2:
                                // Debugging - 거의 나타나지 않음
                                process.GetComponentsInChildren<Text>()[0].text = "잘못된 신호";
                                break;
                            case -1:
                                switch (signal)
                                {
                                    case 0:
                                        process.GetComponentsInChildren<Text>()[0].text = "없는 닉네임입니다.";
                                        break;
                                    case 1:
                                        process.GetComponentsInChildren<Text>()[0].text = "없는 아이디입니다.";
                                        break;
                                }
                                break;
                        }

                        this.userState = UserStage.FailFinding;
                    }
                    break;
                case PROTOCOL.SuccessFinding:
                    {
                        process.SetActive(true);
                        switch (signal)
                        {
                            case 0:
                                process.GetComponentsInChildren<Text>()[0].text = "아이디 : " + msg.PopString();
                                break;
                            case 1:
                                process.GetComponentsInChildren<Text>()[0].text = "비밀번호 : " + msg.PopString();
                                break;
                        }
                        this.userState = UserStage.SuccessFinding;
                    }
                    break;
            }
        }
    }
}