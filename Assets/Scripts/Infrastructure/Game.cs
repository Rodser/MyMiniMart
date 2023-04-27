using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public Game()
        {
            StateMachine = new GameStateMachine();
        }
        
        public static IInputService InputService { get; set; }
        public GameStateMachine StateMachine { get; set; }
    }
}