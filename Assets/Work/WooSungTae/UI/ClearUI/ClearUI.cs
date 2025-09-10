using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ClearUI : MonoBehaviour
{
    [Header("first paenl")]
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private Sprite winClear;
    [SerializeField] private Sprite loseClear;
    [SerializeField] private TextMeshProUGUI clearText;
    private RectTransform panelRect;
    [SerializeField] private float clearImageDOFadeSpeed;
    [SerializeField] private float clearPanelUp;
    [SerializeField] private float endXSize;

    [Header("second paenl")]
    [SerializeField] private GameObject clearPanel2;
    [SerializeField] private float clearImageDOFadeSpeed2;
    [SerializeField] private float endXSize2;
    [SerializeField] private float endYSize;
    [SerializeField] private float timeDelay = 1;

    [Header("buttons")]
    [SerializeField] private GameObject retryBTN;
    [SerializeField] private GameObject mainBTN;
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI timeTex;

    [SerializeField] private float timer = 0;
    [SerializeField] float duration = 5f;
    float elapsed = 0f;
    private void Awake()
    {
        panelRect = clearPanel.GetComponent<RectTransform>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartCoroutine(Win());
        }
        if(Keyboard.current.rKey.wasPressedThisFrame)
        {
            StartCoroutine(Lose());
        }
    }

    public void WinUIOpen()
    {
        StartCoroutine(Win());
    }

    public void LoseUIOpen()
    {
        StartCoroutine(Lose());
    }
    
    #region win
    IEnumerator Win()
    {
        Time.timeScale = 0;
        clearText.color = Color.green;
        StartCoroutine(ClearPanel(winClear, "Clear!"));

        clearPanel2.SetActive(true);
        panelRect = clearPanel2.GetComponent<RectTransform>();
        Image clearImage2 = clearPanel2.GetComponent<Image>();
        clearImage2.DOFade(1, clearImageDOFadeSpeed2).SetUpdate(true);
        float size = 0;
        Sequence seq = DOTween.Sequence();
        seq.Append(panelRect.DOSizeDelta(new Vector2(endXSize2, panelRect.sizeDelta.y), 0.7f)).SetUpdate(true);
        seq.Append(panelRect.DOSizeDelta(new Vector2(endXSize2, endYSize), 0.4f)).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1.6f);
        StartCoroutine(TimeCalculate("�� �ð�"));

        retryBTN.SetActive(true);
        mainBTN.SetActive(true);
    }
    #endregion

    #region lose
    IEnumerator Lose()
    {
        Time.timeScale = 0;
        clearText.color = Color.red;
        StartCoroutine(ClearPanel(loseClear, "Lose..."));

        clearPanel2.SetActive(true);
        panelRect = clearPanel2.GetComponent<RectTransform>();
        Image clearImage2 = clearPanel2.GetComponent<Image>();
        clearImage2.DOFade(1, clearImageDOFadeSpeed2).SetUpdate(true);
        float size = 0;
        Sequence seq = DOTween.Sequence();
        seq.Append(panelRect.DOSizeDelta(new Vector2(endXSize2, panelRect.sizeDelta.y), 0.7f)).SetUpdate(true);
        seq.Append(panelRect.DOSizeDelta(new Vector2(endXSize2, endYSize), 0.4f)).SetUpdate(true);
        yield return new WaitForSecondsRealtime(1.6f);
        StartCoroutine(TimeCalculate("������ �ð�"));
        retryBTN.SetActive(true);
        mainBTN.SetActive(true);
    }
    #endregion
    IEnumerator TimeCalculate(string clear_tex)
    {
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float showTime = Mathf.Lerp(0, timer, t);

            int minute = Mathf.FloorToInt(showTime / 60f);
            int second = Mathf.FloorToInt(showTime % 60f);

            if (showTime < 60)
                timeTex.text = $"{clear_tex}: {second}��";
            else
                timeTex.text = $"{clear_tex}: {minute}�� {second}��";

            yield return null;
        }
    }
    IEnumerator ClearPanel(Sprite sprite, string clear_tex)
    {
        clearPanel.SetActive(true);
        Image clearImage = clearPanel.GetComponent<Image>();
        clearImage.sprite = sprite;
        clearImage.DOFade(1, clearImageDOFadeSpeed).SetUpdate(true);
        panelRect.DOAnchorPosY(clearPanelUp, 0.9f).SetUpdate(true);
        panelRect.DOSizeDelta(new Vector2(endXSize, panelRect.sizeDelta.y), 1).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.4f);
        clearText.text = clear_tex;
        yield return new WaitForSecondsRealtime(0.4f);
    }
}
