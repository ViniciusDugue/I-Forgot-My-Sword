using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mouse_Cursor : MonoBehaviour
{
    Camera Camera;

    void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            GameObject playerCameraObject = GameObject.FindGameObjectWithTag("PlayerCamera");
            if (playerCameraObject != null && playerCameraObject.activeInHierarchy)
            {
                Camera = playerCameraObject.GetComponent<Camera>();
            }
            else
            {
                Camera = null;
            }
        }
        else
        {
            GameObject mainMenuCameraObject = GameObject.FindGameObjectWithTag("MainMenuCamera");
            if (mainMenuCameraObject != null && mainMenuCameraObject.activeInHierarchy)
            {
                Camera = mainMenuCameraObject.GetComponent<Camera>();
            }
            else
            {
                Camera = null;
            }
        }

        if (Camera != null)
        {
            Vector2 cursorPos = Camera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = cursorPos + new Vector2(0.4f,-0.4f);
        }
    }
}