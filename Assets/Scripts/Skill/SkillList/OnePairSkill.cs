using System;
using UnityEngine;

public class OnePairSkill : Skill
{
    // 생성자에서 멤버 변수 초기화
    public OnePairSkill()
    {
        Initialize(SkillID.OnePair);
    }

    // IsPossible 상태는 스킬 매니저에서 바꿔주는 것을 의도함
    public override bool OnCheck(DiceCalculateDto diceDto)
    {
        return diceDto.SortedCountList[0].Value >= 2;
    }

    // TODO: true시 사용 성공한 것으로 처리
    public override bool OnSkill<T>(DiceCalculateDto diceDto, T target)
    {
        if (target is Player p)
        {
            return false;
        }
        else if (target is Enemy e)
        {
            if (diceDto.PairLargePips.TryGetValue(2, out int largePip))
            {
                int damage = CalculateDamage(largePip);
                target.TakeDamage(damage);

                return true;
            }
        }
        return false;
    }
    public override void UpdateDescription(DiceCalculateDto diceDto, Player p)
    {
        if (OnCheck(diceDto))
        {
            if (diceDto.PairLargePips.TryGetValue(2, out int largePip))
            {
                Description = DamageFormula.Replace("{LargePip}", largePip.ToString());
                return;
            }
        }
        Description = DamageFormula;
    }
}
