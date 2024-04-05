using BallGame.Initialization;
using UnityEngine;

namespace BallGame.UI
{
    public abstract class BaseScreen : MonoBehaviour, IInitialization
    {
        [SerializeField]
        private string _id;
        public string Id => _id;
        
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
        
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public abstract void Initialization();
    }
}