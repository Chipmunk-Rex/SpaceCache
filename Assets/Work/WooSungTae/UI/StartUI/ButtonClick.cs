using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private GameObject optionBTN;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private float up = 90;
    private UIMover mover;


    private void Awake()
    {
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
}
