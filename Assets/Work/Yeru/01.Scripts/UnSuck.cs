using System;
using UnityEngine;

public class UnSuck : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]float _speed=5f;
    [SerializeField] private float _currentHP=0;
    [SerializeField] private float _maxHP=5000f;
    [SerializeField] private float _RotationSpeed=2f;
    [SerializeField] private Transform _player;
    
    private Animator _animator;
    private Camera _mainCamera;
    private bool _Degam;

    void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _currentHP=_maxHP;
        _mainCamera = Camera.main;
    }
    void Update()
    {
        Checkbang();
        CheckCamera();
        transform.Rotate(0, 0, _RotationSpeed * 4f * Time.deltaTime);
    }

    private void Checkbang()
    {
        if (_currentHP <= 0)
        {
            _animator.SetTrigger("isDead");
        }
        else if (_currentHP<_maxHP*0.25f)
        {
            _animator.SetBool("isLowHP 0", true);
        }
       else if (_currentHP<_maxHP*0.5f)
        {
            _animator.SetTrigger("isLowHP");
        }
    }
    private void CheckCamera()
    {
        Vector3 viewPos = _mainCamera.WorldToViewportPoint(transform.position);
        bool isVisible = viewPos.z > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;

        if (isVisible&&!_Degam)
        {
            _Degam = true;
            Debug.Log("하.하.");
            
        }
    }

    void Findplayer()
    {
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
    private void OnEnable()
    {
        Findplayer();
        Vector3 dir=(_player.position-transform.position).normalized;
        _rb.linearVelocity=dir*_speed;
    }
}
