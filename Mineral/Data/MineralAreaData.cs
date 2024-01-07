using System;
using System.Collections.Generic;
using UnityEngine;

namespace MXZOO.Mineral
{ 
    [CreateAssetMenu(fileName = "MineralAreaData_", menuName = "Mineral/MineralAreaData")]
public class MineralAreaData : ScriptableObject
{
    [SerializeField] private List<MineralArea<SilicateType>> silicateMinerals;
    [SerializeField] private List<MineralArea<OxideType>> oxideMinerals;
    [SerializeField] private List<MineralArea<SulfideType>> sulfideMinerals;

    /// <summary>
    ///     硅化物生成队列
    /// </summary>
    public List<MineralArea<SilicateType>> SilicateMinerals
    {
        get => silicateMinerals;
        set => silicateMinerals = value;
    }

    /// <summary>
    ///     氧化物生成队列
    /// </summary>
    public List<MineralArea<OxideType>> OxideMinerals
    {
        get => oxideMinerals;
        set => oxideMinerals = value;
    }

    /// <summary>
    ///     硫化物生成队列
    /// </summary>
    public List<MineralArea<SulfideType>> SulfideMinerals
    {
        get => sulfideMinerals;
        set => sulfideMinerals = value;
    }

    /// <summary>
    ///     返回一个地区List
    /// </summary>
    /// <typeparam name="T">矿物类型枚举</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public List<MineralArea<T>> GetMineralArea<T>() where T : struct, Enum
    {
        if (typeof(T) == typeof(SilicateType))
            return SilicateMinerals as List<MineralArea<T>>;
        if (typeof(T) == typeof(OxideType))
            return OxideMinerals as List<MineralArea<T>>;
        if (typeof(T) == typeof(SulfideType))
            return SulfideMinerals as List<MineralArea<T>>;
        throw new ArgumentException($"[{name}]: 该类型无效");
    }

    /// <summary>
    ///     将一个地区List赋值给对应的矿物类型
    /// </summary>
    /// <param name="data">数据</param>
    /// <typeparam name="T">矿物类型枚举</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public void SetMineralArea<T>(List<MineralArea<T>> data) where T : struct, Enum
    {
        if (typeof(T) == typeof(SilicateType))
        {
            SilicateMinerals = data as List<MineralArea<SilicateType>>;
        }
        else if (typeof(T) == typeof(OxideType))
        {
            OxideMinerals = data as List<MineralArea<OxideType>>;
        }
        else if (typeof(T) == typeof(SulfideType))
        {
            SulfideMinerals = data as List<MineralArea<SulfideType>>;
        }
        else
        {
            throw new ArgumentException($"[{name}]: 该类型无效");
        }
    }
}

/// <summary>
///     矿物生成地区
/// </summary>
/// <typeparam name="T">矿物类型</typeparam>
[Serializable]
public class MineralArea<T> where T : struct, Enum
{
    public Vector3 Area;
    public float Range;

    public MineralStyle Mineral;
}
}
