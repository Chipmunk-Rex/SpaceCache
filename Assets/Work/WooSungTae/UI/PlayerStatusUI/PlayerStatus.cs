using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private Sprite _playerProfile;

    [Header("Slider")]
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _expSlider;

    private void Start()
    {
        playerImage.sprite = _playerProfile;
    }

    public void SetHpSliderMaxValue(float maxHp) => _hpSlider.maxValue = maxHp;

    public void SetHPSliderValue(float hp) => _hpSlider.value = hp;

    public void SetExpSliderMaxValue(float maxExp) => _expSlider.maxValue = maxExp;

    public void SetExpSliderValue(float exp) => _expSlider.value = exp;
}
