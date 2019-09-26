using FreeNet;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class NextButton : MonoBehaviour
    {
        public NetworkManager networkManager;
        public Text btnTxt;
        public Button cancleBtn;
        public GameObject nextBtn;
        public GameObject matchCancleBtn;
        List<Vector2> unitPos = new List<Vector2>();

        public void Enter()
        {
            networkManager.messageReceiver = this;
            nextBtn.SetActive(true);
            matchCancleBtn.SetActive(false);
        }

        public void StartMatching()
        {
            int count = 0;
            unitPos.Clear();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Data.Instance.board[i, j] != null)
                    {
                        unitPos.Add(new Vector2(i, j));
                        count++;
                    }
                }
            }

            if (count == 6)
            {
                Data.Instance.bSommons = false; // 다음 씬으로 넘어갈때 소환을 끈다.
                
                btnTxt.text = "매칭 취소";
                nextBtn.SetActive(false);
                matchCancleBtn.SetActive(true);
                cancleBtn.enabled = false;
                Packet msg = Packet.Create((short)PROTOCOL.RequestMatching);
                this.networkManager.Send(msg);
            }
            else
            {
                Debug.Log("리스트의 갯수가 모자랍니다. " + count + "개");
                count = 0;
            }
        }

        public void CancleMatching()
        {
            btnTxt.text = "확인";
            nextBtn.SetActive(true);
            matchCancleBtn.SetActive(false);
            cancleBtn.enabled = true;
            Packet msg = Packet.Create((short)PROTOCOL.CancleMatching);
            this.networkManager.Send(msg);
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
                case PROTOCOL.StartLoading:
                    {
                        byte playerIndex = msg.PopByte();

                        //this.battleRoom.gameObject.SetActive(true);
                        //this.battleRoom.StartLoading(playerIndex);
                        Packet loadingMsg = Packet.Create((short)PROTOCOL.CompleteLoading);
                        for (int i = 0; i < 6; i++)
                        {
                            loadingMsg.Push((int)unitPos[i].x);
                            loadingMsg.Push((int)unitPos[i].y);
                            loadingMsg.Push(Data.Instance.board[(int)unitPos[i].x, (int)unitPos[i].y]);
                        }
                        this.networkManager.Send(loadingMsg);
                    }
                    break;

                case PROTOCOL.StartedGame:
                    {
                        // Play 씬으로 넘어가야함
                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Play;
                        Debug.Log("게임 시작");
                    }
                    break;
            }
        }
    }
}