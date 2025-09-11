using System; 
using Code.Scripts.Players; 
using PSB_Lib.StatSystem; 
using UnityEngine; 
using UnityEngine.InputSystem;

namespace Code.Scripts.Items.UI
{
    public class PlayerStatusUI : MonoBehaviour 
    { 
        [SerializeField] private PlayerStats playerStats; 
        [SerializeField] private StatUIItem statItemPrefab; 
        [SerializeField] private Transform contentRoot;

        private void Start()
        {
            foreach (var stat in playerStats.Stats)
            {
                var item = Instantiate(statItemPrefab, contentRoot); 
                item.Initialize(stat);
            } 
            contentRoot.gameObject.SetActive(false);
        } 
        
        private void Update() 
        { 
            if (Keyboard.current.f1Key.isPressed) 
            { 
                if (!contentRoot.gameObject.activeSelf) 
                    contentRoot.gameObject.SetActive(true); 
            } 
            else 
            { 
                if (contentRoot.gameObject.activeSelf) 
                    contentRoot.gameObject.SetActive(false); 
            } 
        }
        
    }
}