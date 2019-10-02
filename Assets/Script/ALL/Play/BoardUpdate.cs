using FreeNet;
using UnitType;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterChessClient
{
    public class BoardUpdate : MonoBehaviour
    {
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

        private void AddList()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (Data.Instance.board[i, j] != null)
                    {
                        Data.Instance.playList.Add(GameObject.Find(i + "," + j));
                    }
                }
            }
        }

        private void SortList()
        {
            // 항목별 정리
            Data.Instance.playList.Sort(delegate (GameObject first, GameObject second)
            {
                Unit firstUnit = first.GetComponent<Unit>(), secondUnit = second.GetComponent<Unit>();
                if (firstUnit.ID[0] == secondUnit.ID[0])
                {
                    if (firstUnit.Cost == secondUnit.Cost)
                    {
                        return 0;
                    }
                    else
                    {
                        return firstUnit.Cost > secondUnit.Cost ? 1 : -1;
                    }
                }
                else
                {
                    return firstUnit.ID[0] > secondUnit.ID[0] ? 1 : -1;
                }
            });

            for (int i = 0; i < Data.Instance.playList.Count; i++)
            {
                Debug.Log(i + "번" + Data.Instance.playList[i]);
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
                case PROTOCOL.StartedTurn:
                    Data.Instance.currentPlayer = msg.PopByte();
                    int firstMana = msg.PopInt32(), secondMana = msg.PopInt32();
                    Data.Instance.mana = Data.Instance.myIndex == 0 ? firstMana : secondMana;
                    break;
                case PROTOCOL.Timer:
                    Data.Instance.time = msg.PopInt32();
                    timerText.text = Data.Instance.time.ToString();
                    if (Data.Instance.bPlaying == false)
                    {
                        if (Data.Instance.time == 0)
                        {
                            Data.Instance.turnNum++;
                            // 30초 끝
                            AddList();
                            SortList();
                            Data.Instance.bPlaying = true;
                        }
                    }
                    break;
                case PROTOCOL.RequestedMoving:
                    int result = msg.PopInt32();
                    Debug.Log("이동 요청 " + (result == 0 ? "실패" : "성공"));
                    break;
                case PROTOCOL.MovedUnit:
                    Data.Instance.mana++;
                    manaText.text = Data.Instance.mana.ToString();
                    //================================================
                    Data.Instance.bSummons = false;
                    Data.Instance.bMoving = false;
                    break;
                case PROTOCOL.RemovedGame:
                    GameObject.Find("SceneManager").GetComponent<MySceneManager>().Present = SceneList.Main;
                    main.Enter();
                    break;
            }
        }

        public void OnApplicationQuit()
        {
            Packet msg = Packet.Create((short)PROTOCOL.RemovedGame);
            msg.Push(Data.Instance.myIndex);
            networkManager.Send(msg);
        }
    }
}
