using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UnSuck : EnemyBase, IDamageable
{
    private Rigidbody2D _rb;
    [SerializeField]float _speed=1f;
    public float currentHP = 0;
    [SerializeField] private float _maxHP=5000f;
    [SerializeField] private float _RotationSpeed=2f;
    [SerializeField] private Transform _player;
    [SerializeField] private int playerLayer;
    private Vector2 _movedir;
    
    public UnityEvent OnDamagedEvent;
    
    private float bonusHealth = 0f;
    private float bonusSpeed = 0f;
    
    private Animator _animator;
    private Camera _mainCamera;
    private SpriteRenderer _spriteRenderer;
    AudioSource _audioSource;
    private bool _Degam;
    private bool _die;

    private SpawnUnsuk _poolHandler;
    private float _poolCheckDistance = 30f; // 카메라와의 거리 기준값(원하는 값으로 조정)

    protected override void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _audioSource = GetComponentInChildren<AudioSource>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        currentHP=_maxHP;
        _mainCamera = Camera.main;
    }

    protected override void Attack()
    {
    }

    public override void IncreaseAttack(float amount)
    {
    }

    public override void IncreaseDefense(float amount)
    {
        bonusHealth += amount;
        _maxHP += amount; 
    }
    public override void IncreaseSpeed(float amount)
    {
        bonusSpeed += amount;
        _speed += amount; 
    }

    protected override void Start()
    {
        _die=false;
    }

    void Update()
    {
        CheckBang();
        CheckCamera();
        if (currentHP>0f)
        {
            transform.Rotate(0, 0, _RotationSpeed * 4f * Time.deltaTime);
        }
        // 카메라와 일정 거리 이상 멀어지면 Pool 반환
        if (_mainCamera != null && !_die)
        {
            float dist = Vector2.Distance(transform.position, _mainCamera.transform.position);
            if (dist > _poolCheckDistance)
            {
                PoolReturn();
            }
        }
    }

    private void CheckBang()
    {
        if (currentHP <= 0)
        {
            OnDeadEvent?.Invoke();
            if (_die) return;                     
            _die = true;

            _animator.SetTrigger("isdead");
            _rb.linearVelocity = Vector2.zero;

            if (TryGetComponent(out Collider2D col))
                col.enabled = false;     

            GetComponentInChildren<UnSuckExplosion>()?.Bob();
            StartCoroutine(Faid());     
            PoolReturn(); // 죽을 때 Pool 반환
            return;
        }
        else if (currentHP < _maxHP * 0.35f)
        {
            _animator.SetBool("isLowHP 0", true);
            _rb.linearVelocity = _movedir * (_speed + 0.2f);
        }
        else if (currentHP < _maxHP * 0.7f)
        {
            _animator.SetTrigger("isLowHP");
            _rb.linearVelocity = _movedir * (_speed + 0.1f);
        }
    }
    private void CheckCamera()
    {
        Vector3 viewPos = _mainCamera.WorldToViewportPoint(transform.position);
        bool isVisible = viewPos.z > 0 && viewPos.x > -0.03f && viewPos.x < 1.03f && viewPos.y > -0.03f && viewPos.y < 1.03f;

        if (isVisible&&!_Degam)
        {
            _Degam = true;
        }
    }

    void Findplayer()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_die) return;                          
        if (collision.gameObject.layer != playerLayer) return;
        
        _animator.SetTrigger("isHp");
        currentHP = 0f;     
    }

    private void OnEnable()
    {
        currentHP=_maxHP;
        _die = false;  
        
        if (TryGetComponent(out Collider2D col)) col.enabled = true;
            if (_spriteRenderer != null) {
                var c = _spriteRenderer.color; c.a = 1f; _spriteRenderer.color = c;
            }
        Findplayer();
        _movedir = (_player.position - transform.position).normalized;
        _rb.linearVelocity = _movedir * _speed;  
    }


    private IEnumerator Faid()
    {
        
        Color color=_spriteRenderer.color;
        for (float a = 1f; a > 0f; a -= Time.deltaTime * 1.5f)
        {
            color.a=a;
            _spriteRenderer.color = color;
            yield return null; 
        }
        gameObject.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        if(_die) return;
        currentHP=Mathf.Max(0f,currentHP-amount);
        OnDamagedEvent?.Invoke();
    }

    public void SetPoolHandler(SpawnUnsuk handler)
    {
        _poolHandler = handler;
    }

    private void PoolReturn()
    {
        if (_poolHandler != null)
        {
            _poolHandler.UnsukDie(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    
    
}
