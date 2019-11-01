using FreeNet;
using UnityEngine;

namespace MonsterChessClient
{
    public class Main : MonoBehaviour
    {
        public NetworkManager networkManager;
        public Friend friend;
        public SelectSystem select;

        public void Enter()
        {
            this.networkManager.messageReceiver = this;
        }

        public void MoveSelect()
        {
            select.Enter();
        }

        public void MoveFriendList()
        {
            friend.Enter();
            GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Friend;
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
                // case PROTOCOL.Fight:
                // case PROTOCOL.Dictionary:
                // case PROTOCOL.Shop:
                // case PROTOCOL.Friend:
            }
        }
    }
}
