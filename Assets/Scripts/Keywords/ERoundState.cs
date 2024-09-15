public enum RoundStateType
{
    Main, // 메인화면
    SelectSession, // 세선 선택
    SelectEnemy, // 적 선택
    StartRound, // 라운드 시작, 주사위 굴릴 수 있음
    RollDice, // 주사위 굴림, 주사위 굴릴 수 있음
    WaitSkill, // 스킬 선택 대기, 주사위 굴릴 수 없음
    ActiveSkill, // 스킬사용 -> StartRound
    WinEnemy,   // 승리
    LoseEnemy,  // 패배
    ResultSession,  // 결과창
    DevGo, // 임시 진행
}