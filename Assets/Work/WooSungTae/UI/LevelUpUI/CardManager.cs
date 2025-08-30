using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.Items;
using Code.Scripts.Players;
using DG.Tweening;
using PSB_Lib.Dependencies;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectCard;
    [SerializeField] private LevelUpItemSO[] levelUpSO;
    [SerializeField] private CardMusic cardMusic;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject skillUi;

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
                    so.selectCount < so.maxCount &&                          // maxCount 넘으면 제외
                    (!IsFixedSlot(so.itemType) || !selectedSlots.Contains(so.itemType)) && // Q/E/R 이미 선택된 슬롯 제외
                    !usedThisDraw.Contains(so)                               // 같은 세트에서 중복 제외
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

            usedThisDraw.Add(chosen); // 이번 세트에선 다시 안 나오게
        }
    }
    
    #region Card Mover
    IEnumerator CardDown()
    {
        foreach (var a in gameObjectCard)
        {
            cardMusic.SlideCard();
            a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y - 1000, 0), 1).SetUpdate(true);
            yield return new WaitForSecondsRealtime(0.3f);
        }
        yield return new WaitForSecondsRealtime(1);
    }

    public void StartCardUp()
    {
        if(!dontClick)
        {
            dontClick = true;
            StartCoroutine(CardUp());
            Debug.Log("�����");
        }
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
                LevelUpItemSO so = card._levelUpSO;
            
                if (IsFixedSlot(so.itemType))
                {
                    selectedSlots.Add(so.itemType);
                }
                
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


                a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y - 200, 0), 0.5f).SetUpdate(true);
                yield return new WaitForSecondsRealtime(0.5f);
                a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y + 1200, 0), 0.7f).SetUpdate(true);
                yield return new WaitForSecondsRealtime(0.6f);
            }
        }

        foreach (var a in gameObjectCard)
        {
            Card card = a.GetComponent<Card>();
           if (!card.iClicked)
           {
                cardMusic.SlideCard();
                a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y + 1000, 0), 1).SetUpdate(true);
                yield return new WaitForSecondsRealtime(0.5f);
           }
            Card.SetClicked(false);
            card.SetIClicked(false);
            
        }
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 1;
        dontClick = false;
    }
    #endregion
    
    private bool IsFixedSlot(ItemType type)
    {
        return type == ItemType.QCLICK || 
               type == ItemType.ECLICK || 
               type == ItemType.RCLICK;
    }
    
}
