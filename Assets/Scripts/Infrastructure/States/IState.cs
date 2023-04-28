namespace Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    
    public interface IExitableState
    {
        void Exit();
    }
    
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payLoad); 
    }
}