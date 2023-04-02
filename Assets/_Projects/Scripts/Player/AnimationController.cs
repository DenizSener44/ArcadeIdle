using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private string speedAnimationName = "Speed";
        [SerializeField] private string attackAnimationName = "Attack";
        [SerializeField] private string deathAnimationName = "Death";
        
        private int _speedHash;
        private int _attackHash;
        private int _deathHash;
        private void Start()
        {
            _speedHash = Animator.StringToHash(speedAnimationName);
            _attackHash = Animator.StringToHash(attackAnimationName);
            _deathHash = Animator.StringToHash(deathAnimationName);
        }

        private void Update()
        {
            animator.SetFloat(_speedHash,agent.velocity.magnitude);
        }


        public void Attack()
        {
            animator.SetTrigger(_attackHash);
        }

        public void SetOverride(AnimatorOverrideController animatorOverrideController)
        {
            animator.runtimeAnimatorController = animatorOverrideController;
        }

        public void Die()
        {
            animator.SetTrigger(_deathHash);
        }
    }
}
