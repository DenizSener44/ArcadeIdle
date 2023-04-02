using System;
using System.Collections;
using System.Collections.Generic;
using Buildings;
using Enemy;
using InputSystem;
using InteractionSystem;
using Player;
using SideSystems;
using UI;
using UnityEngine;

namespace Scopes
{
    public class GeneralScope : MonoBehaviour
    {
        #region Entries

        [SerializeField] private Joystick joystick;
        [SerializeField] private VFXManager vfxManager;
        [SerializeField] private CameraManager cameraManager;
        #endregion


        #region Outputs

        private IInputData _inputData;
        private Transform _playerTransform;
        private ISwordOpener _swordOpener;
        private IPlayerDeathController _playerDeathController;
        private ICameraShaker _cameraShaker;
        #endregion

        #region Subjects

        [SerializeField] private PlayerMovementController playerMovementController;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private PlayerInteractionController playerInteractionController;
        [SerializeField] private PlayerStackController playerStackController;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private LevelEndUI levelEndUI;
        [SerializeField] private Interactable stoneMine;
        [SerializeField] private Interactable tree;

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
            _playerDeathController = playerController;
            _cameraShaker = cameraManager;
        }
        
        private void Inject()
        {
            playerMovementController.Construct(_inputData);
            playerController.Construct(_inputData,vfxManager,_cameraShaker);
            playerInteractionController.cameraShaker = _cameraShaker;
            playerStackController.cameraShaker = _cameraShaker;
            enemyManager.Construct(_playerTransform,_swordOpener);
            levelEndUI.Construct(_playerDeathController);
            tree.vfxManager = vfxManager;
            stoneMine.vfxManager = vfxManager;
        }
        
    }

}