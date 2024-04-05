namespace BallGame.UI.Screens
{
    public class StartGameScreen : BaseScreen
    {
        private GameState _gameState;
        
        public override void Initialization()
        {
            _gameState = ServiceLocator.GetService<GameState>();
        }
        
        public void OnButtonStartGameClick()
        {
            _gameState.StartGame();
            Hide();
        }
    }
}