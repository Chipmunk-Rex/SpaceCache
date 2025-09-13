using UnityEngine;
using UnityEngine.UI;
using Code.Scripts.Items.Combat;
using Code.Scripts.Entities;

public class BossUI : MonoBehaviour
{
    [SerializeField] private Slider bossHpSlider;
    [SerializeField] private GameObject bossHpPanel;

    private EntityHealth _currentBossHealth;
    private Entity _currentEntity;
    
    public static BossUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        bossHpPanel.SetActive(false);
    }

    public void Show(EntityHealth bossHealth, Entity entity)
    {
        if (_currentBossHealth != null)
            _currentBossHealth.OnHealthChanged -= UpdateHp;
        if (_currentEntity != null)
            _currentEntity.OnDeadEvent.RemoveListener(HandleBossDead);

        _currentBossHealth = bossHealth;
        _currentEntity = entity;

        bossHpPanel.SetActive(true);
        bossHpSlider.maxValue = bossHealth.maxHealth;
        bossHpSlider.value = bossHealth.currentHealth;

        bossHealth.OnHealthChanged += UpdateHp;
        entity.OnDeadEvent.AddListener(HandleBossDead);
    }

    public void Hide()
    {
        bossHpPanel.SetActive(false);

        if (_currentBossHealth != null)
            _currentBossHealth.OnHealthChanged -= UpdateHp;
        if (_currentEntity != null)
            _currentEntity.OnDeadEvent.RemoveListener(HandleBossDead);

        _currentBossHealth = null;
        _currentEntity = null;
    }

    private void UpdateHp(float current, float max)
    {
        bossHpSlider.maxValue = max;
        bossHpSlider.value = current;
    }

    private void HandleBossDead()
    {
        Hide();
    }
}