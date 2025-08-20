using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField] float minMove = 0.01f;   
    [SerializeField] float offHold = 0.12f;  

    Animator anim;
    SpriteRenderer sr;
    Vector3 prevPos;
    float offTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        sr   = GetComponent<SpriteRenderer>();
        prevPos = transform.position;
    }

    void Update()
    {
        Vector3 cur = transform.position;
        float dispSqr = (cur - prevPos).sqrMagnitude;
        prevPos = cur;

        bool rawMoving = dispSqr >= (minMove * minMove);
        if (rawMoving) offTimer = offHold; else offTimer -= Time.deltaTime;

        bool moving = offTimer > 0f;

        if (anim) anim.speed = moving ? 1f : 0f;
        if (sr)   sr.enabled = moving;
    }
}