using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMix : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SFXSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
        }
    }

    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

        SetBGMVolume();
        SetSFXVolume(); 
    }
}
