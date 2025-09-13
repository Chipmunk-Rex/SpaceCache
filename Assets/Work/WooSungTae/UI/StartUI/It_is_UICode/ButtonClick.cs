
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private Button[] buttons;
    [SerializeField] private float up = 90;
    private UIMover mover;


    private void Awake()
    {
        Time.timeScale = 1;
        mover = GetComponent<UIMover>();

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }
    
    public void OptionClick()
    {
        if(!mover.startStop)
        {
            mover.SetStartStop(true);
            optionPanel.transform.DOLocalMove(new Vector3(0, up, 0), 1).SetUpdate(true);
            startPanel.transform.DOLocalMove(new Vector3(0, 1300, 0), 1).SetUpdate(true);
        }
    }
    
    public void OptionExit()
    {
        startPanel.transform.DOLocalMove(new Vector3(0, 0, 0), 1).SetUpdate(true);
        optionPanel.transform.DOLocalMove(new Vector3(0, -1300, 0), 1).SetUpdate(true);
        StartCoroutine(StartStopFalse());
    }

    private IEnumerator StartStopFalse()
    {
        yield return new WaitForSecondsRealtime(1f);
        mover.SetStartStop(false);
    }

    public void ExitBtnClick()
    {
        if (!mover.startStop)
            StartCoroutine(ExitClickCoroutine());
    }

    private IEnumerator ExitClickCoroutine()
    {
        mover.SetStartStop(true);
        for (int i = 0; i < buttons.Length; i++)
        {
            Sequence sequence = DOTween.Sequence();
            buttons[i].interactable = false;
            sequence.Append(buttons[i].transform.DOLocalMoveX(-900, 0.3f)).SetUpdate(true);
            sequence.Append(buttons[i].transform.DOLocalMoveX(1500, 0.6f)).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.15f);
        }
        yield return new WaitForSecondsRealtime(1);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    
    public void StartClick()
    {
        StartCoroutine(StartClickCoroutine());
    }
    
    private IEnumerator StartClickCoroutine()
    {
        if (!mover.startStop)
        {
            mover.SetStartStop(true);
            for(int i = 0; i< buttons.Length; i++)
            {
                Sequence sequence = DOTween.Sequence();
                buttons[i].interactable = false;
                sequence.Append(buttons[i].transform.DOLocalMoveX(-900, 0.3f)).SetUpdate(true);
                sequence.Append(buttons[i].transform.DOLocalMoveX(1500, 0.6f)).SetUpdate(true);
                yield return new WaitForSecondsRealtime(0.15f);
            }
            yield return new WaitForSecondsRealtime(0.6f);

            SceneManager.LoadScene("GameScene");
        }
    }
    
}
