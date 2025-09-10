using UnityEngine;

namespace Code.Scripts.Effects
{
    public interface IPlayableEffect
    {
        void PlayEffect(Vector3 position, Quaternion rotation);
        void StopEffect();
    }
}