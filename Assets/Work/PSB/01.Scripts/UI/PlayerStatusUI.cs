using UnityEngine; 
using UnityEngine.InputSystem;

namespace Code.Scripts.Items.UI
{
    public class PlayerStatusUI : MonoBehaviour 
    { 
        [SerializeField] private CanvasGroup contentRoot;

        private void Start()
        {
            contentRoot.alpha = 0;
        } 
        
        private void Update() 
        { 
            if (Keyboard.current.f1Key.isPressed)
            {
                if (Mathf.Approximately(contentRoot.alpha, 0))
                    contentRoot.alpha = 1;
            } 
            else
            {
                if (Mathf.Approximately(contentRoot.alpha, 1))
                    contentRoot.alpha = 0;
            } 
        }
        
    }
}