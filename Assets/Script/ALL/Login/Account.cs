using FreeNet;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class Account : MonoBehaviour
    {
        enum UserStage
        {
            RequestRegistering,
            EmptyField,
            NotConnected,
            Connected,
            FailRegistering,
            SuccessRegistering
        }

        public NetworkManager networkManager;
        public Login login;
        UserStage userState;

        public InputField id, pwd, nick;
        public Button[] btns;
        public GameObject process;

        float timer, totalTimer;

        public void Enter()
        {
            this.networkManager.messageReceiver = this;
            this.userState = UserStage.RequestRegistering;
            id.text = "";
            pwd.text = "";
            nick.text = "";
            process.SetActive(false);
            EnableObjects(true);
        }

        public void Cancle()
        {
            login.Enter();
            login.EnableObjects(true);
            this.userState = UserStage.RequestRegistering;
            GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneList.Login;
        }

        public void RequestRegistering()
        {
            process.SetActive(true);
            if (id.text == "" || pwd.text == "" || nick.text == "")
            {
                this.userState = UserStage.EmptyField;
                process.GetComponentsInChildren<Text>()[0].text = "모든 칸을 채워주세요.";
            }
            else
            {
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

            Packet msg = Packet.Create((short)PROTOCOL.RequestRegistering);
            msg.Push(id.text);
            msg.Push(pwd.text);
            msg.Push(nick.text);
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
                case UserStage.RequestRegistering:
                    break;
                case UserStage.EmptyField:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestRegistering;
                    }
                    break;
                case UserStage.NotConnected:
                    if (totalTimer > 10)
                    {
                        process.GetComponentsInChildren<Text>()[0].text = "서버 무응답";
                        if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                        {
                            this.userState = UserStage.RequestRegistering;
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
                case UserStage.FailRegistering:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestRegistering;
                    }
                    break;
                case UserStage.SuccessRegistering:
                    if (Input.touchCount > 0 || Input.GetMouseButtonUp(0))
                    {
                        EnableObjects(true);
                        process.SetActive(false);
                        this.userState = UserStage.RequestRegistering;
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
                case PROTOCOL.FailRegistering:
                    {
                        process.SetActive(true);
                        int result = msg.PopInt32();
                        switch (result)
                        {
                            case -3:
                                process.GetComponentsInChildren<Text>()[0].text = "DB 연결 실패";
                                break;
                            case -2:
                                process.GetComponentsInChildren<Text>()[0].text = "알 수 없는 문제";
                                break;
                            case -1:
                                process.GetComponentsInChildren<Text>()[0].text = "아이디 또는 이름 중복";
                                break;
                        }

                        this.userState = UserStage.FailRegistering;
                    }
                    break;
                case PROTOCOL.SuccessRegistering:
                    {
                        process.SetActive(true);
                        process.GetComponentsInChildren<Text>()[0].text = "가입 완료";
                        this.userState = UserStage.SuccessRegistering;
                    }
                    break;
            }
        }
    }
}