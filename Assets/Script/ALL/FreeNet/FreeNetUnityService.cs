using FreeNet;
using UnityEngine;
using System;
using System.Net;

namespace FreeNetUnity
{
    /// <summary>
    /// FreeNet Engine과 Unity Application을 이어주는 Class
    /// FreeNet Engine에서 받은 접속 Event, Message 수신 Event 등을 Application으로 전달하는 역할을 하는데
    /// MonoBehaviour를 상속받아 Unity Application과 동일한 Thread에서 작동되도록 구현
    /// 따라서 이 Class의 Callback Method에서 Unity Object에 접근할 때 별도의 동기화 처리는 하지 않아도 됨
    /// </summary>
    public class FreeNetUnityService : MonoBehaviour
    {
        bool isConnect = false;
        FreeNetEventManager eventManager;

        // 연결된 게임 서버 객체
        IPeer gameServer;

        // TCP 통신을 위한 서비스 객체
        NetworkService service;

        // 접속 완료시 호출되는 Delegate, Application에서 Callback Method를 설정하여 사용
        public delegate void StatusChangedHandler(NetworkEvent status);
        public StatusChangedHandler appCallbackOnStatusChanged;

        // Network Massage 수신시 호출되는 Deleagte, Application에서 Callback Method를 설정하여 사용
        public delegate void MessageHandler(Packet msg);
        public MessageHandler appCallbackOnMessage;

        void Awake()
        {
            PacketBufferManager.Initialize(10);
            this.eventManager = new FreeNetEventManager();
        }

        public void Connect(string host, int port)
        {
            if (IsConnected())
            {
                Debug.LogError("Already connected.");
                return;
            }

            // CNetworkService 객체는 Message의 비동기 송, 수신 처리를 수행
            this.service = new NetworkService();

            // Endpoint 정보를 갖고있는 Connector 생성, 만들어둔 NetworkService 객체를 넣어줌
            Connector connector = new Connector(service);
            // 접속 성공 시 호출될 Callback Method 지정
            connector.connectedCallback += OnConnectedGameServer;
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(host), port);
            connector.Connect(endpoint);
        }

        public bool IsConnected()
        {
            return this.gameServer != null;

        }

        public void Disconnect()
        {
            this.service = null;
            this.gameServer = null;
        }

        /// <summary>
        /// 접속 성공시 호출될 Callback Method
        /// </summary>
        /// <param name="serverToken"></param>
        void OnConnectedGameServer(UserToken serverToken)
        {
            this.gameServer = new RemoteServerPeer(serverToken);
            ((RemoteServerPeer)this.gameServer).SetEventManager(this.eventManager);

            // Unity Application으로 Event를 넘겨주기 위해서 Manager에 Queueing 시켜 줌
            this.eventManager.EnqueueNetworkEvent(NetworkEvent.connected);
        }

        /// <summary>
        /// Network에서 발생하는 모든 Event를 Client에게 알려주는 역할을 Update에서 진행
        /// FreeNet Engine의 Message 송수신 처리는 Worker Thread에서 수행되지만 Unity의 Logic 처리는 Main Thread에서 수행되므로
        /// Queueing 처리를 통하여 Main Thread에서 모든 Logic 처리가 이루어지도록 구성
        /// </summary>
        void Update()
        {
            // 수신된 Message에 대한 Callback
            if (this.eventManager.HasMessage())
            {
                Packet msg = this.eventManager.DequeueNetworkMessage();
                if (this.appCallbackOnMessage != null)
                {
                    this.appCallbackOnMessage(msg);
                }
            }

            // Network 발생 Event에 대한 Callback
            if (this.eventManager.HasEvent())
            {
                NetworkEvent status = this.eventManager.DequeueNetworkEvent();
                if (this.appCallbackOnStatusChanged != null)
                {
                    this.appCallbackOnStatusChanged(status);
                }
            }
        }

        public void Send(Packet msg)
        {
            try
            {
                this.gameServer.Send(msg);
                Packet.Destroy(msg);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        /// <summary>
        /// 정상적인 종료 시에는 OnApplicationQuit Method에서 Disconnect를 호출해 줘야 Unity가 Hang 되지 않음
        /// </summary>
        void OnApplicationQuit()
        {
            if (this.gameServer != null)
            {
                ((RemoteServerPeer)this.gameServer).token.Disconnect();
            }
        }
    }

}