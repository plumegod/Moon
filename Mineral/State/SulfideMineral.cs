using UnityEngine;

namespace MXZOO.Mineral
{
    [CreateAssetMenu(fileName = "SulfideMineralAreaData_", menuName = "Mineral/MineralAreaData/SulfideMineral")]
    public class SulfideMineral : MineralStyle
    {
        [SerializeField] private MineralState<SulfideType> mineralState;

        public MineralState<SulfideType> MineralState => mineralState;
    }
}