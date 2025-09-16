using System;
using Code.Scripts.Items;
using Code.Scripts.Players;
using DG.Tweening;
using PSB_Lib.Dependencies;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Card : MonoBehaviour, IPointerClickHandler
{
    private LevelUpItemSO _levelUpSO;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _image;

    private RectTransform _rectTransform;
    private Vector2 _defaultPosition;

    private Player _player;

    private Action _clickCallback;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaultPosition = _rectTransform.anchoredPosition;
    }

    public void Initialize(LevelUpItemSO levelUpSO, Player player, Action clickCallback)
    {
        _levelUpSO = levelUpSO;
        _descriptionText.text = levelUpSO.Description;
        _image.sprite = levelUpSO.SkillIcon;
        _player = player;
        _clickCallback = clickCallback;
    }

    public void OnClickCard()
    {
        _levelUpSO.selectCount++;
        _levelUpSO.ApplyItem(_player);
        _clickCallback?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickCard();
    }
}