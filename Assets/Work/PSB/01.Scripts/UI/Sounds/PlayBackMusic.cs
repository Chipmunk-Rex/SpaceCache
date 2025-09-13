using Ami.BroAudio;
using UnityEngine;

namespace Code.Scripts.Items.UI.Sounds
{
    public class PlayBackMusic : MonoBehaviour
    {
        [SerializeField] private SoundID soundID;

        private void Start()
        {
            BroAudio.Stop(BroAudioType.Music);
            
            BroAudio.Play(soundID);
        }
        
    }
}