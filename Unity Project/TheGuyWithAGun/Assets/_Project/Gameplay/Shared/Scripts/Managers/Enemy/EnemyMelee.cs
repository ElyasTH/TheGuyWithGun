using System;
using UnityEngine;

namespace _Project.Gameplay.Shared.Scripts.Managers.Enemy
{
    public class EnemyMelee: EnemyBase
    {
        protected Mover Mover;
        protected CollisionDetector TargetDetector;

        protected override void Awake()
        {
            base.Awake();
            Mover = GetComponent<Mover>();
            TargetDetector = GetComponentInChildren<CollisionDetector>();
            Target = GameObject.FindGameObjectWithTag(TagLayerManager.Player).transform;
        }

        private void OnEnable()
        {
            TargetDetector.OnCollide += OnTargetInRange;
            TargetDetector.OnExit += OnTargetOutOfRange;
            TrackTarget();
        }
        
        private void OnDisable()
        {
            TargetDetector.OnCollide -= OnTargetInRange;
            TargetDetector.OnExit -= OnTargetOutOfRange;
        }

        private void Update()
        {
            if (Tracking)
            {
                if (Target.position.x > transform.position.x)
                {
                    Mover.Move(Vector2.right);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    Mover.Move(Vector2.left);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
        }
        protected override void TrackTarget()
        {
            if (Target == null) return;
            
            Tracking = true;
        }

        protected override void UntrackTarget()
        {
            Tracking = false;
            Mover.Stop();
        }
        
        private void OnTargetInRange()
        {
            UntrackTarget();
        }
        
        private void OnTargetOutOfRange()
        {
            TrackTarget();
        }

        protected override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}