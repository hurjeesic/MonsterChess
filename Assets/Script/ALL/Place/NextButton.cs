using FreeNet;
using System;
using System.Collections.Generic;
using UnitType;
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
        public GameObject stateObj;
        List<Vector2> unitPos = new List<Vector2>();

        public BoardUpdate playingObj;

        public void Enter()
        {
            networkManager.messageReceiver = this;
            nextBtn.SetActive(true);
            matchCancleBtn.SetActive(false);
            stateObj.SetActive(false);

            int size = Data.Instance.units.Length - 1;
            for (int i = 0; i < size; i++)
            {
                RawImage summonBtnImg = GameObject.Find(i.ToString()).GetComponent<RawImage>();
                summonBtnImg.texture = Resources.Load("Image/ButtonUnit/" + Data.Instance.units[i]) as Texture;
                summonBtnImg.color = new Color(255, 255, 255, 255);
                Data.Instance.bSummons = false;
            }

            GameObject heroBtn = GameObject.Find("3,0");
            RawImage heroBtnImg = heroBtn.GetComponent<RawImage>();
            heroBtnImg.texture = Resources.Load("Image/UnitMy/" + Data.Instance.units[size]) as Texture;
            heroBtnImg.color = new Color(255, 255, 255, 255);
            Unit unit = heroBtn.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.units[size])) as Unit;
            Data.Instance.board[3, 0] = new KeyValuePair<byte, Unit>(Data.Instance.myIndex, unit);

            
            if (unit != null)
            {
                unit.order = Data.Instance.order;
                unit.x = 3;
                unit.y = 0;
                unit.status = 0;
            }
        }

        public void RequestMatching()
        {
            int count = 0;
            unitPos.Clear();
            for (int i = 0; i < Data.COLUMN; i++)
            {
                for (int j = 0; j < Data.ROW; j++)
                {
                    if (Data.Instance.board[i, j].Value != null)
                    {
                        unitPos.Add(new Vector2(i, j));
                        count++;
                    }
                }
            }

            if (count == 6)
            {
                Data.Instance.bSummons = false; // 다음 씬으로 넘어갈때 소환을 끈다.
                
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
                        stateObj.SetActive(true);
                        stateObj.GetComponentInChildren<Text>().text = "매칭 완료";

                        Data.Instance.myIndex = msg.PopByte();
                        Invoke("CompleteLoading", 1);
                        stateObj.SetActive(false);
                    }
                    break;
                case PROTOCOL.FailDeploy:
                    {
                        int[] signal = { msg.PopByte(), msg.PopByte() };
                        if (signal[Data.Instance.myIndex] == 0)
                        {
                            Debug.Log("상대방의 잘못된 배치로 인해 매칭이 취소되었습니다.");
                        }
                        else
                        {
                            Debug.Log("잘못된 배치로 인해 매칭에 실패하였습니다.");
                        }
                    }
                    break;
                case PROTOCOL.SetGame:
                    {
                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Play;
                        for (int i = 0; i < Data.Instance.units.Length * 2; i++)
                        {
                            byte index = msg.PopByte();
                            string type = msg.PopString();
                            int x = msg.PopInt32(), y = msg.PopInt32();
                            if (Data.Instance.myIndex != 0)
                            {
                                y = Data.ROW - y - 1;
                            }
                            GameObject unitOBJ = GameObject.Find(x + "," + y);
                            Data.Instance.board[x, y] = new KeyValuePair<byte, Unit>(index, unitOBJ.AddComponent(Type.GetType("UnitType.Unit" + type)) as Unit);
                            if (index != Data.Instance.myIndex)
                            {
                                DrawEnemy(x, y);
                            }
                        }

                        Debug.Log("게임 시작");
                        playingObj.Enter();
                    }
                    break;
            }
        }

        private void CompleteLoading()
        {
            Packet loadingMsg = Packet.Create((short)PROTOCOL.CompleteLoading);
            for (int i = 0; i < 6; i++)
            {
                loadingMsg.Push(Data.Instance.board[(int)unitPos[i].x, (int)unitPos[i].y].Value.ID);
                loadingMsg.Push((int)unitPos[i].x);
                loadingMsg.Push((int)unitPos[i].y);
            }
            this.networkManager.Send(loadingMsg);
        }

        private void DrawEnemy(int x, int y)
        {
            GameObject TempUnit = GameObject.Find(x + "," + y);
            Texture UnitImage = Resources.Load("Image/UnitEnemy/" + Data.Instance.board[x, y]) as Texture;

            Unit unit = gameObject.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.board[x, y])) as Unit;
            if (unit != null)
            {
                unit.order = 1;
                unit.x = x;
                unit.y = y;
                unit.status = 0;
            }

            TempUnit.GetComponent<RawImage>().texture = UnitImage;
            TempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
        }
    }
}