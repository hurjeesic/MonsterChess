using FreeNet;
using FreeNetUnity;
using UnityEngine;

namespace MonsterChessClient
{
    public class NetworkManager : MonoBehaviour
    {
        FreeNetUnityService gameServer;
        string receivedMessage;

        [HideInInspector]
        public MonoBehaviour messageReceiver;

        GameObject loginButton;

        void Awake()
        {
            this.receivedMessage = "";

            // Network 통신을 위해 CFreeNetUnityService 객체를 추가
            this.gameServer = gameObject.AddComponent<FreeNetUnityService>();

            // 상태 변화(접속, 끊김 등)를 통보 받을 Delegate 설정
            this.gameServer.appCallbackOnStatusChanged += OnStatusChanged;

            // Packet 수신 Delegate 설정
            this.gameServer.appCallbackOnMessage += OnMessage;

            loginButton = GameObject.Find("LoginButton");
        }


        public void Connect()
        {
            this.gameServer.Connect("172.19.0.94", 7979);
        }

        public bool IsConnected()
        {
            return this.gameServer.IsConnected();
        }

        /// <summary>
        /// Network 상태 변경 시 호출될 Callback Method
        /// </summary>
        /// <param name="status"></param>
        void OnStatusChanged(NetworkEvent status)
        {
            switch (status)
            {
                // 접속 성공
                case NetworkEvent.connected:
                    {
                        LogManager.Log("on connected");
                        this.receivedMessage += "on connected\n";

                        loginButton.GetComponent<Login>().OnConnected();
                    }
                    break;

                // 연결 끊김
                case NetworkEvent.disconnected:
                    LogManager.Log("disconnected");
                    this.receivedMessage += "disconnected\n";

                    this.gameServer.Disconnect();
                    loginButton.GetComponent<Login>().OnDisconnected();
                    break;
            }
        }

        void OnMessage(Packet msg)
        {
            this.messageReceiver.SendMessage("OnReceive", msg);
        }

        public void Send(Packet msg)
        {
            this.gameServer.Send(msg);
        }
    }
}