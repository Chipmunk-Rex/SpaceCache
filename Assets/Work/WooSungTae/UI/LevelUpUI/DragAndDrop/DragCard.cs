using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rt;
    private Canvas canvas;
    private CanvasGroup cg;
    private Vector3 startPos;
    private Card card;
    private CardManager cardManager;

    private void Awake()
    {
        cardManager = GetComponentInParent<CardManager>();
        card = GetComponent<Card>();
        cg = GetComponent<CanvasGroup>();
        rt = (RectTransform)transform;
        canvas = GetComponentInParent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = rt.anchoredPosition;
        cardManager.SetRaycastsAll(false);
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rt.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        #region sorry i used gpt.. becauae i don't solve bug
        // �� ������Ƽ��: ���� ����ĳ��Ʈ�� DropCard ã��
        var gr = canvas.GetComponent<GraphicRaycaster>();
        if (gr)
        {
            var results = new List<RaycastResult>();
            gr.Raycast(eventData, results);
            var drop = results.Select(r => r.gameObject.GetComponentInParent<DropCard>())
                              .FirstOrDefault(d => d != null);
            if (drop)
            {
                // OnDrop�� ����� ������ ȣ��
                ExecuteEvents.Execute<IDropHandler>(drop.gameObject, eventData, ExecuteEvents.dropHandler);
            }
            else
            {
                rt.anchoredPosition = startPos;
            }
        }
        #endregion
        cardManager.SetRaycastsAll(true);
        cg.blocksRaycasts = true;
    }
}
