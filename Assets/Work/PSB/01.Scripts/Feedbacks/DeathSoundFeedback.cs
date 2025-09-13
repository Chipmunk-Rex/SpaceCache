using Ami.BroAudio;
using UnityEngine;

namespace Code.Scripts.Feedbacks
{
    public class DeathSoundFeedback : Feedback
    {
        [SerializeField] private SoundID deathSound;
        [SerializeField] private bool isEndGame = false;
        
        private bool hasPlayed;
        
        public override void CreateFeedback()
        {
            if (isEndGame)
            {
                BroAudio.Stop(BroAudioType.All);
                BroAudio.Play(deathSound);
            }
            else
            {
                if (!hasPlayed)
                {
                    BroAudio.Play(deathSound);
                    hasPlayed = true;
                }
            }
        }

        public override void StopFeedback()
        {
            BroAudio.Stop(BroAudioType.SFX);
        }

        public override void CompletePrevFeedback()
        {
            hasPlayed = false;
        }
        
        
    }
}