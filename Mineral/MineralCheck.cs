using System.Linq;
using MXZOO.Mineral;
using UnityEngine;

public static class MineralCheck
{
    /// <summary>
    ///     检查是否挖到了矿物
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static MineralBag CheckStyle(Vector3 pos)
    {
        var data = GameManager.Instance.MineralAreaData;

        foreach (var mineralArea in from mineralArea in data.SilicateMinerals
                 let distance = Vector3.Distance(pos, mineralArea.Area)
                 where distance < mineralArea.Range
                 select mineralArea)
            return new MineralBag { Mineral = mineralArea.Mineral, HashCode = mineralArea.Area.GetHashCode() };

        foreach (var mineralArea in from mineralArea in data.OxideMinerals
                 let distance = Vector3.Distance(pos, mineralArea.Area)
                 where distance < mineralArea.Range
                 select mineralArea)
            return new MineralBag { Mineral = mineralArea.Mineral, HashCode = mineralArea.Area.GetHashCode() };

        foreach (var mineralArea in from mineralArea in data.SulfideMinerals
                 let distance = Vector3.Distance(pos, mineralArea.Area)
                 where distance < mineralArea.Range
                 select mineralArea)
            return new MineralBag { Mineral = mineralArea.Mineral, HashCode = mineralArea.Area.GetHashCode() };

        return new MineralBag { Mineral = null, HashCode = 0 };
    }

    public static MineralType CheckType(Vector3 pos)
    {
        var data = GameManager.Instance.MineralAreaData;
        foreach (var mineralArea in from mineralArea in data.SilicateMinerals
                 let distance = Vector3.Distance(pos, mineralArea.Area)
                 where distance < mineralArea.Range
                 select mineralArea)
            return MineralType.Silicate;

        foreach (var mineralArea in from mineralArea in data.OxideMinerals
                 let distance = Vector3.Distance(pos, mineralArea.Area)
                 where distance < mineralArea.Range
                 select mineralArea)
            return MineralType.Oxide;

        foreach (var mineralArea in from mineralArea in data.SulfideMinerals
                 let distance = Vector3.Distance(pos, mineralArea.Area)
                 where distance < mineralArea.Range
                 select mineralArea)
            return MineralType.Sulfide;

        return MineralType.None;
    }
    
}