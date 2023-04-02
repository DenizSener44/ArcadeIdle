using System;
using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace SideSystems
{
    public class TutorialController : MonoBehaviour
    {
        public Sword sword;
        
        private ICameraChanger _cameraChanger;
        private ITutorialChanger _tutorialChanger;
        private IPlayerStackCounter _stackCounter;
        private IPlayerKillCounter _playerKillCounter;


        public void Construct(ICameraChanger cc, ITutorialChanger tc,IPlayerStackCounter psc,IPlayerKillCounter pkc)
        {
            _cameraChanger = cc;
            _tutorialChanger = tc;
            _stackCounter = psc;
            _playerKillCounter = pkc;
        }
        
        private void Start()
        {
            StartCoroutine(TutorialRoutine());
        }

        private IEnumerator TutorialRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            
            _cameraChanger.OpenCam(CameraType.Stone);
            _tutorialChanger.OpenTutorial(TutorialType.Stone);
            yield return new WaitForSeconds(2f);
            _tutorialChanger.CloseTutorial();
            _cameraChanger.CloseCam();
            yield return new WaitUntil(() => _stackCounter.StoneCount == 3);

            _cameraChanger.OpenCam(CameraType.Wood);
            _tutorialChanger.OpenTutorial(TutorialType.Wood);
            yield return new WaitForSeconds(2f);
            _tutorialChanger.CloseTutorial();
            _cameraChanger.CloseCam();
            yield return new WaitUntil(() => _stackCounter.WoodCount == 2);
            
            _cameraChanger.OpenCam(CameraType.Sword);
            _tutorialChanger.OpenTutorial(TutorialType.Sword);
            yield return new WaitForSeconds(2f);
            _tutorialChanger.CloseTutorial();
            _cameraChanger.CloseCam();
            yield return new WaitUntil(() => sword.tool.achieved);
            
            _cameraChanger.OpenCam(CameraType.Enemy);
            _tutorialChanger.OpenTutorial(TutorialType.Enemy);
            yield return new WaitForSeconds(2f);
            _tutorialChanger.CloseTutorial();
            _cameraChanger.CloseCam();
            yield return new WaitUntil(() => _playerKillCounter.PlayerKilledEnemy);
            
            _cameraChanger.OpenCam(CameraType.Broom);
            _tutorialChanger.OpenTutorial(TutorialType.Broom);
            yield return new WaitForSeconds(2f);
            _tutorialChanger.CloseTutorial();
            _cameraChanger.CloseCam();
            
            
        }
    }

}