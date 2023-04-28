using Data;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgress()
        {
            _progressService.Progress = _saveLoadService.LoadProgress() ?? CreateNewProgress();
        }

        private PlayerProgress CreateNewProgress()
        {
            return new PlayerProgress("BeginScene");
        }
    }
}