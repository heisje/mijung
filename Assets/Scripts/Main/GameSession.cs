using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameSession : Singleton<GameSession>
{
    public Player Player;
    private TaskCompletionSource<PlayerInputType> UserInputTaskCompletionSource; // 어떤 입력을 했는지 저장하고, Round상태를 결정지음
    public ChangeTMP RollCountText;
    public RoundStateType RoundCurrentState = RoundStateType.DevGo;
    public RoundStateType RoundPreviousState = RoundStateType.DevGo;
    public int NOfRoll = 0;    // 게임 라운드 횟수 저장
    public PlayerInputType PlayerInput;
    public Skill SelectedSkill;
    public Character SelectedTarget;
    void Start()
    {
        Round();
    }

    public async void Round()
    {
        LifeCycleManager.Ins.Register(AttackOrderMM.Ins);
        LifeCycleManager.Ins.Register(EnemyManager.Ins);
        LifeCycleManager.Ins.Register(Player);
        // 스테이지 시작
        while (true)
        {
            // 메인화면 ------------------------------
            RollCountText.ChangeText("메인화면");

            // -------------------------------------
            await MatchPlayerInput(PlayerInputType.ClickSkip);

            // 세선 선택 ------------------------------
            RollCountText.ChangeText("캐릭터 선택");
            KeyValuePair<int, string>[] choiceCharacter = new KeyValuePair<int, string>[]
                   {
                        new(1, "검사"),
                        new(2, "영기"),
                        new(3, "총사")
                   };
            int i = await SelectManager.Ins.SelectButtonGroup(choiceCharacter, SelectManager.Ins.transform);
            // await CharacterLoader.Ins.LoadCharacterPrefab((ECharacter)i, Player);
            Player.SelectCharacter(i);
            // -------------------------------------
            await MatchPlayerInput(PlayerInputType.ClickSkip);


            // 적 선택, 던전 로딩, 카드 로딩 ------------------------------
            RollCountText.ChangeText("적 선택");
            LifeCycleManager.Ins.BeforeStage();

            SkillManager.Ins.CreatePlayerSkillCard(Player);
            // 우선권 초기화

            // -------------------------------------
            await MatchPlayerInput(PlayerInputType.ClickSkip);
            LifeCycleManager.Ins.StartStage();
            // 라운드 시작, 주사위 굴릴 수 있음 ------------
            while (true)
            {
                LifeCycleManager.Ins.StartTurn();
                SkillManager.Ins.ChangeIsPossibleSkill(Player, false);

                RollCountText.ChangeText("라운드 시작: 적 데미지 ROLL");

                // 다이스 관련 초기화
                foreach (Dice dice in Player.HaveDices)
                {
                    dice.UpdateStat(DiceState.ToBeRolled);
                }
                NOfRoll = Player.NOfRoll;

                // 공격순서 초기화
                // -------------------------------------

                await MatchPlayerInput(PlayerInputType.ClickRoll);

                RollCountText.ChangeText("라운드 시작: 주사위를 굴리세요");
                while (NOfRoll >= 0)
                {
                    // 유저 입력별 행동
                    PlayerInputType playerInput = await WaitForPlayerInput();

                    // 반복문 빠져나가기
                    if (playerInput == PlayerInputType.ClickSkip)
                    {
                        break;
                    }

                    // 다이스 굴림 => 전역 스킬 업데이트
                    // 다이스 선택 => 스킬 업데이트

                    switch (playerInput)
                    {
                        case PlayerInputType.ClickRoll:
                            RollDices();
                            break;
                        case PlayerInputType.SelectDice:
                            break;
                        case PlayerInputType.ClickSkill:

                            var selectedDiceInfo = new DiceInfo(Player.GetSelectedDiceValues());

                            Sk_Context player_sk_context = new(Player, SelectedTarget, selectedDiceInfo);
                            if (selectedDiceInfo.IsContainPip(GLOBAL_CONST.HURT_PIP))
                            {
                                Player.AttackOrderValue += Player.BoomHurt(SelectedTarget, selectedDiceInfo);
                            }

                            var playerAction = new PlayerAction(SelectedSkill, player_sk_context);
                            AttackOrderMM.Ins.PlayerActionList.Add(playerAction);
                            foreach (var dice in Player.GetSelectedDice())
                            {
                                dice.UpdateStat(DiceState.Used);
                            }
                            break;
                    }
                    SkillUpdate();
                }

                // 우선권 초기화
                AttackOrderMM.Ins.Act();
                LifeCycleManager.Ins.EndTurn();

                // 종료조건
                if (EnemyManager.Ins.CheckAllDeadEnemy()) break;

                // -------------------------------------
            }
            await WaitForPlayerInput();

            // 승리 ------------------------------
            LifeCycleManager.Ins.EndStage();
            RollCountText.ChangeText("승리.");

            // -------------------------------------
            await WaitForPlayerInput();

            // 패배 ------------------------------

            RollCountText.ChangeText("패배.");

            // -------------------------------------
            await WaitForPlayerInput();

            // 결과창 ------------------------------

            RollCountText.ChangeText("결과.");

            // -------------------------------------
        }

    }

    public void RollDices()
    {
        // 초기화: 스킬 카드 사용 불가능하게 설정
        SkillManager.Ins.ChangeIsPossibleSkill(Player, false);

        // 굴리기 시작
        foreach (Dice dice in Player.HaveDices)
        {
            // 돌릴 수 있는 상태만 돌림
            if (dice.State == DiceState.ToBeRolled)
            {
                dice.Roll();
            }
        }

        NOfRoll -= 1;

        // 상태 변경
        if (NOfRoll <= 0)
        {
            RollCountText.ChangeText("모두 굴렸습니다");
        }
        if (NOfRoll > 0)
        {
            RollCountText.ChangeText("남은 횟수: " + NOfRoll.ToString());
        }
    }

    // TODO: 전역 스킬사용과 선택 스킬 사용 분리
    public void SkillUpdate(Character Target = null)
    {
        var allDiceDTO = new DiceInfo(Player.GetAllDiceValues());
        var selectedDiceDTO = new DiceInfo(Player.GetSelectedDiceValues());
        Sk_Context player_sk_context = new(Player, Target, selectedDiceDTO);
        SkillManager.Ins.UpdateSkills(allDiceDTO, player_sk_context);
    }

    // Util, 해당 요청이 올 때까지 무한 반복 
    public async Task MatchPlayerInput(PlayerInputType matchPlayerInput)
    {
        PlayerInput = PlayerInputType.None;
        while (PlayerInput != matchPlayerInput)
        {
            PlayerInput = await WaitForPlayerInput();
        }
    }

    public void OnPlayerInput(PlayerInputType playerInput)
    {
        // 유저가 클릭했을 때 호출되는 메서드
        if (UserInputTaskCompletionSource != null && !UserInputTaskCompletionSource.Task.IsCompleted)
        {
            UserInputTaskCompletionSource.SetResult(playerInput);
        }
    }

    // OnSkill을 저장하는 함수
    public void OnPlayerSkillInput(Skill skill, Character target) // PlayerAction playerAction)
    {
        // 스킬을 사용했을 때 액티브
        if (UserInputTaskCompletionSource != null && !UserInputTaskCompletionSource.Task.IsCompleted)
        {
            // TODO: 묶어주기
            SelectedSkill = skill;
            SelectedTarget = target;
            UserInputTaskCompletionSource.SetResult(PlayerInputType.ClickSkill);
        }
    }

    /// <summary>
    /// 입력 대기상태로 변경
    /// </summary>
    /// <returns></returns>
    private Task<PlayerInputType> WaitForPlayerInput()
    {
        UserInputTaskCompletionSource = new TaskCompletionSource<PlayerInputType>();
        return UserInputTaskCompletionSource.Task;
    }
}
