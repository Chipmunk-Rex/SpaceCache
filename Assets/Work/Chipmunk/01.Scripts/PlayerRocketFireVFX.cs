using System;
using Code.Scripts.Entities;
using Code.Scripts.Players;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Work.Chipmunk._01.Scripts
{
    public class PlayerRocketFireVFX : MonoBehaviour, IEntityComponent
    {
        private PlayerMovement _playerMovement;
        [SerializeField] private ParticleSystem rocketParticles;
        [SerializeField] private Light2D rocketLight;
        private float _defaultIntensity;

        public void Initialize(Entity entity)
        {
            _playerMovement = entity.GetCompo<PlayerMovement>();
            _defaultIntensity = rocketLight.intensity;
        }

        private float prev = 0f;

        private void FixedUpdate()
        {
            if (_playerMovement.IsAccelerating == false)
            {
                rocketParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
            else
            {
                rocketParticles.Play(true);
                rocketLight.intensity = _defaultIntensity;
            }
        }
    }
}