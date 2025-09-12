using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OutPanel : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _outPanel;
    [SerializeField] private GameObject _greyPanel;
    [SerializeField] private GameObject mainBTN;

    [Header("BTN, _soundPanel")]
    [SerializeField] private GameObject _soundPanel;

    [Header("Rects")]
    [SerializeField] private RectTransform volume_rectTransform;
    [SerializeField] private RectTransform outPanelEsc_rectTransform;

    [Header("Move Offsets (anchored X)")]
    [SerializeField] private float outPanelEsc_moveXPos;
    [SerializeField] private float volume_moveXPos;

    private float _volumeBaseX;
    private float _escBaseX;
    private bool _isClicked = false;
    private bool _soundBTNClicked = false;

    private void Awake()
    {
        _volumeBaseX = volume_rectTransform.anchoredPosition.x;
        _escBaseX = outPanelEsc_rectTransform.anchoredPosition.x;
    }
    
    private void Update()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if(!_isClicked)
            {
                Time.timeScale = 0;
                _outPanel.SetActive(true);
                _greyPanel.SetActive(true);
                _isClicked = true;
            }
            else
            {
                Time.timeScale = 1;
                volume_rectTransform.DOAnchorPosX(_volumeBaseX, 0.3f).SetUpdate(true);
                outPanelEsc_rectTransform.DOAnchorPosX(_escBaseX, 0.3f).SetUpdate(true);
                _outPanel.SetActive(false);
                _greyPanel.SetActive(false);
                _soundPanel.SetActive(false);
                _isClicked = false;
            }
        }
    }

    public void MainBtnClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void MusicBtnClick()
    {
        if (!_soundBTNClicked)
        {
            volume_rectTransform.DOAnchorPosX(volume_moveXPos, 0.3f).SetUpdate(true);
            outPanelEsc_rectTransform.DOAnchorPosX(outPanelEsc_moveXPos, 0.3f).SetUpdate(true);
            _soundBTNClicked = true;
            _soundPanel.SetActive(true);
        }
        else
        {
            volume_rectTransform.DOAnchorPosX(_volumeBaseX, 0.3f).SetUpdate(true);
            outPanelEsc_rectTransform.DOAnchorPosX(_escBaseX, 0.3f).SetUpdate(true);
            _soundPanel.SetActive(false);
            _soundBTNClicked = false;
        }
    }
}
