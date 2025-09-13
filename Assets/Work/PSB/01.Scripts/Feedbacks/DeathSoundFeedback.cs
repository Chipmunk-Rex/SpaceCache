using Ami.BroAudio;
using UnityEngine;

namespace Code.Scripts.Feedbacks
{
    public class DeathSoundFeedback : Feedback
    {
        [SerializeField] private SoundID deathSound;
        
        public override void CreateFeedback()
        {
            BroAudio.Stop(BroAudioType.All);
            BroAudio.Play(deathSound);
        }

        public override void StopFeedback()
        {
            BroAudio.Stop(BroAudioType.SFX);
        }

        public override void CompletePrevFeedback()
        {
            
        }
        
        
    }
}