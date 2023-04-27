using Services.Input;

namespace Infrastructure
{
    public class Game
    {
        public Game()
        {
            InputService = new MobileInputService();
        }

        public static IInputService InputService;
    }
}