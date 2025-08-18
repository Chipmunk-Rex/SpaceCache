using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject cardDescription;
    [SerializeField] private GameObject cardImage;
    public void OnPointerEnter(PointerEventData eventData)
    {
        cardDescription.SetActive(true);
        cardImage.SetActive(false);
        transform.DOScale(new Vector2(1.1f,1.1f), 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardDescription.SetActive(false);
        cardImage.SetActive(true);
        transform.DOScale(new Vector2(1f, 1f), 0.5f);
    }
}
