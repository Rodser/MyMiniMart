using Infrastructure.States;
using Logic;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain _curtain;
        
        private Game _game;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _game = new Game(this, Instantiate(_curtain));
            _game.StateMachine.Enter<BootstrapState>();
        }
    }
}