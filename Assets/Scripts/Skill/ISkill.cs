public interface ISkill
{
    // 오로지 주사위로 가능한지 여부 체크
    bool OnCheck(DiceCalculateDto diceDto, Sk_Context c);

    // 스킬 사용 시 효과. 핵심
    int OnSkill(DiceCalculateDto diceDto, Sk_Context c);

    // 현재 상태에 따라 설명 업데이트
    void UpdateDescription(DiceCalculateDto diceDto, Player p);
}