using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Action OnCollide;
    public Action OnExit;
    private Collision2D _lastCollision;
    private Collider2D _lastCollider;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        _lastCollision = other;
        _lastCollider = null;
        OnCollide?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _lastCollider = other;
        _lastCollision = null;
        OnCollide?.Invoke();   
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        _lastCollision = other;
        _lastCollider = null;
        OnExit?.Invoke();
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _lastCollider = other;
        _lastCollision = null;
        OnExit?.Invoke();
    }

    public Collision2D GetLastCollision()
    {
        return _lastCollision;
    }
    
    public Collider2D GetLastCollider()
    {
        return _lastCollider;
    }
}
