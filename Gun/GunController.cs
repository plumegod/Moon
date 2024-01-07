using Cysharp.Threading.Tasks;
using MXZOO.Input;
using MXZOO.Interface;
using MXZOO.Mineral;
using MXZOO.UIFrame;
using UnityEngine;

public struct GunFireEvent : IEvent
{
    
}
public class GunController : MonoBehaviour
{
    [Header("物体数据")] [SerializeField] private GameObject gun;

    [SerializeField] private Transform gunShootPoint;

    [SerializeField] private float gunDistance = 10f;
    [SerializeField] private float gunCD = 5f;
    [SerializeField] private Animator ani;
    
    private InputReader _input;
    private float nowGunCD = 0f;
    
    private EventBinding<GameNightEvent> gameEndEventBinding;
    private EventBinding<GameStartEvent> gameStartEventBinding;
    private EventBinding<GunFireEvent> GunFireEventBinding;

    private void OnEnable()
    {
        gameStartEventBinding = new EventBinding<GameStartEvent>(OnUseGun);
        gameEndEventBinding = new EventBinding<GameNightEvent>(OnUsedGun);
        GunFireEventBinding = new EventBinding<GunFireEvent>(GetMineral);

        EventBus<GameStartEvent>.Register(gameStartEventBinding);
        EventBus<GameNightEvent>.Register(gameEndEventBinding);
        EventBus<GunFireEvent>.Register(GunFireEventBinding);
    }

    private void OnDisable()
    {
        EventBus<GameStartEvent>.Deregister(gameStartEventBinding);
        EventBus<GameNightEvent>.Deregister(gameEndEventBinding);
        EventBus<GunFireEvent>.Deregister(GunFireEventBinding);
    }


    private void OnDrawGizmos()
    {
        // 绘制起点
        Gizmos.color = Color.green;
        //Gizmos.DrawSphere(gunShootPoint.position, 0.03f);

        // 绘制射线
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gunShootPoint.position, gunShootPoint.forward * 100);
    }

    private void OnUseGun()
    {
        _input = InputController.Instance.InputReader;
        _input.OnInteractEvent += ShootRay;
    }

    private void OnUsedGun()
    {
        _input.OnInteractEvent -= ShootRay;
    }
    
    private async UniTaskVoid GunCD()
    {
        nowGunCD = gunCD;
        while (nowGunCD > 0)
        {
            await UniTask.Delay(100);
            nowGunCD -= 0.1f;
            
        }
    }

    private void ShootRay()
    {
        if(nowGunCD > 0)
        {
            EventBus<PlayerUpTextEvent>.Raise(new PlayerUpTextEvent()
            {
                Text = "挖矿枪冷却中 ( " + nowGunCD.ToString("F2") + " ) 秒",
                Time = 2f
            });
            return;
        }
        else
        {
            ani.SetTrigger("IsFire");
        }
    }

    private void GetMineral()
    {
        Debug.Log("1");
        var ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, gunDistance))
        {
            var hitObject = hit.collider.gameObject;

            // 检查碰撞到的物体是否有IShootable接口
            var shootable = hitObject.GetComponent<IShootable>();
            if (shootable != null)
                // 执行物体的接口事件
                shootable.OnShot();
            //if (hitObject.CompareTag("Area"))
            CheckMineral(hit.point);
            GunCD().Forget();
        }
    }

    /// <summary>
    /// 挖掘到矿物
    /// </summary>
    /// <param name="pos"></param>
    private void CheckMineral(Vector3 pos)
    {
        var mineral = MineralCheck.CheckStyle(pos);
        var type = MineralCheck.CheckType(pos);
        EventBus<PlayerLeftTextEvent>.Raise(new PlayerLeftTextEvent()
        {
            Text = "未分析矿物" + "  +1"
            ,Time = 3f
        });
        // 矿物为空则挖不倒
        if(mineral.Mineral is null) return;
        GameManager.Instance.MineralBags.AddBag(mineral);
        if(MineralRadar.CheckCode(mineral.HashCode))
            MineralRadar.CreateMineralPoint(pos, type);
    }
}