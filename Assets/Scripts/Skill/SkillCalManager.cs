using System;
using System.Collections.Generic;

public class SkillCalManager : Singleton<SkillCalManager>
{

    // 타겟에 따른 Check는 다른 함수에서 하고,
    // 스킬을 적중시키는 것만 다룸
    public virtual int OnDefinedSkill<T>(Skill skill, DiceInfo diceDto, T target) where T : Character
    {
        // 체력만 데미지 준 양
        int takeHealthDamage = 0;

        var player = GameSession.Ins.Player;
        var enemies = EnemyManager.Ins.Enemies;

        // 해당하는 것만 바꿀 수 있게 최적화
        var largePip = diceDto.GetLargePip(skill.Combi);
        List<KeyValuePair<EFormula, string>> replaceFormulas = new();

        // 전처리 (텍스트 대치)
        // bool isChanger = false;

        // switch (skill.Changer)
        // {
        //     case EChanger.None:
        //         break;
        //     case EChanger.ContainPip:
        //         var pips = skill.ChangerValue.Split(",");
        //         foreach (var pip in pips)
        //         {
        //             if (diceDto.CountList[int.Parse(pip)] >= 1)
        //             {
        //                 isChanger = true;
        //             }
        //         }
        //         break;
        //     case EChanger.LowHp:
        //         if (player.HP <= int.Parse(skill.ChangerValue))
        //         {
        //             isChanger = true;
        //         }
        //         break;
        // }
        // 부합하는지 확인
        var formulas = skill.Formulas;
        // if (isChanger)
        // {
        //     formulas = skill.ChangerFormulas;
        // }

        // Repeat 선처리
        // foreach (var formula in formulas)
        // {
        //     EFormula f = formula.Type;
        //     string s = formula.Value;
        //     KeyValuePair<EFormula, string> replaceKeyPair = new(f, s.Replace("{largePip}", largePip.ToString()));
        //     if (f == EFormula.Repeat)
        //     {
        //         repeatCount = replaceKeyPair.Value.EvaluateFormula();
        //         continue;
        //     }
        //     replaceFormulas.Add(replaceKeyPair);
        // }

        // 스킬 효과 발동
        // for (var i = 0; i < repeatCount; i++)   // 반복만 따로 처리
        // {
        //     foreach (var formula in formulas) // Formula 순회
        //     {
        //         EFormula f = formula.Type;
        //         string s = formula.Value;
        //         string replaceKeyPair = s.Replace("{largePip}", largePip.ToString());

        //         int value = replaceKeyPair.EvaluateFormula();
        //         switch (EFormula.TargetAttack)
        //         {
        //             case EFormula.TargetAttack:
        //                 takeHealthDamage += target.TakeDamage(value + player.GetStateCondition(EStateCondition.Empower));
        //                 break;
        //             case EFormula.AllAttack:
        //                 foreach (var enemy in enemies)
        //                 {
        //                     takeHealthDamage += enemy.TakeDamage(value + player.GetStateCondition(EStateCondition.Empower));
        //                 }
        //                 break;
        //             case EFormula.PlayerShieldUp:
        //                 player.Shield += value;
        //                 break;
        //             case EFormula.PlayerShieldDown:
        //                 player.Shield = Math.Max(player.Shield + value, 0);
        //                 break;
        //             case EFormula.PlayerEmpowerUp:
        //                 player.UpdateCondition(EStateCondition.Empower, value);
        //                 break;
        //             case EFormula.PlayerEmpowerDown:
        //                 player.UpdateCondition(EStateCondition.Empower, -value);
        //                 break;
        //             case EFormula.PlayerHeal:
        //                 player.HP += value;
        //                 break;
        //             case EFormula.TargetMarkUp:
        //                 target.UpdateCondition(EStateCondition.Mark, value);
        //                 break;
        //             case EFormula.TargetMarkDown:
        //                 target.UpdateCondition(EStateCondition.Mark, -value);
        //                 break;
        //         }
        //     }
        // }

        return takeHealthDamage;
    }
}