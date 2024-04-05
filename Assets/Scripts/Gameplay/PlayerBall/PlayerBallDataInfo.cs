namespace BallGame.Gameplay.PlayerBall
{
    public class PlayerBallDataInfo
    {
        public PlayerBallController PlayerBallController { get; private set; }
        public void Setup(PlayerBallController playerBallController)
        {
            PlayerBallController = playerBallController;
        }
    }
}