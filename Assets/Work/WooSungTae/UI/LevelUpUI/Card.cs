using Code.Scripts.Items;
using Code.Scripts.Players;
using PSB_Lib.Dependencies;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public  bool iClicked { get; private set; } = false;
    public static bool clicked { get; private set; } = false;
    public LevelUpItemSO _levelUpSO;
    public CardManager _cardManager;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _image;
    [Inject] private Player _player;

    public void CardGetBasic(LevelUpItemSO levelUpSO)
    {
        _levelUpSO = levelUpSO;
        _descriptionText.text = levelUpSO.Description;
        _image.sprite = levelUpSO.SkillIcon;
    }

    public void OnClickCard()
    {
        if(!clicked)
        {
            iClicked = true;
            clicked = true;
            _levelUpSO.selectCount++;
            _levelUpSO.ApplyItem(_player);
        }
    }

    public static void SetClicked(bool value)
    {
        clicked = value;
    }
    public void SetIClicked(bool value)
    {
        iClicked = value;
    }
}
