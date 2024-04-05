namespace BallGame.StateMachine
{
    public class StateMachine : IStateMachine
    {
        private IState _currentState;

        public void ChangeState(IState newState)
        {
            _currentState?.OnExit();
            _currentState = newState;
            _currentState.OnEnter();
        }

        public void Update()
        {
            _currentState?.Tick();
        }
    }
}