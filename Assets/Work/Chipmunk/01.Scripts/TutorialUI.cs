using System;
using System.Collections;
using Code.Scripts.Players;
using DG.Tweening;
using PSB_Lib.Dependencies;
using TMPro;
using UnityEngine;

namespace Work.Chipmunk._01.Scripts
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO _playerInputSo;
        [SerializeField] private RectTransform _tutorialUI;
        private float defaultXPosition;
        [SerializeField] private TMP_Text _tutorialText;

        [SerializeField] private float animationDuration = 0.5f;

        [SerializeField] [Multiline] private string _moveTutorial = "마우스 우클릭으로 이동하세요.";
        [SerializeField] [Multiline] private string _attackTutorial = "마우스 좌클릭으로 공격하세요.";
        [SerializeField] [Multiline] private string _levelTutorial = "경험치를 획득해 레벨을 올리세요.";


        [Inject] private PlayerLevelSystem _playerLevelSystem;

        private void Awake()
        {
            defaultXPosition = _tutorialUI.anchoredPosition.x;
            StartCoroutine(TutorialRoutine());
        }

        private IEnumerator TutorialRoutine()
        {
            _tutorialText.text = _moveTutorial;
            while (_playerInputSo.IsMoving == false)
            {
                yield return null;
            }

            yield return HideUI();

            _tutorialText.text = _attackTutorial;

            yield return ShowUI();
            ;
            while (_playerInputSo.IsAttackPressed == false)
            {
                yield return null;
            }

            yield return HideUI();

            _tutorialText.text = _levelTutorial;

            yield return ShowUI();
            bool isLevelUp = false;
            _playerLevelSystem.OnLevelUp += () => isLevelUp = true;
            while (isLevelUp == false)
            {
                yield return null;
            }

            yield return HideUI();
        }

        private IEnumerator HideUI()
        {
            _tutorialUI.DOAnchorPosX(defaultXPosition - 1000, animationDuration);
            yield return new WaitForSeconds(animationDuration);
        }

        private IEnumerator ShowUI()
        {
            _tutorialUI.DOAnchorPosX(defaultXPosition, animationDuration);
            yield return new WaitForSeconds(animationDuration);
        }
    }
}