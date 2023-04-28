using Infrastructure.Factories;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            Debug.Log($"{sceneName}");
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }

        private void OnLoaded()
        {
            var zone = Object.FindObjectOfType<Zone>();

            GameObject hero = _gameFactory.CreateHero(zone.PointPlayer.gameObject);
            _gameFactory.CreateHud();
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}