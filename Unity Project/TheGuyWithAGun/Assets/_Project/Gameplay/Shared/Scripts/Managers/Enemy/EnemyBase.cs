using UnityEngine;

namespace _Project.Gameplay.Shared.Scripts.Managers.Enemy
{
    public abstract class EnemyBase : MonoBehaviour
    {
        protected Damager Damager;
        protected Damageable Damageable;
        
        protected Transform Target;
        protected bool Tracking;
    
        protected virtual void Awake()
        {
            Damager = GetComponent<Damager>();
            Damageable = GetComponent<Damageable>();
        }
        
        protected abstract void TrackTarget();
        
        protected abstract void UntrackTarget();
        
        protected abstract void Attack();
        
    }
}
