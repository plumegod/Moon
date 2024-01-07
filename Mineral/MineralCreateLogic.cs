using System;
using System.Collections.Generic;
using UnityEngine;

namespace MXZOO.Mineral
{
    public static class MineralCreateLogic
    {

        private static readonly int randomCount = 30;
        private static MineralVector3 CreateRange(float value)
        {
            var pos = new MineralVector3
            {
                StartPosition = Vector3.zero,
                xRange = new MineralRange
                {
                    Min = -value,
                    Max = value
                },
                zRange = new MineralRange
                {
                    Min = -value,
                    Max = value
                }
            };
            return pos;
        }

        private static void CreateMineral(SilicateMineral data, MineralVector3 pos, float minRange, float maxRange)
        {
            int count = 0;
            while (count < randomCount)
            {
                if (GameManager.Instance.CreateMineral<SilicateType>(data, MineralCol.RV(pos), minRange, maxRange))
                {
                    break; // 当GameManager.Instance.CreateMineral<SilicateType>(data, MineralCol.RV(pos), 5, 10)返回true时退出循环
                }
                count++;
            }
        }
        private static void CreateMineral(OxideMineral data, MineralVector3 pos, float minRange, float maxRange)
        {
            int count = 0;
            while (count < randomCount)
            {
                if (GameManager.Instance.CreateMineral<OxideType>(data, MineralCol.RV(pos), minRange, maxRange))
                {
                    break; // 当GameManager.Instance.CreateMineral<SilicateType>(data, MineralCol.RV(pos), 5, 10)返回true时退出循环
                }
                count++;
            }
        }
        private static void CreateMineral(SulfideMineral data, MineralVector3 pos, float minRange, float maxRange)
        {
            int count = 0;
            while (count < randomCount)
            {
                if (GameManager.Instance.CreateMineral<SulfideType>(data, MineralCol.RV(pos), minRange, maxRange))
                {
                    break; // 当GameManager.Instance.CreateMineral<SilicateType>(data, MineralCol.RV(pos), 5, 10)返回true时退出循环
                }
                count++;
            }
        }

