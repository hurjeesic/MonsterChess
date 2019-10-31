namespace MonsterChessServer
{
    public enum PROTOCOL : short
    {
        Begin = 0,

        RequestLogin, // Client의 Login 요청
        FailLogin,
        SuccessLogin,

        RequestRegistering,
        FailRegistering,
        SuccessRegistering,

        RequestFinding,
        FailFinding,
        SuccessFinding,

        RequestFriendList,
        FailFriendList,
        SuccessFriendList,

        RequestMatching, // Client의 Matching 요청
        CancleMatching, // Client의 Matching 취소 요청
        StartLoading,
        CompleteLoading,
        FailDeploy,

        SetGame,
        StartedGame, // Game 시작
        Timer,
        StartedTurn,
        RequestedMoving, // Client의 이동 요청
        RequestedSummons,
        MovedUnit,
        WaitedUnit,
        FinishedTurn, // Client의 Turn 연출이 끝났음을 알림

        RemovedGame, // 상대방 Player가 나가 방이 삭제됨
        GameOver, // Game 종료

        End
    }
}