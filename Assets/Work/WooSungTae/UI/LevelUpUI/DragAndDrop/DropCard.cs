using Ami.BroAudio;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] private Vector3 baseScale;
    private CardManager cardManager;

    [SerializeField] private SoundID skillSelectSound;

    private void Awake()
    {
        cardManager = GetComponentInParent<CardManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Card card = eventData.pointerDrag.GetComponent<Card>();
        card.OnClickCard();
        BroAudio.Play(skillSelectSound);
        // cardManager.StartCardUp();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            transform.localScale = baseScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = baseScale;
    }
}
