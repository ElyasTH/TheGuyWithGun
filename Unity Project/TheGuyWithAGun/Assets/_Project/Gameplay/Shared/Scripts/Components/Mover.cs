using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float dodgeSpeed = 12f;
    [SerializeField] private float dodgeDuration = 0.5f;
    [SerializeField] private float dodgeCooldown = 1f;

    private Rigidbody2D _rigidbody;
    [SerializeField] private Vector2 _currentDirection;
    private bool _grounded;
    private bool _dodging;
    private bool _canDodge = true;

    public Action OnDodgeStart;
    public Action OnDodgeEnd;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.x * moveSpeed, _rigidbody.velocity.y);
        _currentDirection = direction;
    }

    public void Stop()
    {
        _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        if (!_grounded) return;
        _rigidbody.AddForce(Vector2.up * jumpForce);
    }

    public void Dodge()
    {
        if (!_canDodge) return;
        OnDodgeStart.Invoke();
        StartCoroutine(DodgeRoutine());
    }
    
    public bool IsDodging()
    {
        return _dodging;
    }

    private IEnumerator DodgeRoutine()
    {
        _dodging = true;
        _canDodge = false;
        float timer = 0f;
        while (timer < dodgeDuration)
        {
            _rigidbody.velocity = new Vector2(_currentDirection.x * dodgeSpeed, _rigidbody.velocity.y);
            timer += Time.deltaTime;
            yield return null;
        }
        
        OnDodgeEnd.Invoke();
        _rigidbody.velocity = Vector2.zero;
        _dodging = false;
        
        yield return new WaitForSeconds(dodgeCooldown);
        _canDodge = true;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagLayerManager.Ground))
        {
            _grounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(TagLayerManager.Ground))
        {
            _grounded = false;
        }
    }
}
