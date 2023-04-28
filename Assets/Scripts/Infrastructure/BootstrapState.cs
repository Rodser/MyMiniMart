using Services.Input;

namespace Infrastructure
{
    public class BootstrapState : IState
    {            
        private const string Initial = "Initial";
        private const string Main = "BeginScene";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(Main);
        }

        private void RegisterServices()
        {
            Game.InputService = new MobileInputService();
        }

        public void Exit()
        {
            
        }
    }
}