using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public LevelUpSO _levelUpSO;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _image;
    private void Awake()
    {
        CardGetID();
    }

    private void CardGetID()
    {
        _nameText.text = _levelUpSO.Cardname;
        _descriptionText.text = _levelUpSO.CardDescription;
        _image.sprite = _levelUpSO.CardImage;

    }
}
