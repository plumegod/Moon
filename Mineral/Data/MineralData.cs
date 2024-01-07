using System;
using System.Collections.Generic;
using UnityEngine;

namespace MXZOO.Mineral
{
    [CreateAssetMenu(fileName = "MineralData_", menuName = "Mineral/MineralData")]
    public class MineralData : ScriptableObject
    {
        [Header("硅化物")] [SerializeField] private List<SilicateMineral> silicateMinerals;

        [Header("氧化物")] [SerializeField] private List<OxideMineral> oxideMinerals;

        [Header("硫化物")] [SerializeField] private List<SulfideMineral> sulfideMinerals;

        public List<SilicateMineral> SilicateMinerals => silicateMinerals;
        public List<OxideMineral> OxideMinerals => oxideMinerals;
        public List<SulfideMineral> SulfideMinerals => sulfideMinerals;
    }

    [Serializable]
    public class MineralElementState
    {
        [SerializeField] private MineralElement mineralElement;
        [SerializeField] private MineralFloat percentage;

        public MineralElement MineralElement => mineralElement;
        public MineralFloat Percentage => percentage;
    }

    [Serializable]
    public class MineralRange
    {
        [SerializeField] private float min;
        [SerializeField] private float max;

        public float Min
        {
            get => min;
            set => min = value;
        }

        public float Max
        {
            get => max;
            set => max = value;
        }
    }

    [Serializable]
    public class MineralFloat
    {
        [SerializeField] private float num;

        public float Num
        {
            get => num;
            set => num = value;
        }
    }

    [Serializable]
    public class MineralVector3
    {
        public Vector3 StartPosition;
        public MineralRange xRange;
        public MineralRange zRange;
    }
}