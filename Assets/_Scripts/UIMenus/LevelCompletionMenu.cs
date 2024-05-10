using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelCompletionMenu : MonoBehaviour
{
    //open up a menu saying "Level 1 Completed!"
    // displays stats like damage taken-, mana used+, gold collected+, close calls(health went below 25?)+
    // , level time completion-, 
    //maybe theres a max amount of points like 50,000 and some stats take away from points while others add to it
    
    //"Upgrades"  opens Upgrade Menu where player chooses random ability/swaps current abilites for next level
    //"Level Summary" button- goes back to level Summary
    //"Start Next Level"- starts next level

    //**future aditions**
    // "Go to Hub" button- takes you to physical hub where you can upgrade, learn info about the game.
    //The hub contains 2 guys one with info on game enemies etc. and another guy who can swap your abilities around
    //
    public bool isOpen = false;
    public GameObject levelCompletionMenu;
    public TextMeshProUGUI scoreboardText;
    public InventoryMenu inventoryMenu;
    public ScoreBoard scoreBoard;
    void Awake()
    {
        levelCompletionMenu.SetActive(false);
    }
    public void Update()
    {
        if(Time.timeScale == 0f)
        {
            print("time set to 0");
        }
        else
        {
            print("time set to 1");
        }
    }
    public void OpenLevelCompletionMenu()
    {
        print("levelmenu opened");
        if(levelCompletionMenu.activeSelf==false)
        {
            scoreBoard.DisplayScoreBoard();
        }
        isOpen = true;
        levelCompletionMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseLevelCompletionMenu()
    {
        print("levelmenu closed");
        isOpen = false;
        levelCompletionMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PressInventoryButton()
    {
        // CloseLevelCompletionMenu();
        inventoryMenu.OpenInventoryMenu();

    }
    public void GoNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            GameManager.Instance.ChangeState(GameState.Level_2);
        }
        CloseLevelCompletionMenu();
    }

    void OnDestroy()
    {
        Debug.Log("The levelcompletionmenu has been destroyed");
    }
}
