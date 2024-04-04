namespace BallGame.Initialization
{
    public class RegisterServiceCommand<T> : IInitializationCommand where T : class
    {
        private T _service;

        public RegisterServiceCommand(T service)
        {
            _service = service;
        }

        public void Execute()
        {
            ServiceLocator.RegisterService(_service);
        }
    }
}