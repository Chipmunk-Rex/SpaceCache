using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectCard;
    [SerializeField] private LevelUpSO[] levelUpSO;
    public bool dontClick { get; private set; } = false;
    private bool test = false;
    private int rand;

    private void Awake()
    {
    
    }
    private void Update()
    {
        if(!test)
        {
            ImageChange();
            StartCoroutine(CardDown());
        }
    }

    public void ImageChange()
    {
        for(int i = 0; i < 3; i++)
        {
            rand = Random.Range(0, levelUpSO.Length);
            Card a = gameObjectCard[i].GetComponent<Card>();
            a.CardGetBasic(levelUpSO[rand]);
        }
    }

    IEnumerator CardDown()
    {
        test = true;
        foreach (var a in gameObjectCard)
        {
            a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y - 1000, 0), 1);
            yield return new WaitForSeconds(0.5f);
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
                a.transform.DOMove(new Vector3(a.transform.position.x, a.transform.position.y + 1000, 0), 1);
                yield return new WaitForSeconds(0.5f);
           }
            Card.SetClicked(false);
            card.SetIClicked(false);
        }
    }
}
