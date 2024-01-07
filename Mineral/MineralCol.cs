using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MXZOO.Mineral
{
    public static class MineralCol
    {
        /// <summary>
        ///     初始化生成地队列池
        /// </summary>
        /// <param name="data"></param>
        public static void OnInit(MineralAreaData data)
        {
            data.SilicateMinerals.Clear();
            data.SulfideMinerals.Clear();
            data.OxideMinerals.Clear();
        }

        /// <summary>
        ///     生成单个矿物
        /// </summary>
        /// <typeparam name="T">矿物生成地类型枚举</typeparam>
        /// <param name="data">数据保存地</param>
        /// <param name="mineralData">矿物详细数据</param>
        /// <param name="range">矿物生成范围</param>
        public static void OnCreate<T>(MineralAreaData data, MineralStyle mineralData, Vector3 pos, float range)
            where T : struct, Enum
        {
            // 获取矿物生成地
            var area = data.GetMineralArea<T>();
            // 生成矿物
            var newData = new MineralArea<T>
            {
                Range = range,
                Area = pos,
                Mineral = mineralData
            };
            // 添加到生成地
            area.Add(newData);
            // 保存生成地 
            data.SetMineralArea(area);
        }

        public static void OnRemove<T>(MineralAreaData data, int ID) where T : struct, Enum
        {
            var area = data.GetMineralArea<T>();
            area.RemoveAt(ID);
            data.SetMineralArea(area);
        }

        public static float RandomRange(float minRange, float maxRange)
        {
            return Random.Range(minRange, maxRange);
        }

        public static Vector3 RandomVector3(MineralVector3 vector)
        {
            var x = Random.Range(vector.xRange.Min, vector.xRange.Max);
            var z = Random.Range(vector.zRange.Min, vector.zRange.Max);
            const float y = 0f;

            return vector.StartPosition + new Vector3(x, y, z);
        }

        public static Vector3 RV(MineralVector3 vector)
        {
            return RandomVector3(vector);
        }
    }
}