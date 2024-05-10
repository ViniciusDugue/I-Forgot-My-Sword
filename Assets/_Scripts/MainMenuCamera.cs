using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private GameObject mainMenuCamera;
    
    void Awake()
    {
        mainMenuCamera = GameObject.FindWithTag("MainMenuCamera");
        if(mainMenuCamera==null)
        {
            print("mainmenucamera null");
        }
        mainMenuCamera.SetActive(false);
    }

    public void ActivateMainMenuCamera()
    {
       mainMenuCamera.SetActive(true);
    }

    public void DeactivateMainMenuCamera()
    {
        mainMenuCamera = GameObject.FindWithTag("MainMenuCamera");
        mainMenuCamera.SetActive(false);
    }
    void OnDestroy()
    {
        Debug.Log("The mainmenucamear has been destroyed");
    }
}
