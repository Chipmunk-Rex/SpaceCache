using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [Header("base setting")]
    [SerializeField] private GameObject playerProfile;
    [SerializeField] private GameObject circle1;
    [SerializeField] private GameObject circle3;

    [Header("Player Profile Setting")]
    [SerializeField] private Sprite playerImage;

    [Header("Rotate speed setting")]
    [SerializeField] private float circle1Speed;
    [SerializeField] private float circle3Speed;
    [SerializeField] private Vector2 circle3Scale;

    float rotate = 0;
    float time = 0;
    

    private void Start()
    {
        ImageChange();
        StartCoroutine(TurnCircle());
    }

    public void ImageChange() => playerProfile.GetComponent<Image>().sprite = playerImage;

    private IEnumerator TurnCircle()
    {
        while (true)
        {
            rotate++;
            circle1.transform.localRotation = Quaternion.Euler(0, 0, -circle1Speed * rotate);
            circle3.transform.localRotation = Quaternion.Euler(0, 0, circle3Speed * rotate);
            yield return new WaitForSeconds(0.04f);
        }
    }
    
    

}
