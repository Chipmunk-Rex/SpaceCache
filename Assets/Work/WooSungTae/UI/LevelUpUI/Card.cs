using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public  bool iClicked { get; private set; } = false;
    public static bool clicked { get; private set; } = false;
    public LevelUpSO _levelUpSO;
    public CardManager _cardManager;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _image;

    public void CardGetBasic(LevelUpSO levelUpSO)
    {
        _nameText.text = levelUpSO.Cardname;
        _descriptionText.text = levelUpSO.CardDescription;
        _image.sprite = levelUpSO.CardImage;
    }

    public void OnClickCard()
    {
        if(!clicked)
        {
            iClicked = true;
            clicked = true;
            _levelUpSO.level++;
            Debug.Log("´­¸²");
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
