using System;
using System.Collections;
using System.Collections.Generic;
using InputSystem;
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
        
        

        #endregion


        private void Awake()
        {
            Build();
        }

        private void Build()
        {
            _inputData = joystick;
            
        }
        
        
        
    }

}