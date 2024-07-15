using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameSession : Singleton<GameSession>, IClickable
{
    public Player Player;
    public int Id;

    public List<int> Results;
    private TaskCompletionSource<RoundStateType> userInputTaskCompletionSource; // 어떤 입력을 했는지 저장하고, Round상태를 결정지음
    public ChangeTMP RollCountText;
    public RoundStateType RoundCurrentState = RoundStateType.DevGo;
    public RoundStateType RoundPreviousState = RoundStateType.DevGo;
    public int NOfRoll = 0;    // 게임 라운드 횟수 저장
    public RoundStateType Current = RoundStateType.Main;
    public DiceCalculateDto DiceDTO;


    void Start()
    {
        Round();
    }

    public async void Round()
    {

        while (true)
        {
            Current = RoundStateType.Main;
            // 메인화면 ------------------------------
            RollCountText.ChangeText("메인화면");

            // -------------------------------------
            await WaitForUserInput();
            Current = RoundStateType.SelectSession;
            // 세선 선택 ------------------------------
            RollCountText.ChangeText("캐릭터 선택");
            KeyValuePair<int, string>[] choiceCharacter = new KeyValuePair<int, string>[]
                   {
                        new(1, "검사"),
                        new(2, "영기"),
                        new(3, "총사")
                   };
            int i = await SelectManager.Instance.SelectButtonGroup(choiceCharacter);
            await CharacterLoader.Instance.LoadCharacterPrefab((CharacterType)i, Player);

            // -------------------------------------
            await WaitForUserInput();
            Player.SelectCharacter(i);
            Current = RoundStateType.SelectEnemy;
            // 적 선택, 던전 로딩, 카드 로딩 ------------------------------

            RollCountText.ChangeText("적 선택");
            EnemyManager.Instance.SelectEnemy();
            SkillManager.Instance.CreatePlayerSkillCard(Player);
            // -------------------------------------
            await WaitForUserInput();

            // 라운드 시작, 주사위 굴릴 수 있음 ------------

            while (true)
            {
                SkillManager.Instance.ChangeIsPossibleSkill(Player, false);
                Current = RoundStateType.StartRound;
                RollCountText.ChangeText("라운드 시작: 주사위를 굴리세요");

                foreach (Dice dice in Player.HaveDices)
                {
                    dice.UpdateStat(DiceState.ToBeRolled);
                }
                NOfRoll = Player.NOfRoll;

                // -------------------------------------
                await WaitForUserInput();
                Current = RoundStateType.RollDice;
                // 주사위 굴림, 주사위 굴릴 수 있음 ------------

                while (NOfRoll > 0)
                {
                    // 초기화: 스킬 카드 사용 불가능하게 설정
                    SkillManager.Instance.ChangeIsPossibleSkill(Player, false);

                    // 굴리기 시작
                    Roll();
                    NOfRoll -= 1;

                    // 레거시
                    // SkillSelectManager.Instance.UpdateSkillUI(scores);

                    DiceDTO = DiceManager.Instance.Calculate(Player.GetDiceValues());
                    SkillManager.Instance.UpdateSkills(DiceDTO, Player);

                    // UI 업데이트


                    // 상태 변경
                    if (NOfRoll <= 0)
                    {
                        RollCountText.ChangeText("모두 굴렸습니다");
                    }
                    if (NOfRoll > 0)
                    {
                        RollCountText.ChangeText("남은 횟수: " + NOfRoll.ToString());
                    }
                    // 스킬 카드 사용가능하게 설정
                    // SkillManager.Instance.ChangeIsPossibleSkill(Player, true);

                    RoundStateType selectType = await WaitForUserInput();


                    if (selectType == RoundStateType.ActiveSkill)
                    {
                        Current = RoundStateType.ActiveSkill;
                        break;
                    }
                }
                // -------------------------------------

                // 스킬 사용 대기 상태, 이미 액티브 상태가 됐으면 해제 --------------
                while (Current != RoundStateType.ActiveSkill)
                {
                    Current = RoundStateType.WaitSkill;
                    RoundStateType selectType = await WaitForUserInput();
                    if (selectType == RoundStateType.ActiveSkill) Current = RoundStateType.ActiveSkill;
                }
                // 스킬사용 -> StartRound ------------------
                foreach (Enemy enemy in EnemyManager.Instance.Enemies)
                {
                    Player.TakeDamage(enemy.Attack());
                }

                RollCountText.ChangeText("적은 ___의 데미지를 입었습니다.");
                if (EnemyManager.Instance.CheckAllDeadEnemy())
                {
                    break;
                }
                // -------------------------------------
            }


            await WaitForUserInput();
            Current = RoundStateType.WinEnemy;
            // 승리 ------------------------------

            RollCountText.ChangeText("승리.");

            // -------------------------------------
            await WaitForUserInput();
            Current = RoundStateType.LoseEnemy;
            // 패배 ------------------------------

            RollCountText.ChangeText("패배.");

            // -------------------------------------
            await WaitForUserInput();
            Current = RoundStateType.ResultSession;
            // 결과창 ------------------------------

            RollCountText.ChangeText("결과.");

            // -------------------------------------
        }

    }


    public void Roll()
    {
        foreach (Dice dice in Player.HaveDices)
        {
            // 돌릴 수 있는 상태만 돌림
            if (dice.State == DiceState.ToBeRolled)
            {
                dice.Roll();
            }
        }
    }

    public void OnClick()
    {
        // 유저가 클릭했을 때 호출되는 메서드
        if (userInputTaskCompletionSource != null && !userInputTaskCompletionSource.Task.IsCompleted)
        {
            userInputTaskCompletionSource.SetResult(RoundStateType.DevGo);
        }
    }

    // OnSkill을 대신 처리해주는 함수
    public void OnSkillActive<T>(Func<DiceCalculateDto, T, Player, List<Enemy>, bool> onSkillFunction, T target) where T : Character
    {
        // 스킬을 사용했을 때 액티브
        if (userInputTaskCompletionSource != null && !userInputTaskCompletionSource.Task.IsCompleted)
        {
            bool result = onSkillFunction(Instance.DiceDTO, target, Player, EnemyManager.Instance.Enemies);
            userInputTaskCompletionSource.SetResult(RoundStateType.ActiveSkill);
        }
    }

    private Task<RoundStateType> WaitForUserInput()
    {
        userInputTaskCompletionSource = new TaskCompletionSource<RoundStateType>();
        return userInputTaskCompletionSource.Task;
    }

    string ConvertDictionaryToText(SortedDictionary<CombiType, long> dictionary)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (KeyValuePair<CombiType, long> kvp in dictionary)
        {
            sb.AppendLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        return sb.ToString();
    }

    private void ChangeRoundState(RoundStateType RoundToState)
    {
        RoundPreviousState = RoundCurrentState;
        RoundCurrentState = RoundToState;
    }
}
