using Code.Scripts.Effects;
using Code.Scripts.Entities;
using PSB_Lib.Dependencies;
using PSB_Lib.ObjectPool.RunTime;
using UnityEngine;

namespace Code.Scripts.Feedbacks
{
    public class HitImpactFeedback : Feedback
    {
        [SerializeField] private PoolItemSO hitImpact;
        [SerializeField] private float playDuration = 0.5f;
        [SerializeField] private EntityActionData actionData;

        [Inject] private PoolManagerMono _poolManager;

        public override async void CreateFeedback()
        {
            BombEffectPool _effect = _poolManager.Pop<BombEffectPool>(hitImpact);

            Vector3 targetPosition = actionData.Entity.transform.position;
            Quaternion rotation = Quaternion.LookRotation(actionData.HitNormal * -1);

            _effect.PlayEffect(targetPosition, rotation);

            await Awaitable.WaitForSecondsAsync(playDuration);
            _poolManager.Push(_effect);
        }

        public override void StopFeedback()
        {

        }

        public override void CompletePrevFeedback()
        {
            
        }
    }
}