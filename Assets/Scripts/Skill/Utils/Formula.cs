using System;
using System.Collections.Generic;
using System.Linq;

public static class Formula
{
    public static int EvaluateFormula(this string formula)
    {
        // DataTable을 사용하여 수식을 계산합니다.
        var dataTable = new System.Data.DataTable();

        // Compute 메서드를 사용하여 수식의 결과를 계산하고, 정수로 변환하여 반환합니다.
        return Convert.ToInt32(dataTable.Compute(formula, string.Empty));
    }

    public static int EvaluateFormula(this string formula, DiceCalculateDto diceDto, ECombi combi)
    {
        // DataTable을 사용하여 수식을 계산합니다.
        var dataTable = new System.Data.DataTable();

        string replaceKeyFormula = formula.Replace("{L_PIP}", diceDto.GetLargePip(combi).ToString());

        // Compute 메서드를 사용하여 수식의 결과를 계산하고, 정수로 변환하여 반환합니다.
        return Convert.ToInt32(dataTable.Compute(replaceKeyFormula, string.Empty));
    }

    public static int EvaluateFormula(this Skill skill, int i, DiceCalculateDto diceDto)
    {
        // DataTable을 사용하여 수식을 계산합니다.
        var dataTable = new System.Data.DataTable();

        string replaceKeyFormula = skill.Formulas[i].Replace("{L_PIP}", diceDto.GetLargePip(skill.Combi).ToString());

        // Compute 메서드를 사용하여 수식의 결과를 계산하고, 정수로 변환하여 반환합니다.
        return Convert.ToInt32(dataTable.Compute(replaceKeyFormula, string.Empty));
    }

    public static int[] EvaluateFormulas(this Skill skill, DiceCalculateDto diceDto)
    {
        var dataTable = new System.Data.DataTable();
        var largePipValue = diceDto.GetLargePip(skill.Combi).ToString();

        // LINQ로 Formulas 처리
        return skill.Formulas
            .Select(formula =>
            {
                var replacedFormula = formula.Replace("{L_PIP}", largePipValue); // {L_PIP}를 실제 값으로 교체
                return Convert.ToInt32(dataTable.Compute(replacedFormula, string.Empty)); // 수식을 계산하여 정수로 변환
            })
            .ToArray();
    }
}