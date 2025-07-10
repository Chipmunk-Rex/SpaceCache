using UnityEngine;

public class StartMusic : MonoBehaviour
{
    [SerializeField] private MousePos mousePos;
    [SerializeField] private AudioClip btnClick;
    [SerializeField] private AudioClip optionExit;
    [SerializeField] private AudioClip pageChange;
    private AudioSource audioSource;
    private UIMover mover;
    private bool oneTime = false;


    private void Awake()
    {
        mover = GetComponent<UIMover>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }
    private void Update()
    {
        PageChange();
    }

    public void BTNClick()
    {
        audioSource.clip = btnClick;
        audioSource.time = 0.1f;
        audioSource.Play();
    }
    public void OptionExitBTNClick()
    {
        audioSource.clip = optionExit;
        audioSource.time = 0.1f;
        audioSource.Play();
    }

    public void PageChange()
    {
        if(mousePos.OnRangeEnter() && !oneTime && !mover.startStop)
        {
            oneTime = true;
            Debug.Log("½ÇÇàµÊ");
            audioSource.clip = pageChange;
            audioSource.time = 0.1f;
            audioSource.Play();
        } 
        else if(!mousePos.OnRangeEnter() && oneTime)
        {
            audioSource.Play();
            audioSource.clip = pageChange;
            audioSource.time = 0.1f;
            oneTime = false;
        }
       
    }
}
