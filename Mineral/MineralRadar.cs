using System;
using System.Collections.Generic;
using UnityEngine;

namespace MXZOO.Mineral
{
    public class ObjectPool
    {
        private Queue<GameObject> pool = new Queue<GameObject>();

        public void AddToPool(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        public GameObject GetFromPool()
        {
            var newObj = new GameObject();
            return newObj;
        }
        
        public void SetAllActive()
        {
            foreach (var obj in pool)
            {
                obj.SetActive(true);
            }
        }
    }

    public class MineralRadar : SingletonMono<MineralRadar>
    {
        private static ObjectPool mineralPool = new ObjectPool();
        private EventBinding<GameNightEndEvent> gameStartEvent;
        private List<float> code = new List<float>();


        private void OnEnable()
        {
            gameStartEvent = new EventBinding<GameNightEndEvent>(mineralPool.SetAllActive);
            EventBus<GameNightEndEvent>.Register(gameStartEvent);
        }
        
        private void OnDisable()
        {
            EventBus<GameNightEndEvent>.Deregister(gameStartEvent);
        }

        public static bool CheckCode(float value)
        {
            // 检查code内是否有value，如果有就返回false，如果没有就添加并且返回true
            if (Instance.code.Contains(value))
            {
                return false;
            }
            else
            {
                Instance.code.Add(value);
                return true;
            }
        }
        
        public static void CreateMineralPoint(Vector3 pos, MineralType type)
        {
            var mineral = mineralPool.GetFromPool();

            mineral.tag = type switch
            {
                MineralType.Silicate => "MineralSilicate",
                MineralType.Sulfide => "MineralSulfide",
                MineralType.Oxide => "MineralOxygen",
                _ => mineral.tag
            };

            mineral.transform.parent = Instance.transform;
            mineral.transform.position = pos;

            mineralPool.AddToPool(mineral);
        }
    }
}