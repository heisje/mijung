// using System;
// using Unity.VisualScripting;

// public class LargeStraightSkill : Skill
// {
//     // 생성자에서 멤버 변수 초기화
//     public LargeStraightSkill()
//     {
//         Initialize(SkillID.LargeStraight);
//     }

//     // IsPossible 상태는 스킬 매니저에서 바꿔주는 것을 의도함
//     public override bool OnCheck(DiceCalculateDto diceDto)
//     {
//         return diceDto.MaxStraightCount >= 5;
//     }

//     // TODO: true시 사용 성공한 것으로 처리
//     public override bool OnSkill<T>(DiceCalculateDto diceDto, T target)
//     {
//         if (target is Player p)
//         {
//             return false;
//         }
//         else if (target is Enemy e)
//         {
//             int damage = EvaluateFormula(DamageFormula);
//             target.TakeDamage(damage);
//         }
//         return false;
//     }
//     public override void UpdateDescription(DiceCalculateDto diceDto, Player p)
//     {
//         if (OnCheck(diceDto))
//         {
//             Description = DamageFormula;
//             return;
//         }
//         Description = DamageFormula;
//     }
// }
