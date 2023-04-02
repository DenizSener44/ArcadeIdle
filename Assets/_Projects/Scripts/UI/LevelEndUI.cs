using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelEndUI : MonoBehaviour
    {
        public IPlayerDeathController playerDeathController;
        public Action onPlayAgainButtonClicked;
        
        [SerializeField] private Button container;
        

        public void Construct(IPlayerDeathController p)
        {
            playerDeathController = p;
            playerDeathController.OnPlayerDead += LevelLose;
            
            container.onClick.AddListener(() => onPlayAgainButtonClicked?.Invoke());
        }

        private void OnDisable()
        {
            playerDeathController.OnPlayerDead -= LevelLose;
        }

        private void LevelLose()
        {
            container.gameObject.SetActive(true);
        }

        
        
    }
}
