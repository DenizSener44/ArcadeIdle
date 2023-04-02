using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using SideSystems;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    
    public class EnemyController : MonoBehaviour
    {
        [HideInInspector] public Transform player;

        [SerializeField] private AnimationController animationController;
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float attackingRange = 1;
        [SerializeField] private float timeBetweenAttacks = 1;
        
        private Coroutine _movingRoutine;
        private Coroutine _attackingRoutine;
        
        
        public void MoveTowardsPlayer()
        {
            _movingRoutine = StartCoroutine(MovingRoutine());
        }

        private IEnumerator MovingRoutine()
        {
            while (Vector3.Distance(transform.position,player.position) >= attackingRange)
            {
                navMeshAgent.SetDestination(player.position);
                yield return new WaitForSeconds(0.1f);
            }

            _movingRoutine = null;
            _attackingRoutine = StartCoroutine(AttackingRoutine());

        }

        private IEnumerator AttackingRoutine()
        {
            while (Vector3.Distance(transform.position,player.position) < attackingRange)
            {
                Attack();
                yield return new WaitForSeconds(timeBetweenAttacks);
            }

            _attackingRoutine = null;
            _movingRoutine = StartCoroutine(MovingRoutine());
        }

        private void Attack()
        {
            animationController.Attack();
        }

        public bool Damage(int amount = 1)
        {
            healthSystem.SpendHealth(amount);
            if (healthSystem.GetHealth() == 0)
            {
                Die();
                return true;
            }
            return false;
        }

        private void Die()
        {
            animationController.Die();
            if (_movingRoutine != null)
            {
                StopCoroutine(_movingRoutine);
                _movingRoutine = null;
            }
            if (_attackingRoutine != null)
            {
                StopCoroutine(_attackingRoutine);
                _attackingRoutine = null;
            }

            GetComponent<Collider>().enabled = false;
            navMeshAgent.isStopped = true;
            enabled = false;
        }
    }

}