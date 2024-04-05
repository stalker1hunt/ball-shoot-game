namespace BallGame.Initialization
{
    public class InitializationGameCommand<T> : IInitializationCommand where T : IInitialization
    {
        private T _controller;

        public InitializationGameCommand(T controller)
        {
            _controller = controller;
        }
        
        public void Execute()
        {
            _controller.Initialization();
        }
    }
}
