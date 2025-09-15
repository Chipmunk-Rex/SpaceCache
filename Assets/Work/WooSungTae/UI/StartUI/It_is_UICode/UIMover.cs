using System.Collections;
using Ami.BroAudio;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject cachePanel;
    [SerializeField] private MousePos mousePos;
    public bool startStop { get; private set; } = false;
    
    [SerializeField] private SoundID slideSound;
    private bool _isCachePanelVisible;
    
    [SerializeField] private TextMeshProUGUI titleText;
    
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
            if (!_isCachePanelVisible)
            {
                EnterCachePanel();
                EnterStartPanel();
                _isCachePanelVisible = true;
            }
        }
        else
        {
            if (_isCachePanelVisible)
            {
                ExitCachePanel();
                ExitStartPanel();
                _isCachePanelVisible = false;
            }
        }
    }
    
    public void SetStartStop(bool startStop)
    {
        this.startStop = startStop;
    }

    private void StartPanelMove()
    {
        //startPanel.transform.DOLocalMove(new Vector3(0, -90, 0), 1);
        RectTransform rect = startPanel.GetComponent<RectTransform>();
        rect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.OutCubic);
    }

    private void EnterCachePanel()
    {
        BroAudio.Play(slideSound);
        cachePanel.transform.DOLocalMove(new Vector3(-30, 0, 0), 1);
        
        if (titleText != null) titleText.enabled = false;
    }
    
    private void ExitCachePanel()
    {
        BroAudio.Play(slideSound);
        cachePanel.transform.DOLocalMove(new Vector3(2000, 0, 0), 1);
        
        if (titleText != null) titleText.enabled = true;
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
