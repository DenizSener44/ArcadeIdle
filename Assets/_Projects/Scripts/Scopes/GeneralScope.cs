using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Enemy;
using InputSystem;
using Player;
using SideSystems;
using UnityEngine;

namespace Scopes
{
    public class GeneralScope : MonoBehaviour
    {
        #region Entries

        [SerializeField] private Joystick joystick;
        [SerializeField] private VFXManager vfxManager;
        #endregion


        #region Outputs

        private IInputData _inputData;
        private Transform _playerTransform;
        private ISwordOpener _swordOpener;
        #endregion

        #region Subjects

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private EnemyManager enemyManager;

        #endregion


        private void Awake()
        {
            Build();
            Inject();
        }

        

        private void Build()
        {
            _inputData = joystick;
            _playerTransform = playerMovementController.transform;
            _swordOpener = playerController;
        }
        
        private void Inject()
        {
            playerMovementController.Construct(_inputData);
            playerController.Construct(_inputData,vfxManager);
            enemyManager.Construct(_playerTransform,_swordOpener);
        }
        
    }

}