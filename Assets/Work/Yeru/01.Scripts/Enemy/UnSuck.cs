using System;
using System.Collections;
using UnityEngine;

public class UnSuck : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rb;
    [SerializeField]float _speed=1f;
    [SerializeField] private float _currentHP=0;
    [SerializeField] private float _maxHP=5000f;
    [SerializeField] private float _RotationSpeed=2f;
    [SerializeField] private Transform _player;
    [SerializeField] private string _playerTag = "Player";
    private Vector2 _movedir;
    
    private Animator _animator;
    private Camera _mainCamera;
    private SpriteRenderer _spriteRenderer;
    AudioSource _audioSource;
    private bool _Degam;
    private bool _die;

    void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentHP=_maxHP;
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _die=false;
    }

    void Update()
    {
        Checkbang();
        CheckCamera();
        if (_currentHP>0f)
        {
            transform.Rotate(0, 0, _RotationSpeed * 4f * Time.deltaTime);
        }
    }

    private void Checkbang()
    {
        if (_currentHP <= 0)
        {
            if (_die) return;                     
            _die = true;

            _animator.SetTrigger("isDead");
            _rb.linearVelocity = Vector2.zero;

            if (TryGetComponent(out Collider2D col))
                col.enabled = false;     

            GetComponent<UnSuckExplosion>()?.Bob();
            StartCoroutine(Faid());                
            return;
        }
        else if (_currentHP < _maxHP * 0.25f)
        {
            _animator.SetBool("isLowHP 0", true);
            _rb.linearVelocity = _movedir * (_speed + 0.2f);
        }
        else if (_currentHP < _maxHP * 0.5f)
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
            Debug.Log("da");
        }
    }

    void Findplayer()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_die) return;                          
        if (!other.CompareTag(_playerTag)) return; 

        _currentHP = 0f;                           
    }

    private void OnEnable()
    {
        _die = false;  
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
        _currentHP=Mathf.Max(0f,_currentHP-amount);
    }
   
}    