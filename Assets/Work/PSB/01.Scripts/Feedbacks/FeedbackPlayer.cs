using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Feedbacks
{
    public class FeedbackPlayer : MonoBehaviour
    {
        private List<Feedback> _feedbackToPlay = null;

        private void Awake()
        {
            _feedbackToPlay = new List<Feedback>();
            GetComponents<Feedback>(_feedbackToPlay);
        }

        public void PlayFeedback()
        {
            FinishFeedback();
            foreach(Feedback f in _feedbackToPlay)
            {
                f.CreateFeedback();
            }
        }

        public void FinishFeedback()
        {
            foreach(Feedback f in _feedbackToPlay)
            {
                f.CompletePrevFeedback();
            }
        }

    }
}