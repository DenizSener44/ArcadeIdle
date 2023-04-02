using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour,ITutorialChanger
{
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text tutorialText;

    [SerializeField] private TutorialChangeData[] tutorialChangeDatas;
    
    public void OpenTutorial(TutorialType type)
    {
        container.SetActive(true);
        foreach (TutorialChangeData data in tutorialChangeDatas)
        {
            if (data.type == type)
            {
                tutorialText.text = data.text;
                break;
            }
        }
    }

    public void CloseTutorial()
    {
        container.SetActive(false);
    }
    
    
    
    
}
