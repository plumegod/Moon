using System;
using System.Collections.Generic;
using UnityEngine;

namespace MXZOO.Mineral
{
    [Serializable]
    public class MineralState<T> where T : struct, Enum
    {
        [SerializeField] private T mineralName;
        [SerializeField] private MineralFloat pH;
        [SerializeField] private MineralFloat waterContent;
        [SerializeField] private MineralType mineralType;
        [SerializeField] private List<MineralElementState> mineralElementStates;

        private string name;

        public T MineralName => mineralName;
        public MineralFloat PH => pH;
        public MineralFloat WaterContent => waterContent;
        public MineralType MineralType => mineralType;
        public List<MineralElementState> MineralElementStates => mineralElementStates;

        public void GetName()
        {
            name = mineralName.ToString();
        }
    }
}