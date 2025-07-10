using System.Collections;
using DG.Tweening;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject cachePanel;
    [SerializeField] private MousePos mousePos;
    [SerializeField] private GameObject mainCamera;

    public bool startStop { get; private set; } = false;
    private void Start()
    {
        StartPanelMove();
        StartCoroutine(StartStop());
    }
    private void Update()
    {
        if(!startStop)
        {
            if (mousePos.OnRangeEnter())
            {
                EnterCachePanel();
                EnterStartPanel();
            }
            else
            {
                ExitCachePanel();
                ExitStartPanel();
            }
        }
    }
    public void SetStartStop(bool startStop)
    {
        this.startStop = startStop;
    }

    private void StartPanelMove()
    {
        startPanel.transform.DOLocalMove(new Vector3(0, -90, 0), 3);
    }

    private void EnterCachePanel()
    {
        cachePanel.transform.DOLocalMove(new Vector3(-30, 0, 0), 1);
    }
    private void ExitCachePanel()
    {
        cachePanel.transform.DOLocalMove(new Vector3(2000, 0, 0), 1);
    }

    public void EnterStartPanel()
    {
        startPanel.transform.DOLocalMove(new Vector3(-1000, 0, 0), 1);
    }
    private void ExitStartPanel()
    {
        startPanel.transform.DOLocalMove(new Vector3(0, 0, 0), 1);
    }

    private IEnumerator StartStop()
    {
        startStop = true;
        yield return new WaitForSeconds(3f);
        startStop = false;
    }
}
