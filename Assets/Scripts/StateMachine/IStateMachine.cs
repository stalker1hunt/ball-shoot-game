namespace BallGame.StateMachine
{
    public interface IStateMachine
    {
        void ChangeState(IState newState);
        void Update();
    }
}