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
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectCard;
    [SerializeField] private LevelUpItemSO[] levelUpSO;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject skillUi;
    [SerializeField] private GameObject biHangGi_image;
    [SerializeField] private GameObject airPlane;

    [SerializeField] private float downUiValue = -1400;
    [SerializeField] private float upUiValue = 770;
    [SerializeField] private float downPlaneUiValue = -1000;
    [SerializeField] private float upPlaneUiValue = 1000;
    
    [Inject] private PlayerLevelSystem _playerLevelSystem;
    
    private Dictionary<LevelUpItemSO,GameObject> _destroyCopy = new();
    private HashSet<ItemType> _selectedSlots = new();
    
    public bool DontClick { get; private set; } = false;
    private int _rand;

    public enum UIState { Idle, Down, Up, Moving }

    public UIState currentState = UIState.Up;
    private Coroutine _currentCoroutine = null;

    private int pauseRef = 0;

    private void Awake()
    {
        foreach(var so in levelUpSO)
        {
            so.cardUiSpawn = false;
            so.selectCount = 0;
        }
    }

    private void OnEnable()
    {
        _playerLevelSystem.OnLevelUp += LevelUpUIOpen;
    }

    private void OnDestroy()
    {
        _playerLevelSystem.OnLevelUp -= LevelUpUIOpen;
    }

    public void SetRaycastsAll(bool enabled)
    {
        foreach (var go in gameObjectCard)
        {
            var cg = go.GetComponent<CanvasGroup>();
            if (cg) cg.blocksRaycasts = enabled;
        }
    }

    private void LevelUpUIOpen()
    {
        SetPaused(true);
        ImageChange();
        RequestCardDown();
    }

    public void ImageChange()
    {
        HashSet<LevelUpItemSO> usedThisDraw = new HashSet<LevelUpItemSO>();
        
        for(int i = 0; i < 3; i++)
        {
            var candidates = levelUpSO.Where(so =>
                    so.selectCount < so.maxCount &&
                    !usedThisDraw.Contains(so) 
            ).ToList();
            
            if (candidates.Count == 0)
            {
                continue;
            }

            _rand = Random.Range(0, candidates.Count);
            LevelUpItemSO chosen = candidates[_rand];

            Card card = gameObjectCard[i].GetComponent<Card>();
            card.CardGetBasic(chosen);

            usedThisDraw.Add(chosen);
        }
    }

    #region Request / Wrappers
    public void RequestCardDown()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentCoroutine = StartCoroutine(CardDownWrapper());
    }

    public void RequestCardUp()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        _currentCoroutine = StartCoroutine(CardUpWrapper());
    }

    private void SetPaused(bool paused)
    {
        if (paused)
        {
            pauseRef++;
            if (pauseRef == 1)
            {
                Time.timeScale = 0f;
            }
        }
        else
        {
            pauseRef = Mathf.Max(0, pauseRef - 1);
            if (pauseRef == 0)
            {
                Time.timeScale = 1f;
            }
        }
    }
    #endregion

    #region Card Mover
    private IEnumerator CardDownWrapper()
    {
        currentState = UIState.Moving;
        SetPaused(true);

        RectTransform bihanggiRect = (RectTransform)biHangGi_image.transform;
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(bihanggiRect.DOAnchorPosY(downPlaneUiValue, 1f).SetUpdate(true));

        int minusAnchor = 0;
        foreach (var a in gameObjectCard)
        {
            CardScaler cardScaler = a.GetComponent<CardScaler>();
            cardScaler.CardImageActive();
            RectTransform rt = (RectTransform)a.transform;
            seq.Join(rt.DOAnchorPosY(downUiValue + minusAnchor, 1f).SetUpdate(true));
            minusAnchor += 350;
        }
        yield return seq.WaitForCompletion();

        currentState = UIState.Down;
        _currentCoroutine = null;
    }

    public void StartCardUp()
    {
        if(!DontClick)
        {
            DontClick = true;
            RequestCardUp();
        }
    }

    public void AirPlaneMove()
    {
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(airPlane.transform.DOScale(1.4f, 0.5f).SetEase(Ease.InBounce)).SetUpdate(true);
        seq.Append(airPlane.transform.DOScale(1, 0.6f)).SetUpdate(true);
    }
    
    private IEnumerator CardUpWrapper()
    {
        currentState = UIState.Moving;

        yield return new WaitForSecondsRealtime(0.1f);

        foreach (var a in gameObjectCard)
        {
            Card card = a.GetComponent<Card>();
            
            if (card.iClicked)
            {
                GameObject obj = null;
                if(!card._levelUpSO.cardUiSpawn)
                {
                    obj = Instantiate(skillUi, content.transform);
                    obj.GetComponent<Image>().sprite = card._levelUpSO.SkillIcon;
                    card._levelUpSO.cardUiSpawn = true;
                    SkillUiStar skillUiStar = obj.GetComponent<SkillUiStar>();
                    skillUiStar.StarInstantiate(card._levelUpSO);
                    _destroyCopy.Add(card._levelUpSO, obj);
                }
                else
                {
                    obj = Instantiate(skillUi, content.transform);
                    obj.GetComponent<Image>().sprite = card._levelUpSO.SkillIcon;
                    SkillUiStar skillUiStar = obj.GetComponent<SkillUiStar>();
                    skillUiStar.StarInstantiate(card._levelUpSO);
                    if (_destroyCopy.ContainsKey(card._levelUpSO))
                    {
                        Destroy(_destroyCopy[card._levelUpSO]);
                    }
                    _destroyCopy[card._levelUpSO] = obj;
                }

                a.SetActive(false);
                RectTransform rt = (RectTransform)a.transform;

                float flash = 1f;
                Image airPlane_image = airPlane.GetComponent<Image>();
                airPlane_image.material.SetFloat("_Flash", flash);

                AirPlaneMove();
                SetPaused(false);

                while (flash > 0f)
                {
                    flash -= Time.unscaledDeltaTime * 2f;
                    airPlane_image.material.SetFloat("_Flash", flash);
                    yield return null;
                }

                rt.DOAnchorPos(new Vector2(upUiValue, 0), 0.1f).SetUpdate(true);
                yield return new WaitForSecondsRealtime(0.1f);
                a.SetActive(true);
            }
        }
        
        foreach (var a in gameObjectCard)
        {
            Card card = a.GetComponent<Card>();
            if (!card.iClicked)
            {
                RectTransform rt = (RectTransform)a.transform;
                rt.DOAnchorPos(new Vector2(upUiValue, 0),1f).SetUpdate(true);
                yield return new WaitForSecondsRealtime(0.5f);
            }
            Card.SetClicked(false);
            card.SetIClicked(false);
            
        }

        RectTransform bihanggiRect = (RectTransform)biHangGi_image.transform;
        bihanggiRect.DOAnchorPosY(upPlaneUiValue, 1f).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        
        SetPaused(false);

        DontClick = false;
        currentState = UIState.Up;
        _currentCoroutine = null;
    }
    #endregion
    
}
