using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.EventSystems;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    
    [SerializeField] private MainMenuCamera mainMenuCamera;
    // game manager will eventually contain resources
    private MenuManager menuManager;
    private MainMenu_Script mainMenuScript;
    private player_controllert player;
    private PlayerUI playerUI;
    private PlayerCamera playerCamera;
    private AudioManager audioManager;
    private AbilityDraftMenu abilityDraftMenu;
    private ScoreBoard scoreBoard;
    private AbilityManager abilityManager;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        player = FindObjectOfType<player_controllert>();
        playerUI = FindObjectOfType<PlayerUI>();
        playerCamera = FindObjectOfType<PlayerCamera>();
        menuManager = GameObject.FindObjectOfType<MenuManager>();
        mainMenuScript = menuManager.GetComponent<MainMenu_Script>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        abilityDraftMenu =  menuManager.GetComponent<AbilityDraftMenu>();
        scoreBoard =  menuManager.GetComponent<ScoreBoard>();
        abilityManager =  FindObjectOfType<AbilityManager>();
    }
    private void Update() 
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>(); 
        menuManager = GameObject.FindObjectOfType<MenuManager>();
        abilityDraftMenu =  menuManager.GetComponent<AbilityDraftMenu>();
    }
    void Start()    
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Level_1:
                HandleLevel_1();
                break;
            case GameState.Level_2:
                HandleLevel_2();
                break;
            case GameState.Victory: 
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }
    
    public void HandleMainMenu()
    {
        //if the current scene isnt MainMenu scene, then load into mainmenu
        if (SceneManager.GetActiveScene().name!="Main Menu")
        {
            
            SceneManager.LoadScene("Main Menu");
        }
        
        mainMenuScript.ShowMainMenu();
        // audioManager.StopMainMenuAudio();
        // audioManager.StartMainMenuAudio();
        mainMenuCamera.ActivateMainMenuCamera();
        player.DeactivatePlayer(); //##### THIS WAS THE PROBLEM BECAUSE PLAYER ALREADY DEACTIVATES ON LOAD
        playerCamera.DeactivatePlayerCamera();
        playerUI.HidePlayerUI();
        Debug.Log("Changed to MainMenu State");
    }

    public void HandleLevel_1()
    {
        SceneManager.LoadScene("Level_1");
        //audioManager.StopMainMenuAudio();
        mainMenuCamera.DeactivateMainMenuCamera();
        
        player.ActivatePlayer();
        player.transform.position = new Vector3(-19,0,0);
        playerUI.ShowPlayerUI();
        // abilityManager.AddItem(abilityManager.lockedAbilities.transform.Find("HealAbilityObject").gameObject);
        playerCamera.ActivatePlayerCamera();
        abilityDraftMenu.OpenDraftMenu();
        Debug.Log("Changed to Level_1 State");
        //waits until the Level_1 scene is  loaded and then subscribes to OnSceneLoaded to Spawn Enemies
        SceneManager.sceneLoaded += OnSceneLoaded;
       
    }

    public void HandleLevel_2()
    {
        SceneManager.LoadScene("Level_2");
        
        player.ActivatePlayer();
        playerUI.ShowPlayerUI();
        playerCamera.ActivatePlayerCamera();
        abilityDraftMenu.OpenDraftMenu();
        player.transform.position = new Vector3(-19,0,0);

        player.ResetPlayer();
        Debug.Log("Changed to Level_2 State");
        //waits until the Level_1 scene is  loaded and then subscribes to OnSceneLoaded to Spawn Enemies
        SceneManager.sceneLoaded += OnSceneLoaded;
       
    }
    
    public void HandleVictory()
    {
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level_1")
        {
            Debug.Log("Enemies Spawned");
        }
    }
    void OnDestroy()
    {   // makes sure we unsubscribe to OnsceneLoaded so destroyed unit manager instances dont spawn in more enemies.
        SceneManager.sceneLoaded -= OnSceneLoaded;
        
    }
    void UnloadAllScenes()
    {
        int sceneCount = SceneManager.sceneCount;
        for (int i = 0; i < sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded && scene.name != gameObject.scene.name)
            {
                SceneManager.UnloadSceneAsync(scene);
                Debug.Log("Unloading Scene");
            }
        }
    }

}

public enum GameState
{
    MainMenu,
    Level_1,
    Level_2,
    Victory
}
