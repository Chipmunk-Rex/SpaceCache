using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("base setting")]
    [SerializeField] private GameObject _playerState;
    [SerializeField] private GameObject _playerHp_bar;
    [SerializeField] private GameObject _playerExp_bar;
    [SerializeField] private GameObject _playerProfile;
    [SerializeField] private GameObject _circle1;
    [SerializeField] private GameObject _circle2;
    [SerializeField] private GameObject _circle3;

    [Header("Player Profile Setting")]
    [SerializeField] private Sprite playerImage;

    [Header("Slider")]
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _expSlider;

    [Header("Rotate speed setting")]
    [SerializeField] private float _circle1Speed;
    [SerializeField] private float _circle3Speed;
    [SerializeField] private Vector2 _circle3Scale;

    float rotate = 0;
    float time = 0;

    [Header("hit check")]
    [field:SerializeField] public bool _hitted { get; private set; } = false;
    [SerializeField] private float hitContinue = 3;
    

    private void Start()
    {
        ImageChange();
        StartCoroutine(TurnCircle());
    }

    [ContextMenu("hit test")]
    public void TakeDamage()
    {
        time = 0;
        if (_hitted) return;
        _hitted = true;
        StartCoroutine(HittedTurn());
    }

    public void ImageChange() => _playerProfile.GetComponent<Image>().sprite = playerImage;

    public void StateUISpawn()
    {

    }

    IEnumerator StateUISpawn_Coroutine()
    {
        yield return new WaitForSeconds(0.3f);

    }

    IEnumerator TurnCircle()
    {
        while (true)
        {
            rotate++;
            _circle1.transform.localRotation = Quaternion.Euler(0, 0, -_circle1Speed * rotate);
            _circle3.transform.localRotation = Quaternion.Euler(0, 0, _circle3Speed * rotate);
            yield return new WaitForSeconds(0.04f);
            if (_hitted)
                break;
        }
    }

    IEnumerator HittedTurn()
    {
        _circle3.GetComponent<Image>().color = Color.red;
        _circle1.GetComponent<Image>().color = Color.red;
        _circle3.transform.DOScale(_circle3Scale, 0.4f);

        while (true)
        {
            time += Time.deltaTime;
            if (time > hitContinue)
                break;

            rotate++;
            _circle1.transform.localRotation = Quaternion.Euler(0, 0, rotate * _circle1Speed * 2);
            _circle3.transform.localRotation = Quaternion.Euler(0, 0,rotate * _circle3Speed * 2);
            yield return null;
        }
        _circle3.transform.DOScale(new Vector2(1,1), 0.4f);
        _circle3.GetComponent<Image>().color = Color.white;
        _circle1.GetComponent<Image>().color = Color.white;
        _hitted = false;
        StartCoroutine(TurnCircle());
    }

    IEnumerator PlayerGetDamage()
    {
        _playerProfile.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(1f);
        _playerProfile.GetComponent<Image>().color = Color.white;
    }

    #region slider setting
    public void SetHpSliderMaxValue(float maxHp) => _hpSlider.maxValue = maxHp;

    public void SetHpSliderMinValue(float minHp) => _hpSlider.minValue = minHp;

    public void SetHPSliderValue(float hp) => _hpSlider.value = hp;

    public void SetExpSliderMaxValue(float maxExp) => _expSlider.maxValue = maxExp;

    public void SetExpSliderMinValue(float minExp) => _expSlider.minValue = minExp;

    public void SetExpSliderValue(float exp) => _expSlider.value = exp;
    #endregion
}
