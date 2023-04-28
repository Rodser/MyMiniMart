using Infrastructure.Services;
using Infrastructure.States;

namespace Infrastructure
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
        }
      
        public GameStateMachine StateMachine { get; set; }
    }
}