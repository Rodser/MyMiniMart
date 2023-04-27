using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
        
        public static IInputService InputService { get; set; }
        public GameStateMachine StateMachine { get; set; }
    }
}