using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float scaleMultiplier = 1.2f;
    public float duration = 1;
    private bool btnWait = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnWait = true;
        StartCoroutine(BtnEnter());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, duration);
        btnWait = false;
    }
     
    public IEnumerator BtnEnter()
    {
        if(btnWait)
        {
            while (true)
            {
                if (!btnWait) break;
                transform.DOScale(originalScale * scaleMultiplier, duration);
                yield return new WaitForSeconds(duration);
                if (!btnWait) break;
                transform.DOScale(originalScale, duration);
                yield return new WaitForSeconds(duration);
                if (!btnWait) break;
            }
        }
    }
}