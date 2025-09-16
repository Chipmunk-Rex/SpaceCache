// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// public class DragCard : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
// {
//     private RectTransform rt;
//     private Vector3 startPos;
//     private Card card;
//     private CardManager cardManager;
//
//     private void Awake()
//     {
//         cardManager = GetComponentInParent<CardManager>();
//         card = GetComponent<Card>();
//         rt = (RectTransform)transform;
//     }
//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         startPos = rt.anchoredPosition;
//         cardManager.SetRaycastsAll(false);
//         cg.blocksRaycasts = false;
//     }
// }
