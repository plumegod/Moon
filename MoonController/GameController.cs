using MXZOO;
using MXZOO.Input;
using MXZOO.UIFrame;
using UnityEngine;

public struct GameStartEvent : IEvent
{
}

public struct GameNightEvent : IEvent
{
}

public struct GameDieEvent : IEvent
{
}

public struct GameDayEvent : IEvent
{
}

public struct GameEndEvent : IEvent
{
}


public class GameController : SingletonMono<GameController>
{
    private EventBinding<GameStartEvent> gameStartEvent;
    private EventBinding<GameDieEvent> gameDieEvent;
    private bool isDie;
    
    public bool IsDie
    {
        get => isDie;
        set => isDie = value;
    }

    private void OnEnable()
    {
        gameStartEvent = new EventBinding<GameStartEvent>(StartDay);
        EventBus<GameStartEvent>.Register(gameStartEvent);
        
        gameDieEvent = new EventBinding<GameDieEvent>(OnDie);
        EventBus<GameDieEvent>.Register(gameDieEvent);
    }

    private void OnDisable()
    {
        EventBus<GameStartEvent>.Deregister(gameStartEvent);
        EventBus<GameDieEvent>.Deregister(gameDieEvent);
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

    private void CheckGame()
    {
        if(GameManager.Instance.IsStart) OnEndDay();
        if(!GameManager.Instance.IsStart && !EndDayController.Instance.IsNight)
            OnStartDay();
        
        OnTriggerExit(null);
    }


    private void OnStartDay()
    {
        if(GameManager.Instance.IsStart) return;
        EventBus<GameStartEvent>.Raise(new GameStartEvent());
    }

    private void OnEndDay()
    {
        if(!GameManager.Instance.IsStart) return;
        EventBus<GameNightEvent>.Raise(new GameNightEvent());
    }

    private void StartDay()
    {
        // 游戏天速加一
        GameManager.Instance.GameManagerData.GameDay++;
    }

    private void OnShowUI()
    {
        EventBus<PlayerDownTextShowEvent>.Raise(new PlayerDownTextShowEvent { Text = GetText() });
    }

    private string GetText()
    {
        if (GameManager.Instance.IsStart && !EndDayController.Instance.IsNight)
            return GameManager.Instance.EndText;
        if(!GameManager.Instance.IsStart && !EndDayController.Instance.IsNight) return GameManager.Instance.StartText;
        if(EndDayController.Instance.IsNight) return GameManager.Instance.NightText;
        return "";
    }
    
    private void OnDie()
    {
        UIFrame.Show<DiePanel>();
    }
}