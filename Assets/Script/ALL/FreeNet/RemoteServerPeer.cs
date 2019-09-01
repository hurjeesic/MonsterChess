using FreeNet;
using System;

namespace FreeNetUnity
{
    public class RemoteServerPeer : IPeer
    {
        public UserToken token { get; private set; }
        WeakReference freeNetEventManager;

        public RemoteServerPeer(UserToken token)
        {
            this.token = token;
            this.token.SetPeer(this);
        }

        public void SetEventManager(FreeNetEventManager eventManager)
        {
            this.freeNetEventManager = new WeakReference(eventManager);
        }

        /// <summary>
        /// Message를 수신했을 때 호출됨
        /// Parameter로 넘어온 Buffer는 Worker Thread에서 재사용 되므로 복사한 뒤 Application으로 넘겨줌
        /// </summary>
        /// <param name="buffer"></param>
        void IPeer.OnMessage(Const<byte[]> buffer)
        {
            // Buffer를 복사한 뒤 Packet Class로 감싼 뒤 넘겨줌
            // Packet Class 내부에서는 참조로만 들고 있음
            byte[] appBuffer = new byte[buffer.Value.Length];
            Array.Copy(buffer.Value, appBuffer, buffer.Value.Length);
            Packet msg = new Packet(appBuffer, this);
            (this.freeNetEventManager.Target as FreeNetEventManager).EnqueueNetworkMessage(msg);
        }

        void IPeer.OnRemoved()
        {
            (this.freeNetEventManager.Target as FreeNetEventManager).EnqueueNetworkEvent(NetworkEvent.disconnected);
        }

        void IPeer.Send(Packet msg)
        {
            this.token.Send(msg);
        }

        void IPeer.Disconnect()
        {

        }

        void IPeer.ProcessUserOperation(Packet msg)
        {

        }
    }
}
