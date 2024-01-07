using System;
using System.Collections.Generic;
using UnityEngine;

namespace MXZOO.Mineral
{
    [CreateAssetMenu(fileName = "MineralBag_", menuName = "Mineral/MineralBag")]
    public class MineralBags : ScriptableObject
    {
        [SerializeField] private List<MineralBag> bag;

        public List<MineralBag> Bag => bag;

        public bool AddBag(MineralBag value)
        {
            bag.Add(value);
            return true;
        }

        public bool ClearBag()
        {
            bag.Clear();
            return true;
        }
    }

    [Serializable]
    public class MineralBag
    {
        [SerializeField] private MineralStyle mineral;
        [SerializeField] private float hashCode;

        public MineralStyle Mineral
        {
            get => mineral;
            set => mineral = value;
        }

        public float HashCode
        {
            get => hashCode;
            set => hashCode = value;
        }
    }
}