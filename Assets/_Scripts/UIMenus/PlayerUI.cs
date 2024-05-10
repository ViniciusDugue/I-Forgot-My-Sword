using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private GameObject playerUI;
    
    void Awake()
    {
        playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        playerUI.SetActive(false);
    }
    
    public void ShowPlayerUI()
    {
        playerUI.SetActive(true);
    }

    public void HidePlayerUI()
    {
        playerUI.SetActive(false);
    }
    void OnDestroy()
    {
        Debug.Log("The playerui has been destroyed");
    }
}
