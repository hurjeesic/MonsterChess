namespace MonsterChessClient
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

        StartedGame, // Game 시작
        StartedTurn,
        RequestedMoving, // Client의 이동 요청
        MovedUnit, // 요청된 이동 중 하나가 완료
        CompleteMoving, // 하나의 이동 연출이 완료됨
        FinishedTurn, // Client의 Turn 연출이 끝났음을 알림

        RemovedGame, // 상대방 Player가 나가 방이 삭제됨
        GameOver, // Game 종료

        End
    }
}