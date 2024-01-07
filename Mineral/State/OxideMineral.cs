using UnityEngine;

namespace MXZOO.Mineral
{
    [CreateAssetMenu(fileName = "OxideMineralAreaData_", menuName = "Mineral/MineralAreaData/OxideMineral")]
    public class OxideMineral : MineralStyle
    {
        [SerializeField] private MineralState<OxideType> mineralState;

        public MineralState<OxideType> MineralState => mineralState;
    }
}