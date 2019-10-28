using FreeNet;
using UnitType;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace MonsterChessClient
{
    public class BoardUpdate : MonoBehaviour
    {
        int turnCount;
        enum UserStage
        {
            ProcessTimer,
            ProcessGame
        }

        public NetworkManager networkManager;
        public Text timerText, manaText;
        public GameObject startingTimerObj;
        UserStage userState;

        public Main main;

        float timer;
        const int maxTime = 5;

        public void Enter()
        {
            networkManager.messageReceiver = this;
            this.userState = UserStage.ProcessTimer;
            startingTimerObj.SetActive(true);
        }

        void Update()
        {
            switch (this.userState)
            {
                case UserStage.ProcessTimer:
                    timer -= Time.deltaTime;
                    startingTimerObj.GetComponentInChildren<Text>().text = (int)timer + "초 후 시작";
                    if (timer <= 0)
                    {
                        this.userState = UserStage.ProcessGame;
                        startingTimerObj.GetComponentInChildren<Text>().text = "게임 시작";
                        Invoke("StartGame", 1);
                    }
                    break;
                case UserStage.ProcessGame:
                    //여기부터 유닛들이 진짜 움직이기 시작함

                    break;
            }
        }

        private void StartGame()
        {
            timer = maxTime;
            Packet msg = Packet.Create((short)PROTOCOL.StartedGame);
            networkManager.Send(msg);

            startingTimerObj.SetActive(false);
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
                case PROTOCOL.StartedTurn:
                    {
                        Data.Instance.currentPlayer = msg.PopByte();
                        
                        int firstMana = msg.PopInt32(), secondMana = msg.PopInt32();
                        Data.Instance.mana = Data.Instance.myIndex == 0 ? firstMana : secondMana;
                   
                        manaText.text = ""+Data.Instance.mana;
                        Data.Instance.bSummons = false;
                        Data.Instance.bMoving = false;
                    }
                    break;
                case PROTOCOL.Timer:
                    {
                        Data.Instance.time = msg.PopInt32();
                        timerText.text = Data.Instance.time.ToString();
                        if (Data.Instance.bPlaying == false)
                        {
                            if (Data.Instance.time == 0)
                            {
                                Data.Instance.turnNum++;
                                
                                Data.Instance.bPlaying = true;

                                Data.Instance.bSummons = false;
                                Data.Instance.bMoving = false;
                            }
                        }
                    }
                    break;
                case PROTOCOL.RequestedMoving:
                    {
                        int result = msg.PopInt32();
                        Debug.Log("이동 요청 " + (result == 0 ? "실패" : "성공"));
                    }
                    break;
                case PROTOCOL.RequestedSummons:
                    {
                        Debug.Log("리퀘스티드 서먼");
                        //소환을 할려면 ID x,y,
                        int temp = msg.PopInt32();
                        Debug.Log(temp);
                        if (temp == 1)
                        {
                            //소환을 하지 않음
                            Debug.Log("소환을 하지 않음");
                        }
                        else
                        {
                            //소환을 함
                            int summonIdex = msg.PopByte();
                            if (summonIdex == Data.Instance.myIndex)//내소환
                            {
                                int x, y;
                                Data.Instance.summonId = msg.PopString();//summonID를 받아옴
                                if (Data.Instance.myIndex == 0)//내 인덱스가 0일때
                                {
                                    x = msg.PopInt32();
                                    y = msg.PopInt32();
                                }
                                else//내 인덱스가 1일때
                                {
                                    x = msg.PopInt32();
                                    y = 6-msg.PopInt32();
                                }
                                GameObject summonBoard = GameObject.Find(x + "," + y);

                                Unit unit = summonBoard.AddComponent(Type.GetType("UnitType.Unit" + Data.Instance.summonId)) as Unit;
                                summonBoard.GetComponent<RawImage>().texture = Resources.Load("Image/UnitMy/" + Data.Instance.summonId) as Texture;
                                summonBoard.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                Data.Instance.mana = msg.PopInt32();
                                manaText.text = "" + Data.Instance.mana;
                                unit.x = x;
                                unit.y = y;
                                unit.status = 2;
                                unit.order = Data.Instance.order;
                            }
                            else//적소환
                            {
                                int x, y;
                                string tempID = msg.PopString();//summonID를 받아옴
                                if (Data.Instance.myIndex == 0)//내 인덱스가 0일때 상대방 소환
                                {
                                    x = msg.PopInt32();
                                    y = msg.PopInt32();
                                }
                                else
                                {
                                    x = msg.PopInt32();
                                    y = 6 - msg.PopInt32();
                                }
                                GameObject summonBoard = GameObject.Find(x + "," + y);

                                Unit unit = summonBoard.AddComponent(Type.GetType("UnitType.Unit" + tempID)) as Unit;
                                summonBoard.GetComponent<RawImage>().texture = Resources.Load("Image/UnitEnemy/" + tempID) as Texture;
                                summonBoard.GetComponent<RawImage>().color = new Color(255, 255, 255, 255);
                                msg.PopInt32();
                                unit.x = x;
                                unit.y = y;
                                unit.status = 2;
                                unit.order = 1;
                            }
                           
                        }
                    }
                    break;
                case PROTOCOL.MovedUnit:
                    {
                        int result = msg.PopInt32(); // -1이면 움직임이 전부 전송된 것
                        if (result == 0) 
                        {
                            string id = msg.PopString();
                            int x = msg.PopInt32(), y = msg.PopInt32();
                            int moveX = msg.PopInt32(), moveY = msg.PopInt32();
                            int myUnitHP = msg.PopInt32();
                            result = msg.PopInt32();
                            Debug.Log(id);
                            Debug.Log(x);
                            Debug.Log(y);
                            Debug.Log(moveX);
                            Debug.Log(moveY);
                            Debug.Log(myUnitHP);

                            if (result == 0)// 0이면 공격당한 유닛이 있는 것
                            {
                                int enemyX = msg.PopInt32(), enemyY = msg.PopInt32();
                                int enemyMoveX = msg.PopInt32(), enemyMoveY = msg.PopInt32();
                                int enemyHP = msg.PopInt32();

                                Unit unit = GameObject.Find(x + "," + y).GetComponent<Unit>();
                                unit.x = x; unit.y = y; unit.moveX = moveX; unit.moveY = moveY; unit.hp = myUnitHP;

                                Unit enemyUnit = GameObject.Find(enemyX + "," + enemyY).GetComponent<Unit>();
                                enemyUnit.x = x; enemyUnit.y = y; enemyUnit.moveX = moveX; enemyUnit.moveY = moveY;
                                enemyUnit.hp = enemyHP;

                                unit.Attack();
                            }
                            else //단순이동
                            {
                                
                                Unit unit = GameObject.Find(x + "," + y).GetComponent<Unit>();
                                unit.x = x; unit.y = y; unit.moveX = moveX; unit.moveY=moveY;
                                unit.hp = myUnitHP;
                                unit.Move();
                            }
                        }
                        else//이동안함
                        {
                            Data.Instance.currentPlayer = (byte)msg.PopInt32();
                        }
                    }
                    break;
                case PROTOCOL.FinishedTurn:
                    {
                        int firstMana = msg.PopInt32(), secondMana = msg.PopInt32();
                        Debug.Log("바뀌기전 마나" + Data.Instance.mana);
                        Data.Instance.mana = Data.Instance.myIndex == 0 ? firstMana : secondMana;
                        Debug.Log("바뀐후 마나" + Data.Instance.mana);

                        manaText.text = Data.Instance.mana.ToString();
                        turnCount = msg.PopInt32();
                    }
                    break;
                case PROTOCOL.GameOver:
                    {
                        byte winnerIndex = msg.PopByte();
                        if (winnerIndex == Data.Instance.myIndex)
                        {
                            Debug.Log("승리하였습니다.");
                        }
                        else
                        {
                            Debug.Log("패배하였습니다.");
                        }

                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
                        main.Enter();
                    }
                    break;
                case PROTOCOL.RemovedGame:
                    {
                        GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
                        main.Enter();
                    }
                    break;
            }
        }

        /*private void CheckMove(string id, int moveDirection, int x, int y, int order)
        {
            int tempX;
            GameObject targetObj;
            Unit unit;
            switch (moveDirection)
            {
                case 0: // 동
                    for (int i = x + 1; i < moveX + 1; i++)
                    {
                        if (i < 7 && Data.Instance.board[y, i].Value != null)
                        {
                            targetObj = GameObject.Find(y + "," + i);
                            unit = targetObj.GetComponent<Unit>();

                            if (unit != null)
                            {
                                moveX = i - (unit.order == order ? 1 : 0);
                                break;
                            }
                        }
                    }
                    break;
                case 1: // 서
                    for (int i = x - 1; i > moveX - 1; i--)
                    {
                        if (i >= 0 && Data.Instance.board[y, i].Value != null)
                        {
                            targetObj = GameObject.Find(y + "," + i);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                moveX = i + (unit.order == order ? 1 : 0);

                                break;
                            }
                        }
                    }
                    break;
                case 2: // 남
                    for (int i = y - 1; i > moveY - 1; i--)
                    {
                        if (i >= 0 && Data.Instance.board[i, x].Value != null)
                        {
                            targetObj = GameObject.Find(i + "," + x);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                moveY = i + (unit.order == order ? 1 : 0);

                                break;
                            }
                        }
                    }
                    break;
                case 3: // 북
                    for (int i = y + 1; i < moveY + 1; i++)
                    {
                        Debug.Log("for안");
                        if (i < 7 && Data.Instance.board[i, x].Value != null)
                        {
                            Debug.Log("들어왓따");
                            targetObj = GameObject.Find(i + "," + x);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                moveY = i - (unit.order == order ? 1 : 0);
                                Debug.Log(moveY);
                                break;
                            }
                        }
                    }
                    break;
                case 4: // 북동
                    tempX = x + 1;
                    for (int i = y + 1; i < moveY + 1; i++)
                    {
                        if (i < 7 && tempX < 7 && Data.Instance.board[i, tempX].Value != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i - (flag ? 1 : 0);
                                moveX = tempX - (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX++;
                    }
                    break;
                case 5: // 남동
                    tempX = x + 1;
                    for (int i = y - 1; i > moveY; i--)
                    {
                        if (i >= 0 && tempX < 7 && Data.Instance.board[i, tempX].Value != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i + (flag ? 1 : 0);
                                moveX = tempX - (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX++;
                    }
                    break;
                case 6: // 남서
                    tempX = x - 1;
                    for (int i = y - 1; i > moveY; i--)
                    {
                        if (i >= 0 && tempX >= 0 && Data.Instance.board[i, tempX].Value != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i + (flag ? 1 : 0);
                                moveX = tempX + (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX--;
                    }
                    break;
                case 7: // 북서
                    tempX = x - 1;
                    for (int i = y + 1; i < moveY; i++)
                    {
                        if (i < 7 && tempX >= 0 && Data.Instance.board[i, tempX].Value != null)
                        {
                            targetObj = GameObject.Find(i + "," + tempX);
                            unit = targetObj.GetComponent<Unit>();
                            if (unit != null)
                            {
                                bool flag = unit.order == order;
                                moveY = i - (flag ? 1 : 0);
                                moveX = tempX + (flag ? 1 : 0);

                                break;
                            }
                        }

                        tempX--;
                    }
                    break;
            }

        }*/

        public void OnApplicationQuit()
        {
            Packet msg = Packet.Create((short)PROTOCOL.RemovedGame);
            msg.Push(Data.Instance.myIndex);
            networkManager.Send(msg);
        }
    }
}
