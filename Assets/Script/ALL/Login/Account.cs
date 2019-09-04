using FreeNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class Account : MonoBehaviour
    {
        enum UserStage
        {
            RequestRegistering,
            NotConnected,
            Connected,
        }

        public NetworkManager networkManager;
        public Login login;
        UserStage userState;

        public InputField id, pwd, name;
        public Button[] btns;

        float timer, totalTimer;

        // Use this for initialization
        void Start()
        {

        }

        public void Enter()
        {
            this.networkManager.messageReceiver = this;
            this.userState = UserStage.RequestRegistering;
        }

        public void Cancle()
        {
            login.Enter();
            login.EnableButtons(true);
            this.userState = UserStage.RequestRegistering;
            GameObject.Find("SceneManager").GetComponent<SceneManager>().Present = SceneManager.SceneList.Login;
        }

        public void RequestRegistering()
        {
            this.userState = UserStage.NotConnected;
            EnableButtons(false);
        }

        public void Connect()
        {
            if (!this.networkManager.IsConnected())
            {
                this.networkManager.Connect();
            }
            else
            {
                EnableButtons(true);
                OnConnected();
                totalTimer = 0;
            }
        }

        public void OnConnected()
        {
            timer = 0;
            // Debug.Log("Connected");

            Packet msg = Packet.Create((short)PROTOCOL.RequestRegistering);
            msg.Push(id.text);
            msg.Push(pwd.text);
            msg.Push(name.text);
            this.networkManager.Send(msg);
            this.userState = UserStage.Connected;
        }

        public void EnableButtons(bool enable)
        {
            foreach (Button btn in btns) btn.GetComponent<Button>().enabled = enable;
        }

        void OnGUI()
        {
            switch (this.userState)
            {
                case UserStage.RequestRegistering:
                    break;

                case UserStage.NotConnected:
                    GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Connecting...");
                    if (timer > 3)
                    {
                        Connect();
                        // Debug.Log("Connecting...");
                        timer = 0;
                    }

                    if (totalTimer > 10)
                    {
                        Debug.Log("서버가 응답하지 않습니다.");
                        this.userState = UserStage.RequestRegistering;
                        EnableButtons(true);
                        totalTimer = 0;
                    }

                    timer += Time.deltaTime;
                    totalTimer += Time.deltaTime;
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
                        int result = msg.PopInt32();
                        switch (result)
                        {
                            case -3:
                                Debug.Log("서버의 데이터베이스에 문제가 생겼습니다.");
                                break;
                            case -2:
                                Debug.Log("알 수 없는 이유가 가입하지 못하였습니다.");
                                break;
                            case -1:
                                Debug.Log("중복되는 아이디 또는 이름이 있습니다.");
                                break;
                        }
                    }
                    break;
                case PROTOCOL.SuccessRegistering:
                    {
                        Debug.Log("가입되었습니다.");
                        Cancle();
                    }
                    break;
            }
        }
    }
}