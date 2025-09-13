using System.Collections;
using Ami.BroAudio;
using DG.Tweening;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject cachePanel;
    [SerializeField] private MousePos mousePos;
    public bool startStop { get; private set; } = false;
    
    [SerializeField] private SoundID slideSound;
    private bool isCachePanelVisible;
    
    private void Start()
    {
        StartPanelMove();
        StartCoroutine(StartStop());
    }
    
    private void Update()
    {
        if (startStop) return;

        if (mousePos.OnRangeEnter())
        {
            if (!isCachePanelVisible)
            {
                EnterCachePanel();
                EnterStartPanel();
                isCachePanelVisible = true;
            }
        }
        else
        {
            if (isCachePanelVisible)
            {
                ExitCachePanel();
                ExitStartPanel();
                isCachePanelVisible = false;
            }
        }
    }
    
    public void SetStartStop(bool startStop)
    {
        this.startStop = startStop;
    }

    private void StartPanelMove()
    {
        startPanel.transform.DOLocalMove(new Vector3(0, -90, 0), 1);
    }

    private void EnterCachePanel()
    {
        BroAudio.Play(slideSound);
        cachePanel.transform.DOLocalMove(new Vector3(-30, 0, 0), 1);
    }
    
    private void ExitCachePanel()
    {
        BroAudio.Play(slideSound);
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
        yield return new WaitForSeconds(1f);
        startStop = false;
    }

  
}
