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

        public void Enter() //셋게임 마지막 작업
        {

            networkManager.messageReceiver = this;
            nextBtn.SetActive(true);
            matchCancleBtn.SetActive(false);
            stateObj.SetActive(false);

            Array.Sort(Data.Instance.units);
            int size = Data.Instance.units.Length - 1;
            for (int i = 0; i < size; i++)
            {
                RawImage summonBtnImg = GameObject.Find(i.ToString()).GetComponent<RawImage>();
                summonBtnImg.texture = Resources.Load("Image/UnitMy/" + Data.Instance.units[i]) as Texture;
                summonBtnImg.color = new Color(255, 255, 255, 255);
                Data.Instance.bSummons = false;
            }

            PlaceInitialization();
        }

        public void RequestMatching()
        {
            int count = 0;
            unitPos.Clear();
            for (int i = 0; i < Data.COLUMN; i++)
            {
                for (int j = 0; j < Data.ROW; j++)
                {
                    Unit unit = GameObject.Find(i + "," + j).GetComponent<Unit>();
                    if (unit != null)
                    {
                        //유닛 포즈를 저장시키는것
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
        public void OnReceive(Packet msg)// (1) 메인
        {
            // 제일 먼저 프로토콜 아이디를 꺼내온다.
            PROTOCOL protocolID = (PROTOCOL)msg.PopProtocolID();

            switch (protocolID)
            {
                case PROTOCOL.StartLoading://플레이를 구성하는 것
                    {
                        stateObj.SetActive(true);
                        stateObj.GetComponentInChildren<Text>().text = "매칭 완료";

                        Data.Instance.myIndex = msg.PopByte();//내 인덱스를 가져온다.
                        
                        stateObj.SetActive(false);
                        Debug.Log("현재 내 보드값들");
                        for (int x = 0; x < Data.COLUMN; x++)
                        {
                            // y값 설정
                            for (int y = 0; y < Data.ROW / 2; y++)
                            {
                                // x값 설정
                                //내유닛들을 추가함
                                Unit tempUnit = GameObject.Find(x + "," + y).GetComponent<Unit>();
                                if (tempUnit != null)
                                {
                                    //내 유닛들 추가
                                    Data.Instance.board[x, y] = new KeyValuePair<byte, Unit>(Data.Instance.myIndex, tempUnit);
                                    Data.Instance.board[x, y].Value.order = Data.Instance.order;
                                    Data.Instance.board[x, y].Value.x = x;
                                    Data.Instance.board[x, y].Value.y = y;
                                    Data.Instance.board[x, y].Value.status = 0;
                                    Debug.Log(x + "," + y + " : " + Data.Instance.board[x, y].Value.ID+","+Data.Instance.board[x,y].Key);

                                }

                            }
                        }
                        Invoke("CompleteLoading", 1);
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
                case PROTOCOL.SetGame://플레이구성
                    {
                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Play;
                        for (int i = 0; i < Data.Instance.units.Length * 2; i++)
                        {
                            byte index = msg.PopByte();//인덱스를 받아옴
                            
                            string type = msg.PopString(); // 타입을 받아옴(ID)
                            int x = msg.PopInt32(), y = msg.PopInt32();//좌표값을 받아옴
                            if (index != Data.Instance.myIndex)//적표시
                            {
                                Debug.Log("적의 인덱스는" + index);
                                if (Data.Instance.myIndex != 0)//내가 1일경우 반전
                                {
                                    y = Data.ROW - y - 1;
                                }
                                GameObject unitOBJ = GameObject.Find(x + "," + y);//적유닛
                                Data.Instance.board[x, y] = new KeyValuePair<byte, Unit>(index, unitOBJ.AddComponent(Type.GetType("UnitType.Unit" + type)) as Unit);
                                //적유닛에 유닛을 넣고 그 유닛을 보드에넣음
                                Debug.Log("보드에 값은" + Data.Instance.board[x, y].Value.ID + "\t" + Data.Instance.board[x,y].Key);
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
            GameObject TempUnit = GameObject.Find(x + "," + y);//적유닛
            Texture UnitImage = Resources.Load("Image/UnitEnemy/" + Data.Instance.board[x, y].Value.ID) as Texture;
            //이미지저장
            Unit unit = TempUnit.GetComponent<Unit>();
            //유닛을 가져옴
            if (unit != null)
            {
                //
                unit.order = 1;
                unit.x = x;
                unit.y = y;
                unit.status = 0;
                Data.Instance.board[x, y].Value.x = x;
                Data.Instance.board[x, y].Value.y = y;
                Data.Instance.board[x, y].Value.status = 0;
                Data.Instance.board[x, y].Value.order = 1;
                GameObject.Find("hpBar"+x+","+y).GetComponent<Image>().color = new Color(255 / 255, 0, 0, 255 / 255); ;
                Debug.Log(x+","+y+": 적 좌표값");


            }

            TempUnit.GetComponent<RawImage>().texture = UnitImage;
            TempUnit.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
        }
        public void PlaceInitialization()
        {
            Data.Instance.mana = 10;//마나 설정
            Text text = GameObject.Find("ManaText").GetComponent<Text>();//마나텍스트 설정
            text.text = "" + Data.Instance.mana;//텍스트 설정
            for (int i = 0; i < Data.COLUMN; i++)
            {
                for (int j = 0; j < Data.ROW; j++)
                {
                    //히어로를 제외한 칸 초기화
                    GameObject tempBoard = GameObject.Find(i + "," + j);
                    Unit unit = tempBoard.GetComponent<Unit>();
                    if (unit != null) DestroyImmediate(unit);
                    tempBoard.GetComponent<RawImage>().texture = null;
                    tempBoard.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
                    Data.Instance.board[i, j] = Data.Instance.Empty;
                    if (i == 3 && j == 0)
                    {
                        GameObject heroBtn = GameObject.Find("3,0");//히어로칸 탐색
                        RawImage heroBtnImg = heroBtn.GetComponent<RawImage>();//히어로 이미지추가
                        heroBtnImg.texture = Resources.Load("Image/UnitMy/" + Data.Instance.units[5]) as Texture;
                        heroBtnImg.color = new Color(255, 255, 255, 255);//히어로 그림표시
                        Unit heroUnit = heroBtn.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.units[5])) as Unit;//유닛 컴포넌트추가
                        heroUnit.x = 3;
                        heroUnit.y = 0;
                        heroUnit.moveX = 3;
                        heroUnit.moveY = 0;
                        heroUnit.status = 0;
                        heroUnit.order = Data.Instance.order;
                        GameObject.Find("hpBar3,0").GetComponent<Image>().color = new Color(255 / 255, 0, 0, 255 / 255); ;


                    }
                }
            }
        }

    }
}