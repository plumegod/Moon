using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using MXZOO;
using MXZOO.UIFrame;
using UnityEngine;

public struct PCUIExitEvent : IEvent
{
}

public struct GameNightEndEvent : IEvent
{
}

public class EndDayController : SingletonMono<EndDayController>
{
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraRotateSpeed;
    [SerializeField] private Transform cameraTf;
    [SerializeField] private Transform roleTf;
    [SerializeField] private Transform newDayTf;
    [SerializeField] private Animator ani;
    
    private Vector3 cameraPosition;
    private Quaternion cameraRotation;
    private bool isNightEnd;

    private Coroutine cameraMoveCoroutine;
    private EventBinding<GameNightEvent> gameNightEvent;
    private EventBinding<GameNightEndEvent> gameNightEndEvent;

    private EventBinding<GameDayEvent> gameStartEvent;
    private EventBinding<PCUIExitEvent> pcUIExitEvent;
    
    private EventBinding<OnUILoading> onUILoadingEvent;

    public bool IsNight { get; private set; }

    private void Start()
    {
        cameraPosition = cameraTf.position;
        cameraRotation = cameraTf.rotation;
    }

    private void OnEnable()
    {
        IsNight = false;
        gameStartEvent = new EventBinding<GameDayEvent>(StartDay);
        gameNightEvent = new EventBinding<GameNightEvent>(NightDay);
        pcUIExitEvent = new EventBinding<PCUIExitEvent>(PlayStandAnimation);
        gameNightEndEvent = new EventBinding<GameNightEndEvent>(EndDay);

        EventBus<GameDayEvent>.Register(gameStartEvent);
        EventBus<GameNightEvent>.Register(gameNightEvent);
        EventBus<PCUIExitEvent>.Register(pcUIExitEvent);
        EventBus<GameNightEndEvent>.Register(gameNightEndEvent);
    }

    private void OnDisable()
    {
        EventBus<GameDayEvent>.Deregister(gameStartEvent);
        EventBus<GameNightEvent>.Deregister(gameNightEvent);
        EventBus<PCUIExitEvent>.Deregister(pcUIExitEvent);
        EventBus<GameNightEndEvent>.Deregister(gameNightEndEvent);
    }


    private void OnTriggerEnter(Collider other)
    {
        OnShowUI();
        InputController.Instance.InputReader.OnClickEvent += CheckGame;
    }

    private void OnTriggerExit(Collider other)
    {
        EventBus<PlayerDownTextHideEvent>.Raise(new PlayerDownTextHideEvent());
        InputController.Instance.InputReader.OnClickEvent -= CheckGame;
    }

    private void NightDay()
    {
        IsNight = true;
        isNightEnd = false;
    }

    private void StartDay()
    {
        IsNight = false;
    }

    private void EndDay()
    {
        if (IsNight)
        {
            IsNight = false;
            isNightEnd = true;
        }
    }

    private void CheckGame()
    {
        OnTriggerExit(null);
        RoleController.Instance.SetStart(false);
        PlaySitAnimation().Forget();
    }

    private void OnShowUI()
    {
        EventBus<PlayerDownTextShowEvent>.Raise(new PlayerDownTextShowEvent { Text = GetText() });
    }

    private string GetText()
    {
        return GameManager.Instance.PCText;
    }

    private async UniTaskVoid PlaySitAnimation()
    {
        RoleController.Instance.SetStart(false);
        await UIFrame.Hide<PlayerUIPanel>();

        await MoveCamera();
        ani.Play("EndDay_Start");
    }

    private void PlayStandAnimation()
    {
        ani.Play("EndDay_End");
    }

    private async UniTask MoveCamera()
    {
        cameraTf.position = cameraPosition;
        cameraTf.rotation = cameraRotation;
        
        // 计算移动参数
        var targetPosition = cameraTf.position;
        var targetRotation = cameraTf.rotation;

        while (Vector3.Distance(CameraController.Instance.MainCamera.transform.position, targetPosition) > 0.01f ||
               Quaternion.Angle(CameraController.Instance.MainCamera.transform.rotation, targetRotation) > 0.01f)
        {
            CameraController.Instance.MainCamera.transform.position = Vector3.Lerp(
                CameraController.Instance.MainCamera.transform.position, targetPosition,
                cameraMoveSpeed * Time.deltaTime);
            CameraController.Instance.MainCamera.transform.rotation = Quaternion.Lerp(
                CameraController.Instance.MainCamera.transform.rotation, targetRotation,
                cameraRotateSpeed * Time.deltaTime);

            await UniTask.Yield(); // 等待下一帧
        }
    }

    private IEnumerator UpdateCameraMoveCoroutine(Transform target)
    {
        while (true)
        {
            CameraController.Instance.MainCamera.transform.position = Vector3.Lerp(
                CameraController.Instance.MainCamera.transform.position, target.position,
                cameraMoveSpeed * Time.deltaTime);
            CameraController.Instance.MainCamera.transform.rotation = Quaternion.Lerp(
                CameraController.Instance.MainCamera.transform.rotation, target.rotation,
                cameraRotateSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void StartUpdateCameraMove()
    {
        cameraMoveCoroutine ??= StartCoroutine(UpdateCameraMoveCoroutine(cameraTf));
    }

    public void EndUpdateCameraMove()
    {
        if (cameraMoveCoroutine == null) return;
        StopCoroutine(cameraMoveCoroutine);
        cameraMoveCoroutine = null;
    }
    
    public void ShowPCUI()
    {
        UIFrame.Show<PCUIPanel>();
    }
    
    public void BackGame()
    {
        if (!isNightEnd)
        {
            // 设置角色位置
            RoleController.Instance.SetTransform(roleTf.position, roleTf.rotation);
            RoleController.Instance.SetStart(true);
            UIFrame.Show<PlayerUIPanel>();
        }else if (isNightEnd)
        {
            // 注册界面加载时执行的操作
            onUILoadingEvent = new EventBinding<OnUILoading>(NewDay);
            EventBus<OnUILoading>.Register(onUILoadingEvent);
            // 显示UI
            var panelData = new LoadPanelData { IsStart = true, IsEnd = false };
            UIFrame.Show<LoadPanel>(panelData);
            isNightEnd = false;
        }
    }

    private void NewDay()
    {
        // 设置角色位置
        RoleController.Instance.SetTransform(newDayTf.position, newDayTf.rotation);
        RoleController.Instance.SetStart(true);
        UIFrame.Show<PlayerUIPanel>();
        EventBus<OnUILoading>.Deregister(onUILoadingEvent);
        
        //关闭加载UI
        EventBus<OnUILoaded>.Raise(new OnUILoaded());
    }
}