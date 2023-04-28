using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain);
        }
        
        public static IInputService InputService { get; set; }
        public GameStateMachine StateMachine { get; set; }
    }
}