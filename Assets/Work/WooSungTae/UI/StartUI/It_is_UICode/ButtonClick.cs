
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject[] BTN;
    [SerializeField] private GameObject closePanel2;
    [SerializeField] private float up = 90;
    private UIMover mover;
    private StartMusicSFX startMusic; // 쓸 수도 있음


    private void Awake()
    {
        startMusic = GetComponent<StartMusicSFX>();
        mover = GetComponent<UIMover>();
    }
    public void OptionClick()
    {
        if(!mover.startStop)
        {
            mover.SetStartStop(true);
            optionPanel.transform.DOLocalMove(new Vector3(0, up, 0), 1);
            startPanel.transform.DOLocalMove(new Vector3(0, 1300, 0), 1);
        }
    }
    public void OptionExit()
    {
        startPanel.transform.DOLocalMove(new Vector3(0, 0, 0), 1);
        optionPanel.transform.DOLocalMove(new Vector3(0, -1300, 0), 1);
        StartCoroutine(StartStopFalse());
    }

    private IEnumerator StartStopFalse()
    {
        yield return new WaitForSeconds(1f);
        mover.SetStartStop(false);
    }

    public void ExitBTNClick()
    {
        Application.Quit();
    }
    public void StartClick()
    {
        StartCoroutine(StartClickCoroutine());
    }
    IEnumerator StartClickCoroutine()
    {
        if (!mover.startStop)
        {
            mover.SetStartStop(true);
            for(int i = 0; i< BTN.Length; i++)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.Append(BTN[i].transform.DOLocalMoveX(-900, 0.3f));
                sequence.Append(BTN[i].transform.DOLocalMoveX(1500, 0.6f));
                yield return new WaitForSeconds(0.15f);
            }
            yield return new WaitForSeconds(1);

            //SceneManager.LoadScene("DemoScene");
        }
    }
}
