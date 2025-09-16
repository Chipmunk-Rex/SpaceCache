using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Items;
using Code.Scripts.Players;
using DG.Tweening;
using JetBrains.Annotations;
using PSB_Lib.Dependencies;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private List<LevelUpItemSO> levelUpItems;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private Card CardPrefab;

    [Inject] private Player _player;
    [Inject] private PlayerLevelSystem _playerLevelSystem;
    public UnityEvent skillClick;

    [SerializeField] private float yOffset = 1000f;
    private int cardCount = 3;

    private RectTransform rectTransform;
    private Vector2 _defaultPosition;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _defaultPosition = rectTransform.anchoredPosition;
        _playerLevelSystem.OnLevelUp += PlayerLevelUpHandler;
        rectTransform.anchoredPosition = _defaultPosition + Vector2.up * yOffset;
    }

    private void OnDestroy()
    {
        _playerLevelSystem.OnLevelUp -= PlayerLevelUpHandler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            PlayerLevelUpHandler();
    }

    private void PlayerLevelUpHandler() => DrawCards();

    private void DrawCards()
    {
        SetPaused(true);
        rectTransform.DOAnchorPos(_defaultPosition, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
        
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }

        var availableItems = levelUpItems.Where(item => item.selectCount < item.maxCount).ToList();
        var selectedItems = new List<LevelUpItemSO>();
        int count = Mathf.Min(cardCount, availableItems.Count);
        for (int i = 0; i < count; i++)
        {
            int idx = Random.Range(0, availableItems.Count);
            selectedItems.Add(availableItems[idx]);
            availableItems.RemoveAt(idx);
        }

        foreach (var item in selectedItems)
        {
            var card = Instantiate(CardPrefab, cardContainer);
            card.Initialize(item, _player, OnCardSelected);
        }
    }

    private void OnCardSelected()
    {
        SetPaused(false);
        skillClick?.Invoke();
        rectTransform.DOAnchorPos(_defaultPosition + Vector2.up * yOffset, 0.5f).SetEase(Ease.InBack);
    }

    #region Request / Wrappers

    private void SetPaused(bool paused)
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    #endregion
}