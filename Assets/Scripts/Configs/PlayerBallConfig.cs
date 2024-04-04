using UnityEngine;

namespace BallGame.Configs
{
    [CreateAssetMenu(fileName = "PlayerBall", menuName = "Create PlayerBall", order = 0)]
    public class PlayerBallConfig : ScriptableObject
    {
        [SerializeField]
        private GameObject _playerBallPrefab;
        public GameObject PlayerBallPrefab => _playerBallPrefab;
    }
}