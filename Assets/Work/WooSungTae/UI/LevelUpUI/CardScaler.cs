using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject cardDescription;
    [SerializeField] private GameObject cardImage;


    public void OnPointerEnter(PointerEventData eventData)
    {
        CardDescriptionActive();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CardImageActive();
    }

    public void CardDescriptionActive()
    {
        cardDescription.SetActive(true);
        cardImage.SetActive(false);
        transform.DOScale(new Vector2(1.2f, 1.2f), 0.5f);
    }
    public void CardImageActive()
    {
        cardDescription.SetActive(false);
        cardImage.SetActive(true);
        transform.DOScale(new Vector2(1f, 1f), 0.5f);
    }
}
