using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public Action OnCollide;
    private Collision2D _lastCollision;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        _lastCollision = other;
        OnCollide?.Invoke();
    }
    
    public Collision2D GetLastCollision()
    {
        return _lastCollision;
    }
}
