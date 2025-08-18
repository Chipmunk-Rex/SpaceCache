using UnityEngine;

public class StartMusicBGM : MonoBehaviour
{
    [SerializeField] private AudioClip startBGM;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = startBGM;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
