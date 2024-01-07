using System;
using System.Collections.Generic;
using MXZOO.Mineral;
using Random = UnityEngine.Random;

public static class MineralScore
{
    private static readonly Dictionary<MineralElement, float> rarityScores = new()
    {
        { MineralElement.O, 10 },
        { MineralElement.Si, 20 },
        { MineralElement.Al, 30 },
        { MineralElement.Fe, 40 },
        { MineralElement.Ca, 50 },
        { MineralElement.Na, 60 },
        { MineralElement.Ti, 70 },
        { MineralElement.Mg, 80 },
        { MineralElement.S, 90 },
        { MineralElement.C, 100 },
        { MineralElement.H, 110 },
        { MineralElement.Cu, 120 },
        { MineralElement.Mo, 130 },
        { MineralElement.Pb, 140 },
        { MineralElement.N, 150 },
        { MineralElement.Cr, 160 }
    };

    // 分数计算
    public static float GetMineralScore(MineralBags bag)
    {
        var score = 0f;
        foreach (var mineralBag in bag.Bag)
        {
            var mineral = mineralBag.Mineral;
            var origenScore = GetMineralPoint(mineral);
            var otherMineral = GetOtherMineralHashCode(bag, mineralBag.HashCode);
            var newScore = origenScore / otherMineral;
            score += newScore;
        }

        return score;
    }

    private static float GetOtherMineralHashCode(MineralBags bag,float mineralCode)
    {
        var otherCode = 0f;
        foreach (var mineral in bag.Bag)
        {
            if (Math.Abs(mineral.HashCode - mineralCode) < 1)
                otherCode++;
        }
        return otherCode;
    }

    public static float GetMineralPoint(MineralStyle value)
    {
        var score = 0f;
        if (value is OxideMineral oxideMineral)
        {
            // 基础分数
            score += 10 * Random.Range(0.9f, 1.1f);
            // PH分数
            score += GetPHPoint(oxideMineral.MineralState.PH.Num);
            // 水分分数
            score += GetWaterContentPoint(oxideMineral.MineralState.WaterContent.Num);
            // 元素分数
            score += GetElementPoint(oxideMineral.MineralState.MineralElementStates);
        }

        if (value is SilicateMineral silicateMineral)
        {
            score += 30 * Random.Range(0.9f, 1.1f);
            score += GetPHPoint(silicateMineral.MineralState.PH.Num);
            score += GetWaterContentPoint(silicateMineral.MineralState.WaterContent.Num);
            score += GetElementPoint(silicateMineral.MineralState.MineralElementStates);
        }

        if (value is SulfideMineral sulfideMineral)
        {
            score += 50 * Random.Range(0.9f, 1.1f);
            score += GetPHPoint(sulfideMineral.MineralState.PH.Num);
            score += GetWaterContentPoint(sulfideMineral.MineralState.WaterContent.Num);
            score += GetElementPoint(sulfideMineral.MineralState.MineralElementStates);
        }

        return score;
    }


    private static float GetPHPoint(float ph)
    {
        var score = 25; // 基础分数
        var deviation = Math.Abs(7 - ph); // 计算偏离7的程度

        // 根据偏离程度逐渐减分
        for (var i = 1; i <= deviation; i++) score -= (int)Math.Pow(2, i); // 每偏离1点，减少的是上一点的两倍

        // 最低得分为0
        if (score < 0) score = 0;

        return score;
    }

    private static float GetWaterContentPoint(float waterContent)
    {
        // 得分= 30 * e^(水的千分比)
        var score = 30 * Math.Exp(waterContent);

        return (float)score;
    }

    private static float GetElementPoint(List<MineralElementState> elementStates)
    {
        var score = 0f;
        foreach (var elementState in elementStates)
        {
            var element = elementState.MineralElement;
            var num = elementState.Percentage.Num;

            // 计算得分
            if (rarityScores.TryGetValue(element, out var rarityScore))
                // 分数 = 稀有度分数 * ln(元素含量比 + 1)
                score += rarityScore * (float)Math.Log(num + 1);
        }

        return score;
    }
}