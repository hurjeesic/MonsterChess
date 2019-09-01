using FreeNet;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MonterChessClient
{
    public class BattleRoom : MonoBehaviour
    {
        enum GameState
        {
            READY = 0,
            STARTED
        }

        // 가로, 세로 칸 수
        public static readonly int COLUMN = 7;
        
        List<Player> players;

        List<short> board; // 진행중인 Game Board의 상태를 나타내는 Data
        List<short> tableBoard; // 0 ~ 49까지의 Index를 갖고 있는 Board Data
        List<short> availableAttackCells; // 공격 가능한 범위를 나타낼 때 사용하는 List

        byte currentPlayerIndex; // 현재 Turn을 진행 중인 Player Index
        byte playerMeIndex; // Server에서 지정해 준 본인의 Player Index
        byte step; // 상황에 따른 Touch 입력을 처리하기 위한 변수

        Login mainTitle; // Game 종료 후 Main으로 돌아갈 때 사용하기 위한 MainTitle 객체의 Reference
        NetworkManager networkManager; // Network Data 송, 수신을 위한 Network Manager Reference
        GameState gameState; // Game 상태에 따라 각각 다른 GUI 모습을 구현하기 위해 필요한 상태 변수

        // OnGUI Method에서 호출할 Delegate
        // 여러 종류의 Method를 만들어 놓고 상황에 맞게 draw에 대입해 주는 방식으로 GUI를 변경시킴
        delegate void GuiFunction();
        GuiFunction draw;

        byte winPlayerIndex; // 승리한 Player Index, 무승부일 때는 byte.MaxValue

        ImageNumber scoreImages; // 점수를 표시하기 위한 Image 숫자 객체, 선명하고 예쁘게 표현하기 위해 Font 대신 Image로 만들어 사용한다.  
        BattleInformaionPanel battleInformation; // 현재 진행 중인 Player를 나타내는 객체

        // Game이 종료되었는지를 나타내는 Flag
        bool isGameFinished;

        // 각종 Image Texture
        List<Texture> imagePlayers;
        Texture background;
        Texture blankImage;
        Texture gameBoard;

        Texture grayCell;
        Texture focusCell;

        Texture winImage;
        Texture loseImage;
        Texture drawImage;
        Texture grayTransparent;

        void Awake()
        {
            this.tableBoard = new List<short>();
            this.availableAttackCells = new List<short>();

            this.grayCell = Resources.Load("images/graycell") as Texture;
            this.focusCell = Resources.Load("images/border") as Texture;

            this.blankImage = Resources.Load("images/blank") as Texture;
            this.gameBoard = Resources.Load("images/gameboard") as Texture;
            this.background = Resources.Load("images/gameboard_bg") as Texture;
            this.imagePlayers = new List<Texture>();
            this.imagePlayers.Add(Resources.Load("images/red") as Texture);
            this.imagePlayers.Add(Resources.Load("images/blue") as Texture);

            this.winImage = Resources.Load("images/win") as Texture;
            this.loseImage = Resources.Load("images/lose") as Texture;
            this.drawImage = Resources.Load("images/draw") as Texture;
            this.grayTransparent = Resources.Load("images/gray_transparent") as Texture;

            this.board = new List<short>();

            this.networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();

            this.gameState = GameState.READY;

            this.mainTitle = GameObject.Find("MainTitle").GetComponent<Login>();
            this.scoreImages = gameObject.AddComponent<ImageNumber>();

            this.winPlayerIndex = byte.MaxValue;
            this.draw = this.OnGuiPlaying;
            this.battleInformation = gameObject.AddComponent<BattleInformaionPanel>();
        }
        
        void Initialize()
        {
            // Board Data를 모두 초기화
            this.board.Clear();
            this.tableBoard.Clear();
            for (int i = 0; i < COLUMN * COLUMN; i++)
            {
                this.board.Add(short.MaxValue);
                this.tableBoard.Add((short)i);
            }

            // Board에 각 Player들의 위치를 입력
            this.players.ForEach(obj =>
            {
                obj.cellIndexes.ForEach(cell =>
                {
                    Debug.Log("cell " + cell);
                    this.board[cell] = obj.playerIndex;
                });
            });
        }

        void Clear()
        {
            this.currentPlayerIndex = 0;
            this.step = 0;
            this.draw = this.OnGuiPlaying;
            this.isGameFinished = false;
        }

        /// <summary>
        /// Game Room에 입장할 때 호출됨, 변수 초기화 등 Game Play를 위한 준비 작업을 진행
        /// </summary>
        public void StartLoading(byte playerMeIndex)
        {
            Clear();

            this.networkManager.messageReceiver = this;
            this.playerMeIndex = playerMeIndex;

            Packet msg = Packet.Create((short)PROTOCOL.CompleteLoading);
            this.networkManager.Send(msg);
        }

        /// <summary>
        /// Packet을 수신 했을 때 호출됨
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="msg"></param>
        void OnReceive(Packet msg) // recv
        {
            PROTOCOL protocolID = (PROTOCOL)msg.PopProtocolID();

            switch (protocolID)
            {
                case PROTOCOL.StartGame:
                    OnGameStart(msg);
                    break;
                case PROTOCOL.PlayerMovedFirst:
                    OnPlayerMoved(msg);
                    break;
                case PROTOCOL.RequestFinishedTurn: // 원래 턴이 시작될때
                    OnStartPlayerTurn(msg);
                    break;
                case PROTOCOL.RemovedGame:
                    OnRoomRemoved();
                    break;
                case PROTOCOL.GameOver:
                    OnGameover(msg);
                    break;
            }
        }

        void OnRoomRemoved()
        {
            if (!isGameFinished)
            {
                BackToMain();
            }
        }

        void BackToMain()
        {
            this.mainTitle.gameObject.SetActive(true);
            this.mainTitle.Enter();

            gameObject.SetActive(false);
        }

        void OnGameover(Packet msg)
        {
            this.isGameFinished = true;
            this.winPlayerIndex = msg.PopByte();
            this.draw = this.OnGuiGameResult;
        }

        void Update()
        {
            if (this.isGameFinished)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    BackToMain();
                }
            }
        }

        void OnGameStart(Packet msg)
        {
            this.players = new List<Player>();

            byte count = msg.PopByte();
            for (byte i = 0; i < count; i++)
            {
                byte playerIndex = msg.PopByte();

                GameObject obj = new GameObject(string.Format("player{0}", i));
                Player player = obj.AddComponent<Player>();
                player.Initialize(playerIndex);
                player.Clear();

                byte virusCount = msg.PopByte();
                for (byte index = 0; index < virusCount; ++index)
                {
                    short position = msg.PopInt16();
                    player.Add(position);
                }

                this.players.Add(player);
            }

            this.currentPlayerIndex = msg.PopByte();
            Initialize();

            this.gameState = GameState.STARTED;
        }

        void OnPlayerMoved(Packet msg)
        {
            byte playerIndex = msg.PopByte();
            short from = msg.PopInt16();
            short to = msg.PopInt16();

            StartCoroutine(OnSelectedCellToAttack(playerIndex, from, to));
        }

        void OnStartPlayerTurn(Packet msg)
        {
            PhaseEnd();

            this.currentPlayerIndex = msg.PopByte();
        }

        float ratio = 1.0f;
        void OnGUI()
        {
            this.draw();
        }

        /// <summary>
        /// Game 진행 화면 그리기
        /// </summary>
        void OnGuiPlaying()
        {
            if (this.gameState != GameState.STARTED)
            {
                return;
            }

            this.ratio = Screen.width / 800.0f;

            DrawBoard();
        }

        /// <summary>  
        /// 결과 화면 그리기
        /// </summary>  
        void OnGuiGameResult()
        {
            OnGuiPlaying();

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.grayTransparent);
            GUI.BeginGroup(new Rect(Screen.width / 2 - 173, Screen.height / 2 - 84,
                this.winImage.width, this.winImage.height));
            {
                if (this.winPlayerIndex == byte.MaxValue)
                {
                    GUI.DrawTexture(new Rect(0, 0, this.drawImage.width, this.drawImage.height), this.drawImage);
                }
                else
                {
                    // win, lose이미지 출력.  
                    if (this.playerMeIndex == this.winPlayerIndex)
                    {
                        GUI.DrawTexture(new Rect(0, 0, 346, 169), this.winImage);
                    }
                    else
                    {
                        GUI.DrawTexture(new Rect(0, 0, 346, 169), this.loseImage);
                    }
                }

                // 자기 자신의 Player Image 출력
                Texture character = this.imagePlayers[this.playerMeIndex];
                GUI.DrawTexture(new Rect(28, 43, character.width, character.height), character);
            }
            GUI.EndGroup();
        }

        void DrawBoard()
        {
            float scaledHeight = 480.0f * ratio;
            float gapHeight = Screen.height - scaledHeight;

            float outlineLeft = 0;
            float outlineTop = gapHeight * 0.5f;
            float outlineWidth = Screen.width;
            float outlineHeight = scaledHeight;

            float horizentalCenter = outlineWidth * 0.5f;
            float verticalCenter = outlineHeight * 0.5f;

            GUI.BeginGroup(new Rect(0, 0, outlineWidth, Screen.height));

            // Draw background to full of the screen.
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.background);

            // 점수 표시
            // red(index 0)팀은 왼쪽,
            // blue(index 1)팀은 오른쪽에 표시
            Rect redteamRect = new Rect(outlineLeft + 20 * ratio, verticalCenter - 60 * ratio, 100 * ratio, 60 * ratio);
            Rect blueteamRect = new Rect(outlineWidth - 120 * ratio, verticalCenter - 60 * ratio, 100 * ratio, 60 * ratio);
            this.scoreImages.Print(this.players[0].GetVirusCount(), redteamRect.xMin, redteamRect.yMin, ratio);
            this.scoreImages.Print(this.players[1].GetVirusCount(), blueteamRect.xMin, blueteamRect.yMin, ratio);

            // Draw a board(alignment : center)
            GUI.DrawTexture(new Rect(0, outlineTop, outlineWidth, outlineHeight), this.gameBoard);

            // 현재 진행중인 Turn 표시
            this.battleInformation.DrawTurnInformation(this.currentPlayerIndex, ratio);
            this.battleInformation.DrawMyInformation(this.playerMeIndex, ratio);

            int width = (int)(60 * ratio);
            int celloutlineWidth = width * BattleRoom.COLUMN;
            float halfCelloutlineWidth = celloutlineWidth * 0.5f;

            GUI.BeginGroup(new Rect(horizentalCenter - halfCelloutlineWidth, verticalCenter - halfCelloutlineWidth + outlineTop, celloutlineWidth, celloutlineWidth));

            List<int> currentTurn = new List<int>();
            short index = 0;
            for (int row = 0; row < BattleRoom.COLUMN; ++row)
            {
                int gapY = 0; // (row * 1);
                for (int col = 0; col < BattleRoom.COLUMN; ++col)
                {
                    int gapX = 0; // (col * 1);

                    Rect cellRect = new Rect(col * width + gapX, row * width + gapY, width, width);
                    if (GUI.Button(cellRect, ""))
                    {
                        OnClick(index);
                    }

                    if (this.board[index] != short.MaxValue)
                    {
                        int playerIndex = this.board[index];
                        GUI.DrawTexture(cellRect, this.imagePlayers[playerIndex]);

                        if (this.currentPlayerIndex == playerIndex)
                        {
                            GUI.DrawTexture(cellRect, this.focusCell);
                        }
                    }

                    if (this.availableAttackCells.Contains(index))
                    {
                        GUI.DrawTexture(cellRect, this.focusCell);
                    }

                    ++index;
                }
            }
            GUI.EndGroup();
            GUI.EndGroup();
        }

        short selectedCell = short.MaxValue;
        void OnClick(short cell)
        {
            // Debug.Log(cell);
            // 자신의 차례가 아니면 처리하지 않고 반환
            if (this.playerMeIndex != this.currentPlayerIndex)
            {
                return;
            }

            switch (this.step)
            {
                case 0:
                    if (ValidateBeginCell(cell))
                    {
                        this.selectedCell = cell;
                        this.step = 1;

                        RefreshAvailableCells(this.selectedCell);
                    }
                    break;

                case 1:
                    {
                        // 자신의 세균을 Touch하였을 경우에는 다시 공격 범위를 계산하여 출력
                        if (this.players[this.currentPlayerIndex].cellIndexes.Exists(obj => obj == cell))
                        {
                            this.selectedCell = cell;
                            RefreshAvailableCells(this.selectedCell);
                            break;
                        }

                        // Game 규칙에 따라서 다른 플레이어의 세균은 선택할 수 없도록 처리
                        foreach (Player player in this.players)
                        {
                            if (player.cellIndexes.Exists(obj => obj == cell))
                            {
                                return;
                            }
                        }

                        // 2칸을 초과하는 거리는 이동할 수 없음
                        if (Helper.get_distance(this.selectedCell, cell) > 2)
                        {
                            return;
                        }

                        // 모든 검사가 정상이므로 Server에 이동 요청을 보냄
                        Packet msg = Packet.Create((short)PROTOCOL.RequestMoving);
                        msg.Push(this.selectedCell);
                        msg.Push(cell);
                        this.networkManager.Send(msg);

                        this.step = 2;
                    }
                    break;
            }
        }

        IEnumerator OnSelectedCellToAttack(byte playerIndex, short from, short to)
        {
            byte distance = Helper.howfar_from_clicked_cell(from, to);
            if (distance == 1)
            {
                // copy to cell
                yield return StartCoroutine(Reproduce(to));
            }
            else if (distance == 2)
            {
                // move
                this.board[from] = short.MaxValue;
                this.players[playerIndex].Remove(from);
                yield return StartCoroutine(Reproduce(to));
            }

            Packet msg = Packet.Create((short)PROTOCOL.RequestFinishedTurn);
            this.networkManager.Send(msg);

            yield return 0;
        }

        void PhaseEnd()
        {
            this.step = 0;
            this.availableAttackCells.Clear();
        }

        void RefreshAvailableCells(short cell)
        {
            this.availableAttackCells = Helper.find_available_cells(cell, this.tableBoard, this.players);
        }

        void ClearAvailableAttackingCells()
        {
            this.availableAttackCells.Clear();
        }

        IEnumerator Reproduce(short cell)
        {
            Player current_player = this.players[this.currentPlayerIndex];
            Player other_player = this.players.Find(obj => obj.playerIndex != this.currentPlayerIndex);

            ClearAvailableAttackingCells();
            yield return new WaitForSeconds(0.5f);

            this.board[cell] = current_player.playerIndex;
            current_player.Add(cell);

            yield return new WaitForSeconds(0.5f);

            // eat.
            List<short> neighbors = Helper.find_neighbor_cells(cell, other_player.cellIndexes, 1);
            foreach (short obj in neighbors)
            {
                this.board[obj] = current_player.playerIndex;
                current_player.Add(obj);

                other_player.Remove(obj);

                yield return new WaitForSeconds(0.2f);
            }
        }

        bool ValidateBeginCell(short cell)
        {
            return this.players[this.currentPlayerIndex].cellIndexes.Exists(obj => obj == cell);
        }
    }
}