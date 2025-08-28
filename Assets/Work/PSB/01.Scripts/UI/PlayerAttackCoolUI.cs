using System.Collections;
using Code.Scripts.Entities;
using Code.Scripts.Players.States;
using UnityEngine;

namespace Code.Scripts.Items.UI
{
    public class PlayerAttackCoolUI : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private RectTransform cooldownBar;

        private PlayerAttackCompo _attackCompo;
        private Coroutine _cooldownRoutine;

        public void Initialize(Entity entity)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();

            _attackCompo.OnAttackCooldownStart += StartCooldown;
            _attackCompo.OnAttackCooldownEnd += EndCooldown;
        }

        private void OnDestroy()
        {
            if (_attackCompo != null)
            {
                _attackCompo.OnAttackCooldownStart -= StartCooldown;
                _attackCompo.OnAttackCooldownEnd -= EndCooldown;
            }
        }

        private void StartCooldown(float duration)
        {
            if (_cooldownRoutine != null)
                StopCoroutine(_cooldownRoutine);

            _cooldownRoutine = StartCoroutine(CooldownRoutine(duration));
        }

        private void EndCooldown()
        {
            if (_cooldownRoutine != null)
                StopCoroutine(_cooldownRoutine);

            SetYScale(cooldownBar, 1f);
        }

        private IEnumerator CooldownRoutine(float duration)
        {
            float startTime = Time.time;
            while (Time.time < startTime + duration)
            {
                float elapsed = Time.time - startTime;
                float progress = Mathf.Clamp01(elapsed / duration);

                SetYScale(cooldownBar, progress);

                yield return null;
            }
            
            SetYScale(cooldownBar, 1f);

            _cooldownRoutine = null;
        }

        private void SetYScale(RectTransform rect, float xValue)
        {
            if (rect == null) return;

            Vector3 scale = rect.localScale;
            scale.y = xValue;
            rect.localScale = scale;
        }
    }
}
