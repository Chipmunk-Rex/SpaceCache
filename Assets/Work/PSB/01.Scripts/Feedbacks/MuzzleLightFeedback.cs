using System.Collections;
using UnityEngine;

namespace Code.Scripts.Feedbacks
{
    public class MuzzleLightFeedback : Feedback
    {
        [SerializeField] private GameObject[] _muzzleFlash;
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
            for (int i = 0; i < _muzzleFlash.Length; i++)
            {
                _muzzleFlash[i].SetActive(true);
            }
            yield return new WaitForSeconds(_turnOnTime);
            for (int i = 0; i < _muzzleFlash.Length; i++)
            {
                _muzzleFlash[i].SetActive(false);
            }
        }

        public override void CompletePrevFeedback()
        {
            StopAllCoroutines();
            for (int i = 0; i < _muzzleFlash.Length; i++)
            {
                _muzzleFlash[i].SetActive(_defaultState);
            }
        }

        
    }
}