using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MXZOO.MoonController
{
    public class OxygenController : SingletonMono<OxygenController>
    {
        private EventBinding<GameDieEvent> gameDieEvent;
        private EventBinding<GameNightEvent> gameEndEvent;
        private EventBinding<GameStartEvent> gameStartEvent;
        private bool isStart;
        private float oxygenRecoverSpeed;
        public float NowOxygenDuraction { get; private set; }

        private void OnEnable()
        {
            gameStartEvent = new EventBinding<GameStartEvent>(OnStartUseOxygen);
            gameEndEvent = new EventBinding<GameNightEvent>(OnDisableUseOxygen);
            gameDieEvent = new EventBinding<GameDieEvent>(OnDisableUseOxygen);
            EventBus<GameStartEvent>.Register(gameStartEvent);
            EventBus<GameNightEvent>.Register(gameEndEvent);
            EventBus<GameDieEvent>.Register(gameDieEvent);
        }

        private void OnDisable()
        {
            EventBus<GameStartEvent>.Deregister(gameStartEvent);
            EventBus<GameNightEvent>.Deregister(gameEndEvent);
            EventBus<GameDieEvent>.Deregister(gameDieEvent);
        }

        private float ConsumeOxygen(float value)
        {
            //todo:消耗氧气算法
            var oxygen = value - 1 * oxygenRecoverSpeed;
            return oxygen;
        }

        private async UniTask OnUseOxygen()
        {
            while (isStart)
            {
                await UniTask.Delay(1000);
                var oxygen = ConsumeOxygen(NowOxygenDuraction);
                NowOxygenDuraction = oxygen;
                EventBus<PlayerLeftUpOxygenEvent>.Raise(new PlayerLeftUpOxygenEvent()
                {
                    Progress = NowOxygenDuraction / GameManager.Instance.GameManagerData.OxygenDuraction
                });

                if (NowOxygenDuraction <= 0) EventBus<GameDieEvent>.Raise(new GameDieEvent());
            }
        }

        private void OnStartUseOxygen()
        {
            NowOxygenDuraction = GameManager.Instance.GameManagerData.OxygenDuraction;
            isStart = true;
            oxygenRecoverSpeed = GameManager.Instance.GameManagerData.OxygenRecoverSpeed;
            OnUseOxygen().Forget();
        }

        private void OnDisableUseOxygen()
        {
            isStart = false;
        }
    }
}