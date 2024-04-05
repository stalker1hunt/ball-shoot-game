namespace BallGame
{
    public class GameState
    {
        public bool IsGameplay { get; private set; }
        
        public void StartGame()
        {
            IsGameplay = true;
        }
        
        public void EndGame()
        {
            IsGameplay = false;
        }
    }
}