using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Items;
using Code.Scripts.Players;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using JetBrains.Annotations;
using PSB_Lib.Dependencies;
using UnityEngine;
using UnityEngine.InputSystem;
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
    
    [Inject] private PlayerLevelSystem playerLevelSystem;
    
    private Dictionary<LevelUpItemSO,GameObject> destroyCopy = new();
    private HashSet<ItemType> selectedSlots = new();
    
    public bool dontClick { get; private set; } = false;
    private int rand;

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
        playerLevelSystem.OnLevelUp += LevelUpUIOpen;
    }

    private void OnDestroy()
    {
        playerLevelSystem.OnLevelUp -= LevelUpUIOpen;
    }

    private void Update()
    {
        if(Keyboard.current.leftAltKey.wasPressedThisFrame)
        {
            ImageChange();
            StartCoroutine(CardDown());
        }
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
        Time.timeScale = 0;
        ImageChange();
        StartCoroutine(CardDown());
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
                Debug.Log("������ ����");
                continue;
            }

            rand = Random.Range(0, candidates.Count);
            LevelUpItemSO chosen = candidates[rand];

            Card card = gameObjectCard[i].GetComponent<Card>();
            card.CardGetBasic(chosen);

            usedThisDraw.Add(chosen);
        }
    }
    
    #region Card Mover
    IEnumerator CardDown()
    {
        RectTransform bihanggiRect = (RectTransform)biHangGi_image.transform;
        bihanggiRect.DOAnchorPosY(downPlaneUiValue, 1).SetUpdate(true);
        int minusAnchor = 0;
        foreach (var a in gameObjectCard)
        {
            CardScaler cardScaler = a.GetComponent<CardScaler>();
            cardScaler.CardImageActive();
            RectTransform rt = (RectTransform)a.transform;
            rt.DOAnchorPosY(downUiValue + minusAnchor, 1f).SetUpdate(true);
            minusAnchor += 350;
            yield return new WaitForSecondsRealtime(0.3f);
        }
        yield return new WaitForSecondsRealtime(1);
    }

    public void StartCardUp()
    {
        Debug.Log("ed");
        if(!dontClick)
        {
            dontClick = true;
            StartCoroutine(CardUp());
        }
    }
    public void AirPlaneMove()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(airPlane.transform.DOScale(1.4f, 0.5f).SetEase(Ease.InBounce)).SetUpdate(true);
        seq.Append(airPlane.transform.DOScale(1, 0.6f)).SetUpdate(true);
    }
    
    IEnumerator CardUp()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        
        foreach (var a in gameObjectCard)
        {
            Card card = a.GetComponent<Card>();
            Debug.Log(card.iClicked);
            
            if (card.iClicked)
            {
                #region ESC button image spawn
                GameObject obj = null;
                if(!card._levelUpSO.cardUiSpawn)
                {
                    obj = Instantiate(skillUi, content.transform);
                    obj.GetComponent<Image>().sprite = card._levelUpSO.SkillIcon;
                    card._levelUpSO.cardUiSpawn = true;
                    SkillUiStar skillUiStar = obj.GetComponent<SkillUiStar>();
                    skillUiStar.StarInstantiate(card._levelUpSO);
                    destroyCopy.Add(card._levelUpSO, obj);
                }
                else
                {
                    obj = Instantiate(skillUi, content.transform);
                    obj.GetComponent<Image>().sprite = card._levelUpSO.SkillIcon;
                    SkillUiStar skillUiStar = obj.GetComponent<SkillUiStar>();
                    skillUiStar.StarInstantiate(card._levelUpSO);
                    if (destroyCopy.ContainsKey(card._levelUpSO))
                    {
                        Destroy(destroyCopy[card._levelUpSO]);
                    }
                    destroyCopy[card._levelUpSO] = obj;
                }
                #endregion

                a.SetActive(false);
                RectTransform rt = (RectTransform)a.transform;

                float flash = 1;
                Image airPlane_image = airPlane.GetComponent<Image>();
                airPlane_image.material.SetFloat("_Flash", flash);
                AirPlaneMove();
                Time.timeScale = 1;
                
                while (flash > 0)
                {
                    flash -= Time.deltaTime * 2;
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

                rt.DOAnchorPos(new Vector2(upUiValue, 0),1).SetUpdate(true);

                yield return new WaitForSecondsRealtime(0.5f);
            }
            Card.SetClicked(false);
            card.SetIClicked(false);
            
        }
        RectTransform bihanggiRect = (RectTransform)biHangGi_image.transform;
        bihanggiRect.DOAnchorPosY(upPlaneUiValue, 1).SetUpdate(true);
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        dontClick = false;
    }
    #endregion
    
}
