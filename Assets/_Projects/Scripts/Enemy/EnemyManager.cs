using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyManager : MonoBehaviour
    {
        private Transform _player;
        private ISwordOpener _swordOpener;

        [SerializeField] private EnemyController enemyPrefab;
        
        [SerializeField] private float timeToInstantiateNewEnemy;
        [SerializeField] private float minInstantiateRange = 20;
        [SerializeField] private float maxInstantiateRange = 30;
        
        private Coroutine _enemyCreationRoutine;

        public void Construct(Transform player,ISwordOpener swordOpener)
        {
            _player = player;
            _swordOpener = swordOpener;
            _swordOpener.OnSwordOpen += StartEnemyCreationRoutine;
        }

        private void OnDisable()
        {
            if(_swordOpener == null) return;
            _swordOpener.OnSwordOpen -= StartEnemyCreationRoutine;
        }


        private void StartEnemyCreationRoutine()
        {
            _enemyCreationRoutine = StartCoroutine(EnemyCreationRoutine());
            _swordOpener.OnSwordOpen -= StartEnemyCreationRoutine;
            _swordOpener = null;
        }

        private IEnumerator EnemyCreationRoutine()
        {
            while (true)
            {
                CreateEnemy();
                yield return new WaitForSeconds(timeToInstantiateNewEnemy);
            }
        }

        private void CreateEnemy()
        {
            Vector3 sphere = Random.insideUnitSphere;
            sphere.Normalize();
            sphere *= Random.Range(minInstantiateRange,maxInstantiateRange);
            sphere.y = 0;
            EnemyController e = Instantiate(enemyPrefab, sphere, Quaternion.identity);
            e.player = _player;
            e.MoveTowardsPlayer();
        }
    }

}