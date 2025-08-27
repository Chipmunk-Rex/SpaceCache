using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectCard;
    [SerializeField] private LevelUpSO[] levelUpSO;
    [SerializeField] private CardMusic cardMusic;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject skillUi;
    private Dictionary<LevelUpSO,GameObject> destroyCopy = new();
    public bool dontClick { get; private set; } = false;
    private int rand;

    private void Awake()
    {
        foreach(var so in levelUpSO)
        {
            so.cardUiSpawn = false;
            so.level = 0;
        }
    }
    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ImageChange();
            StartCoroutine(CardDown());
        }
    }
    public void ImageChange()
    {
        for(int i = 0; i < 3; i++)
        {
            var candidates = levelUpSO.Where(so => so.level < so.maxLevel).ToList();
            if (candidates.Count == 0)
            {
                Debug.Log("»ÌÀ»°Ô ¾øÀ½");
                continue;
            }

            rand = Random.Range(0, candidates.Count);
            Card a = gameObjectCard[i].GetComponent<Card>();
            a.CardGetBasic(candidates[rand]);
        }
    }
    #region Card Mover
    IEnumerator CardDown()
    {
        foreach (var a in gameObjectCard)
        {
            cardMusic.SlideCard();
            a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y - 1000, 0), 1);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(1);
    }

    public void StartCardUp()
    {
        if(!dontClick)
        {
            dontClick = true;
            StartCoroutine(CardUp());
            Debug.Log("½ÇÇàµÊ");
        }
    }
    IEnumerator CardUp()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (var a in gameObjectCard)
        {
            Card card = a.GetComponent<Card>();
            Debug.Log(card.iClicked);
            if (card.iClicked)
            {
                GameObject obj = null;
                if(!card._levelUpSO.cardUiSpawn)
                {
                    obj = Instantiate(skillUi, content.transform);
                    obj.GetComponent<Image>().sprite = card._levelUpSO.CardImage;
                    card._levelUpSO.cardUiSpawn = true;
                    SkillUiStar skillUiStar = obj.GetComponent<SkillUiStar>();
                    skillUiStar.StarInstantiate(card._levelUpSO);
                    destroyCopy.Add(card._levelUpSO, obj);
                }
                else
                {
                    obj = Instantiate(skillUi, content.transform);
                    obj.GetComponent<Image>().sprite = card._levelUpSO.CardImage;
                    SkillUiStar skillUiStar = obj.GetComponent<SkillUiStar>();
                    skillUiStar.StarInstantiate(card._levelUpSO);
                    if (destroyCopy.ContainsKey(card._levelUpSO))
                    {
                        Destroy(destroyCopy[card._levelUpSO]);
                    }
                    destroyCopy[card._levelUpSO] = obj;
                }


                a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y - 200, 0), 0.5f);
                yield return new WaitForSeconds(0.5f);
                a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y + 1200, 0), 0.7f);
                yield return new WaitForSeconds(0.6f);
            }
        }

        foreach (var a in gameObjectCard)
        {
            Card card = a.GetComponent<Card>();
           if (!card.iClicked)
           {
                cardMusic.SlideCard();
                a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y + 1000, 0), 1);
                yield return new WaitForSeconds(0.5f);
           }
            Card.SetClicked(false);
            card.SetIClicked(false);
        }
        yield return new WaitForSeconds(0.5f);
        dontClick = false;
    }
    #endregion
}