        /// <summary>
        ///     矿物生成逻辑
        /// </summary>
        /// <param name="data">数据保存地址</param>
        public static void OnLogic(MineralData data)
        {
            var pos = CreateRange(50);
            CreateMineral(data.SilicateMinerals[0], pos, 5, 10);
            CreateMineral(data.SilicateMinerals[0], pos, 5, 10);
            CreateMineral(data.OxideMinerals[0], pos, 10, 30);
            CreateMineral(data.OxideMinerals[0], pos, 15, 40);
            CreateMineral(data.OxideMinerals[1], pos, 15, 40);
            CreateMineral(data.SulfideMinerals[0], pos, 10, 40);
            
            var pos2 = CreateRange(150);
            CreateMineral(data.SulfideMinerals[1], pos2, 10, 20);
            CreateMineral(data.SulfideMinerals[2], pos2, 10, 15);
            CreateMineral(data.OxideMinerals[2], pos2, 10, 30);
            CreateMineral(data.OxideMinerals[2], pos2, 10, 30);
            CreateMineral(data.SulfideMinerals[1], pos2, 10, 20);
            CreateMineral(data.SulfideMinerals[2], pos2, 10, 15);
            CreateMineral(data.OxideMinerals[2], pos2, 10, 30);
            CreateMineral(data.OxideMinerals[2], pos2, 10, 30);

            var pos3 = CreateRange(250);
            CreateMineral(data.SilicateMinerals[1], pos3, 15, 20);
            CreateMineral(data.SilicateMinerals[0], pos3, 6, 15);
            CreateMineral(data.SilicateMinerals[0], pos3, 3, 12);
            CreateMineral(data.OxideMinerals[2], pos3, 10, 15);
            CreateMineral(data.OxideMinerals[4], pos3, 6, 20);
            CreateMineral(data.OxideMinerals[0], pos3, 12, 20);
            CreateMineral(data.OxideMinerals[0], pos3, 8, 20);
            CreateMineral(data.SilicateMinerals[1], pos3, 15, 20);
            CreateMineral(data.SilicateMinerals[0], pos3, 6, 15);
            CreateMineral(data.SilicateMinerals[0], pos3, 3, 12);
            CreateMineral(data.OxideMinerals[2], pos3, 10, 15);
            CreateMineral(data.OxideMinerals[4], pos3, 6, 20);
            CreateMineral(data.OxideMinerals[0], pos3, 12, 20);
            CreateMineral(data.OxideMinerals[0], pos3, 8, 20);

            var pos4 = CreateRange(350);
            CreateMineral(data.OxideMinerals[1], pos4, 10, 30);
            CreateMineral(data.OxideMinerals[0], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SulfideMinerals[1], pos4, 10, 20);
            CreateMineral(data.OxideMinerals[1], pos4, 10, 30);
            CreateMineral(data.OxideMinerals[0], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SulfideMinerals[1], pos4, 10, 20);
            CreateMineral(data.OxideMinerals[1], pos4, 10, 30);
            CreateMineral(data.OxideMinerals[0], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SulfideMinerals[1], pos4, 10, 20);
            CreateMineral(data.OxideMinerals[1], pos4, 10, 30);
            CreateMineral(data.OxideMinerals[0], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SilicateMinerals[3], pos4, 10, 30);
            CreateMineral(data.SulfideMinerals[1], pos4, 10, 20);

            var pos5 = CreateRange(450);
            CreateMineral(data.OxideMinerals[3], pos5, 10, 30);
            CreateMineral(data.OxideMinerals[4], pos5, 10, 15);
            CreateMineral(data.SilicateMinerals[2], pos5, 6, 15);
            CreateMineral(data.SilicateMinerals[3], pos5, 6, 10);
            CreateMineral(data.SulfideMinerals[3], pos5, 10, 20);
            CreateMineral(data.OxideMinerals[3], pos5, 10, 30);
            CreateMineral(data.OxideMinerals[4], pos5, 10, 15);
            CreateMineral(data.SilicateMinerals[2], pos5, 6, 15);
            CreateMineral(data.SilicateMinerals[3], pos5, 6, 10);
            CreateMineral(data.SulfideMinerals[3], pos5, 10, 20);

            
            var pos6 = CreateRange(500);
            CreateMineral(data.SulfideMinerals[1], pos6, 10, 20);
            CreateMineral(data.SulfideMinerals[2], pos6, 10, 15);
            CreateMineral(data.OxideMinerals[2], pos6, 10, 30);
            CreateMineral(data.OxideMinerals[2], pos6, 10, 30);
            CreateMineral(data.SilicateMinerals[0], pos6, 5, 10);
            CreateMineral(data.OxideMinerals[0], pos6, 10, 30);
            CreateMineral(data.OxideMinerals[0], pos6, 15, 40);
            CreateMineral(data.OxideMinerals[1], pos6, 15, 40);
            CreateMineral(data.SulfideMinerals[0], pos6, 10, 40);
            CreateMineral(data.OxideMinerals[4], pos6, 5, 15);
            CreateMineral(data.OxideMinerals[4], pos6, 5, 15);
            CreateMineral(data.SilicateMinerals[4], pos6, 5, 15);
            CreateMineral(data.SilicateMinerals[4], pos6, 5, 15);
            CreateMineral(data.SulfideMinerals[3], pos6, 5, 10);
            CreateMineral(data.SulfideMinerals[1], pos6, 10, 20);
            CreateMineral(data.SulfideMinerals[2], pos6, 10, 15);
            CreateMineral(data.OxideMinerals[2], pos6, 10, 30);
            CreateMineral(data.OxideMinerals[2], pos6, 10, 30);
            CreateMineral(data.SilicateMinerals[0], pos6, 5, 10);
            CreateMineral(data.OxideMinerals[0], pos6, 10, 30);
            CreateMineral(data.OxideMinerals[0], pos6, 15, 40);
            CreateMineral(data.OxideMinerals[1], pos6, 15, 40);
            CreateMineral(data.SulfideMinerals[0], pos6, 10, 40);
            CreateMineral(data.OxideMinerals[4], pos6, 5, 15);
            CreateMineral(data.OxideMinerals[4], pos6, 5, 15);
            CreateMineral(data.SilicateMinerals[4], pos6, 5, 15);
            CreateMineral(data.SilicateMinerals[4], pos6, 5, 15);
            CreateMineral(data.SulfideMinerals[3], pos6, 5, 10);

        }
    }
}