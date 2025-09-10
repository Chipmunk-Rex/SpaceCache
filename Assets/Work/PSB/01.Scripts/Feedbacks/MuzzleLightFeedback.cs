using System.Collections;
using UnityEngine;

namespace Code.Scripts.Feedbacks
{
    public class MuzzleLightFeedback : Feedback
    {
        [SerializeField] private GameObject _muzzleFlash1;
        [SerializeField] private GameObject _muzzleFlash2;
        [SerializeField] private float _turnOnTime = 0.08f;
        [SerializeField] private bool _defaultState = false;
        
        
        public override void CreateFeedback()
        {
            StartCoroutine(ActiveCoroutine());
        }

        public override void StopFeedback()
        {
            
        }

        private IEnumerator ActiveCoroutine()
        {
            _muzzleFlash1.SetActive(true);
            _muzzleFlash2.SetActive(true);
            yield return new WaitForSeconds(_turnOnTime);
            _muzzleFlash1.SetActive(false);
            _muzzleFlash2.SetActive(false);
        }

        public override void CompletePrevFeedback()
        {
            StopAllCoroutines();
            _muzzleFlash1.SetActive(_defaultState);
            _muzzleFlash2.SetActive(_defaultState);
        }

        
    }
}