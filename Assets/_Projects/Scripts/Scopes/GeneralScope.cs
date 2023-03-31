using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
using Player;
using UnityEngine;

namespace Scopes
{
    public class GeneralScope : MonoBehaviour
    {
        #region Entries

        [SerializeField] private Joystick joystick;
        

        #endregion


        #region Outputs

        private IInputData _inputData;

        #endregion

        #region Subjects

        [SerializeField] private PlayerMovementController playerMovementController;

        #endregion


        private void Awake()
        {
            Build();
            Inject();
        }

        

        private void Build()
        {
            _inputData = joystick;
        }
        
        private void Inject()
        {
            playerMovementController.Construct(_inputData);
        }
        
    }

}