using Ami.BroAudio;
using UnityEngine;

namespace Code.Scripts.Feedbacks
{
    public class AttackSoundFeedback : Feedback
    {
        [SerializeField] private SoundID attackSound;
        [SerializeField] private float playInterval = 1f;
        
        private float lastPlayTime;

        public override void CreateFeedback()
        {
            if (Time.time - lastPlayTime >= playInterval)
            {
                BroAudio.Play(attackSound);
                lastPlayTime = Time.time;
            }
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