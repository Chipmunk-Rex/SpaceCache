
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject optionBTN;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject startBTN;
    [SerializeField] private GameObject startBTNParticle;
    private Rigidbody2D rigid;
    private Rigidbody2D rigid2;
    [SerializeField] private float up = 90;
    private UIMover mover;
    private StartMusicSFX startMusic;


    private void Awake()
    {
        startMusic = GetComponent<StartMusicSFX>();
        rigid = startBTN.GetComponent<Rigidbody2D>();
        rigid2 = startBTNParticle.GetComponent<Rigidbody2D>();
        mover = GetComponent<UIMover>();
    }
    public void OptionClick()
    {
        if(!mover.startStop)
        {
            mover.SetStartStop(true);
            optionBTN.transform.DOLocalMove(new Vector3(0, up, 0), 1);
            startPanel.transform.DOLocalMove(new Vector3(0, 1300, 0), 1);
        }
    }
    public void OptionExit()
    {
        startPanel.transform.DOLocalMove(new Vector3(0, 0, 0), 1);
        optionBTN.transform.DOLocalMove(new Vector3(0, -1300, 0), 1);
        StartCoroutine(StartStopFalse());
    }

    private IEnumerator StartStopFalse()
    {
        yield return new WaitForSeconds(1);
        mover.SetStartStop(false);
    }

    public void ExitBTNClick()
    {
        Application.Quit();
    }
    public void StartClick()
    {
        StartCoroutine(StartClickCoroutine());
        startMusic.RocketLaunch();
    }
    IEnumerator StartClickCoroutine()
    {
        if (!mover.startStop)
        {
            mover.SetStartStop(true);
            startBTN.transform.DOLocalMove(new Vector2(-900, 0), 1);
            yield return new WaitForSeconds(0.5f);
            rigid.linearVelocity = new Vector2(1600, 0);
            yield return new WaitForSeconds(0.65f);
            startBTNParticle.SetActive(true);
            rigid2.linearVelocity = new Vector2(14.5f, 0);
        }
    }
}
