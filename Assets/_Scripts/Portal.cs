using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public LevelCompletionMenu levelCompletionMenu;
    private GameObject player;
    // void Update()// for some reason timeScale =0 doesnt work with ontriggerenter2d but it works with update?
    // {
    //     player =GameObject.FindGameObjectWithTag("Player");
    //     levelCompletionMenu  = FindObjectOfType<LevelCompletionMenu>();
    //     if (player.GetComponent<Collider2D>().IsTouching(gameObject.GetComponent<Collider2D>())&&levelCompletionMenu.isLevelCompletionMenuOpen == false)
    //     {
    //         levelCompletionMenu.OpenLevelCompletionMenu();
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        levelCompletionMenu  = FindObjectOfType<LevelCompletionMenu>();
        if (other.CompareTag("Player")&&levelCompletionMenu.isOpen == false)
        {
            levelCompletionMenu.OpenLevelCompletionMenu();
        }
    }

}
