using TMPro;
using UnityEngine;

namespace BallGame.UI.Screens
{
    public class EndGameScreen : BaseScreen
    {
        [SerializeField]
        private TMP_Text _endGameText;
        
        public void Setup(bool isWin)
        {
            _endGameText.text = isWin ? "You win!" : "You lose!";
        }
        
        public void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}