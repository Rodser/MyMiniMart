using UnityEngine;

namespace Infrastructure
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private static void OnLoaded()
        {
            Debug.Log("ONLoaded");
            GameObject hero = Instantiate("Character/Player");
            Debug.Log(hero.name);

            GameObject hud = Instantiate("Hud/Hud");
            Debug.Log(hud.name);
        }

        public void Exit()
        {
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}