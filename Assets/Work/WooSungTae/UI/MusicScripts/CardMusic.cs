using UnityEngine;
using UnityEngine.Audio;

public class CardMusic : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _cardSlide;
    [SerializeField] private AudioMixerGroup sfxGroup;

    private void Awake()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = sfxGroup;
        _audioSource.loop = false;
        _audioSource.playOnAwake = false;
    }

    public void SlideCard()
    {
        _audioSource.clip = _cardSlide;
        _audioSource.Play();
    }
}
