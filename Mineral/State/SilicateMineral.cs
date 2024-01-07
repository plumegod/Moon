using UnityEngine;

namespace MXZOO.Mineral
{
    [CreateAssetMenu(fileName = "SilicateMineralAreaData_", menuName = "Mineral/MineralAreaData/SilicateMineral")]
    public class SilicateMineral : MineralStyle
    {
        [SerializeField] private MineralState<SilicateType> mineralState;

        public MineralState<SilicateType> MineralState => mineralState;
    }
}