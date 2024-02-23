using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _currentDirection;
    private bool _grounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = new Vector2(direction.x * moveSpeed, _rigidbody.velocity.y);
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
