// using Code.Scripts.Items;
// using Code.Scripts.Players;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
//
// namespace Work.Chipmunk._01.Scripts
// {
//     public class CardUI : MonoBehaviour
//     {
//         public LevelUpItemSO levelUpSO;
//         public CardListUI cardListUI;
//         [SerializeField] private TextMeshProUGUI descriptionText;
//         [SerializeField] private Image image;
//         [SerializeField] private Player player; // DI가 필요하면 외부에서 할당
//
//         public void SetCard(LevelUpItemSO so, CardListUI listUI)
//         {
//             levelUpSO = so;
//             cardListUI = listUI;
//             descriptionText.text = so.Description;
//             image.sprite = so.SkillIcon;
//         }
//
//         public void OnClickCard()
//         {
//             if (!clicked && cardListUI != null && !cardListUI.DontClick)
//             {
//                 iClicked = true;
//                 clicked = true;
//                 levelUpSO.selectCount++;
//                 levelUpSO.ApplyItem(player);
//                 cardListUI.StartCardUp();
//             }
//         }
//
//         public static void SetClicked(bool value)
//         {
//             clicked = value;
//         }
//         public void SetIClicked(bool value)
//         {
//             iClicked = value;
//         }
//         public void CardImageActive()
//         {
//             gameObject.SetActive(true);
//         }
//     }
// }