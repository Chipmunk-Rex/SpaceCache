using Ami.BroAudio;
using UnityEngine;

namespace Code.Scripts.Items.UI.Sounds
{
    public class ButtonSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundID buttonSound;

        public void PlaySound()
        {
            BroAudio.Play(buttonSound);
        }
        
        
    }
}