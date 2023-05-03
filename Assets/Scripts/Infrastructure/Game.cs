using Infrastructure.Services;
using Infrastructure.States;
using Logic;

namespace Infrastructure
{
    public class Game
    {
        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            AllServices allServices = new AllServices();
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, allServices.Container);
        }
      
        public GameStateMachine StateMachine { get; }
    }
}