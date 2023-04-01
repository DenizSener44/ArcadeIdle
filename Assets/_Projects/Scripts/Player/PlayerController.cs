using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Enemy;
using InteractionSystem;
using SideSystems;
using Tools;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour,ISwordOpener
    {
        public Action OnSwordOpen { get; set; }
        
        private IInputData _inputData;
        
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private AnimationController animationController;
        [SerializeField] private Transform toolHolder;
        [SerializeField] private States[] states;
        [SerializeField] private List<EnemyController> closeEnemies = new List<EnemyController>();
        
        [SerializeField] private float timeBetweenAttacks;
        [SerializeField] private float attackAnimationDuration;
        [SerializeField] private string enemySwordTag = "EnemySword";
        [SerializeField] private string enemyTag = "Enemy";
        [SerializeField] private float sphereCheckRadius = 5;
        [SerializeField] private string enemyLayer;

        private Coroutine _attackingRoutine;
        private bool _canAttack;


        public void Construct(IInputData i)
        {
            _inputData = i;
            _inputData.OnInputStarted += InputStarted;
            _inputData.OnInputReleased += InputReleased;
        }

        private void OnDisable()
        {
            _inputData.OnInputStarted -= InputStarted;
            _inputData.OnInputReleased -= InputReleased;
        }

        private void InputStarted()
        {
            _canAttack = false;
            transform.DOKill();
        }

        private void InputReleased()
        {
            _canAttack = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(enemySwordTag))
            {
                healthSystem.SpendHealth();
                if (healthSystem.GetHealth() <=0)
                {
                    Die();
                }
                return;
            }

            Interactable i = other.GetComponent<Interactable>();
            if (i)
            {
                SetState(i.state);
                return;
            }

            Sword s = other.GetComponent<Sword>();
            if (s)
            {
                if (!s.tool.achieved)
                {
                    s.tool.achieved = true;
                    OnSwordOpen?.Invoke();
                }
                SetState(StateNames.Sword);
                Destroy(s.gameObject);
            }

        }




        private void FixedUpdate()
        {
            Collider[] results = Physics.OverlapSphere(transform.position, sphereCheckRadius, LayerMask.GetMask(enemyLayer));
            foreach (Collider c in results)
            {
                if (!c.CompareTag(enemyTag)) continue;
                EnemyController e = c.GetComponent<EnemyController>();
                if(!e) continue;
                if(closeEnemies.Contains(e)) continue;
                closeEnemies.Add(e);
                if(_attackingRoutine != null) continue;
                SetState(StateNames.Sword);
                _attackingRoutine = StartCoroutine(AttackingRoutine());
            }
        }

        private IEnumerator AttackingRoutine()
        {
            while (closeEnemies.Count > 0)
            {
                if (_canAttack)
                {
                    transform.DOLookAt(closeEnemies[0].transform.position, 0.5f);
                    animationController.Attack();
                    yield return new WaitForSeconds(attackAnimationDuration);
                    bool isEnemyDead = closeEnemies[0].Damage();
                    Debug.Log(isEnemyDead);
                    if (isEnemyDead)
                    {
                        closeEnemies.RemoveAt(0);
                    }
                    
                    yield return new WaitForSeconds(timeBetweenAttacks - attackAnimationDuration);
                }
                yield return null;
            }

            _attackingRoutine = null;
        }


        private void SetState(StateNames sta)
        {
            foreach (States state in states)
            {
                if (state.currentState != sta) continue;
                animationController.SetOverride(state.overrideController);
                CreateTool(state.tool);
                break;
            }    
        }

        private void CreateTool(Tool t)
        {
            if (!t.achieved) return;
            DestroyPreviousTool();
            GameObject o = Instantiate(t.tool, Vector3.zero, t.tool.transform.rotation);
            o.transform.SetParent(toolHolder,false);
        }

        private void DestroyPreviousTool()
        {
            foreach (Transform t in toolHolder.transform)
            {
                Destroy(t.gameObject);
            }
        }
        
        
        
        private void Die()
        {
            
        }
        
    }


}