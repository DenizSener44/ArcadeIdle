using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float speed;
        
        private IInputData _inputData;

        private NavMeshHit _hit;


        public void Construct(IInputData inputData)
        {
            _inputData = inputData;
            _inputData.OnInputUpdate += Move;
            _inputData.OnInputReleased += StopMovement;
            _inputData.OnInputStarted += StartMovement;
        }

        private void OnDisable()
        {
            _inputData.OnInputUpdate -= Move;
            _inputData.OnInputReleased -= StopMovement;
            _inputData.OnInputStarted -= StartMovement;
        }

        


        private void Move(Vector2 obj)
        {
            obj.Normalize();
            Vector3 movementAmount = new Vector3(obj.x, 0, obj.y);
            if (NavMesh.SamplePosition(transform.position + movementAmount*speed,out _hit,1f,NavMesh.AllAreas))
            {
                agent.SetDestination(_hit.position);
            }
        }
        
        
        
        private void StopMovement()
        {
            agent.isStopped = true;
        }

        private void StartMovement()
        {
            agent.isStopped = false;
        }
        
        
    }
}
