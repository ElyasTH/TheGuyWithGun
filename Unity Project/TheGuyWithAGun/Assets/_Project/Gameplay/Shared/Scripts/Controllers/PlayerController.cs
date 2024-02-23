using _Project.Gameplay.Shared.Scripts.Utility;
using UnityEngine;

namespace _Project.Gameplay.Shared.Scripts.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Vector2 _direction;
        private Damager _damager;
        private CollisionDetector _fistCollisionDetector;
        private Animator _animator;
    

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _damager = GetComponent<Damager>();
            _fistCollisionDetector = GetComponentInChildren<CollisionDetector>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _fistCollisionDetector.OnCollide += OnPunch;
        }

        private void OnDisable()
        {
            _fistCollisionDetector.OnCollide -= OnPunch;
        }

        private void Update()
        {
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
        
            _mover.Move(_direction);
        
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _mover.Jump();
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
    }
}
