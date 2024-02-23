using _Project.Gameplay.Shared.Scripts.Utility;
using UnityEngine;

namespace _Project.Gameplay.Shared.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Vector2 _direction;
        private Damager _damager;
        private Damageable _damageable;
        private CollisionDetector _fistCollisionDetector;
        private Animator _animator;
    

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _damager = GetComponent<Damager>();
            _damageable = GetComponent<Damageable>();
            _fistCollisionDetector = GetComponentInChildren<CollisionDetector>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _fistCollisionDetector.OnCollide += OnPunch;
            _mover.OnDodgeStart += OnDodgeStart;
            _mover.OnDodgeEnd += OnDodgeEnd;
        }

        private void OnDisable()
        {
            _fistCollisionDetector.OnCollide -= OnPunch;
            _mover.OnDodgeStart -= OnDodgeStart;
            _mover.OnDodgeEnd -= OnDodgeEnd;
        }

        private void Update()
        {
            if (_mover.IsDodging()) return;
            
            if (Input.GetKey(KeyCode.D))
            {
                _direction = Vector2.right;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _direction = Vector2.left;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                _direction = Vector2.zero;
            }
            
            if (_direction != Vector2.zero)
                _mover.Move(_direction);
            else
                _mover.Stop();
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _mover.Jump();
            }
            
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _mover.Dodge();
            }
        
            if (Input.GetKeyDown(KeyCode.F))
            {
                _animator.SetTrigger(Animations.Punch);
            }
        }
    
        private void OnPunch()
        {
            _damager.Damage(_fistCollisionDetector.GetLastCollision().gameObject.GetComponent<Damageable>());
        }
        
        private void OnDodgeStart()
        {
            _damageable.SetInvincible();
        }
        
        private void OnDodgeEnd()
        {
            _damageable.SetVulnerable();
        }
    }
}
