using FreeNet;
using System.Collections.Generic;

namespace FreeNetUnity
{
    public enum NetworkEvent : byte
    {
        connected, // 접속 완료
        disconnected, // 연결 끊김
        end // 끝
    }

    /// <summary>
    /// Network Engine에서 발생된 Event들을 Queueing 시킴
    /// Worder Thread와 Main Thread 양쪽에서 호출될 수 있으므로 Thread 동기화 처리를 적용함
    /// </summary>
    public class FreeNetEventManager
    {
        // 동기화 객체
        object csEvent;

        // Network Engine에서 발생된 Event들을 보관해 놓는 Queue
        Queue<NetworkEvent> networkEvents;

        // Server에서 받은 Packet들을 보관해놓는 Queue
        Queue<Packet> networkMessageEvents;

        public FreeNetEventManager()
        {
            this.networkEvents = new Queue<NetworkEvent>();
            this.networkMessageEvents = new Queue<Packet>();
            this.csEvent = new object();
        }

        public void EnqueueNetworkEvent(NetworkEvent eventType)
        {
            lock (this.csEvent)
            {
                this.networkEvents.Enqueue(eventType);
            }
        }

        public bool HasEvent()
        {
            lock (this.csEvent)
            {
                return this.networkEvents.Count > 0;
            }
        }

        public NetworkEvent DequeueNetworkEvent()
        {
            lock (this.csEvent)
            {
                return this.networkEvents.Dequeue();
            }
        }


        public bool HasMessage()
        {
            lock (this.csEvent)
            {
                return this.networkMessageEvents.Count > 0;
            }
        }

        public void EnqueueNetworkMessage(Packet buffer)
        {
            lock (this.csEvent)
            {
                this.networkMessageEvents.Enqueue(buffer);
            }
        }

        public Packet DequeueNetworkMessage()
        {
            lock (this.csEvent)
            {
                return this.networkMessageEvents.Dequeue();
            }
        }
    }
}