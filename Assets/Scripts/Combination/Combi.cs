using System.Collections.Generic;

public abstract class Combi
{
    public bool IsPossible { get; private set; }    // 가능 여부
    public long Score;
    public CombinationType Type { get; set; }       // 타입 매칭
    public int Pip { get; set; }                    // 눈

    // 파생 클래스에서 구현할 메서드
    protected abstract long CheckScore(DiceCalculateDto diceCalculateDto);   // 점수 계산
    protected abstract bool CheckPossible(DiceCalculateDto diceCalculateDto);    // 가능 여부 계산

    public void ChangeAll(DiceCalculateDto diceCalculateDto)      // 상태 변경
    {
        if (CheckPossible(diceCalculateDto))
        {
            Score = CheckScore(diceCalculateDto);
            IsPossible = true;
        }
        else
        {
            IsPossible = false;
        }
    }
}
