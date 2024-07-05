using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Animator
        _animator;

    public float 
        _moveSpeed = 5f;

    private Rigidbody2D 
        _rb;
        
    private Vector2 
        _moveDirection;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        SetInitialDirection();
    }

    void FixedUpdate()
    {
        _rb.velocity = _moveDirection * _moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var normal = collision.contacts[0].normal;
        _moveDirection = Vector2.Reflect(_moveDirection, normal).normalized;
    }
    void SetInitialDirection()
    {
        var initialAngle = Random.Range(225f, 315f);

        _moveDirection = new Vector2(Mathf.Cos(initialAngle * Mathf.Deg2Rad), Mathf.Sin(initialAngle * Mathf.Deg2Rad)).normalized;
    }
    private IEnumerator DieCo()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
    public void OnStomped()
    {
        StartCoroutine(DieCo());
    }
}
