using System.Collections.Generic;
using BallGame.Initialization;
using UnityEngine;

namespace BallGame.UI
{
    public class ScreenUiController : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private BaseScreen[] _screens;
        private Dictionary<string, BaseScreen> _screensDictionary = new();
        
        public void Initialization()
        {
            foreach (var screen in _screens)
            {
                _screensDictionary.Add(screen.Id, screen);
            }
        }

        public BaseScreen ShowScreenById(string id)
        {
            if (_screensDictionary.TryGetValue(id, out var screen))
            {
                screen.Show();
                return screen;
            }

            return null;
        }
        
        public void HideScreenById(string id)
        {
            if (_screensDictionary.TryGetValue(id, out var screen))
            {
                screen.Hide();
            }
        }
    }
}