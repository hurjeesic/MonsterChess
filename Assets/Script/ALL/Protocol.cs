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

        StartGame, // Game 시작
        StartTurn,
        RequestMoving, // Client의 이동 요청
        PlayerMovedFirst, // 선공인 Player가 이동 했음을 알림
        PlayerMovedSecond, // 후공인 Player가 이동 했음을 알림
        RequestFinishedTurn, // Client의 Turn 연출이 끝났음을 알림

        RemovedGame, // 상대방 Player가 나가 방이 삭제됨
        GameOver, // Game 종료

        End
    }
}