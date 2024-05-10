using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class player_controllert : Unit
{ 
    private InputAction ability1Input;
    private InputAction ability2Input;
    private InputAction ability3Input;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI magikaText;
    public Unit playerStats;
    private Vector2 moveInput;
    private GameObject player;
    public Player_input playerControls;
    private InputAction pause;
    private PauseMenu pauseMenu;
    private Camera playerCamera;
    public bool isDead = false;
    private Vector3 mousePosition;
    private AbilityManager abilityManager;
    public SpriteRenderer playerSpriteRenderer;
    private DeathMenu deathMenu;
    
    public int numSpikeTiles = 0;
    public bool playerOnSpike { get { return numSpikeTiles > 0; } }

    public void Awake()
    {
        rb =  GetComponent<Rigidbody2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        deathMenu = FindObjectOfType<DeathMenu>();
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<Unit>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        playerControls =  new Player_input();
        pause = playerControls.Player.Pause;
        abilityManager =  GameObject.FindGameObjectWithTag("AbilityManager").GetComponent<AbilityManager>();
        
        ability1Input = playerControls.Player.Ability1;
        ability2Input = playerControls.Player.Ability2;
        ability3Input = playerControls.Player.Ability3;

        if(pause==null)
            print("player pause is null");
        DeactivatePlayer();
        
        Debug.Log("player var set");
    }

    public void ActivatePlayer()
    {
        if (player == null)
        {
            Debug.LogWarning("Player object is null!");
            return;
        }
        player.SetActive(true);
    }

    

    

    public void DeactivatePlayer()
    {
        player.SetActive(false);
    }

    void Update()
    {
        deathMenu = FindObjectOfType<DeathMenu>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        PassiveMagikaRegeneration();
        healthText.text = Mathf.Round(playerStats.health).ToString() + " / " + playerStats.maxHealth.ToString();
        magikaText.text = Mathf.Round(playerStats.magika).ToString() + " / " + playerStats.maxMagika.ToString();
        if(isDead ==true && deathMenu.isDeathMenuOpen==false)
        {
            print("player died");
            playerSpriteRenderer.enabled = false;
            if(deathMenu ==null)
            {
                print("death menu is null");
            }
            deathMenu.OpenDeathMenu();
        }
    }

    private void OnEnable()
    {
        pause.Enable();
        pause.performed += Pause;
        ability1Input.Enable();
        ability1Input.performed +=UseAbility1;
        ability2Input.Enable();
        ability2Input.performed +=UseAbility2;
        ability3Input.Enable();
        ability3Input.performed +=UseAbility3;
        Debug.Log("Player enabled");
    }

    private void OnDisable()
    {
        pause.Disable();
        pause.performed -= Pause;
        ability1Input.Disable();
        ability1Input.performed -=UseAbility1;
        ability2Input.Disable();
        ability2Input.performed -=UseAbility2;
        ability3Input.Disable();
        ability3Input.performed -=UseAbility3;
        Debug.Log("Player disabled");

    }

    public void Pause(InputAction.CallbackContext context)
    {
        bool isPaused = PauseMenu.isPaused;
        if(context.performed && isPaused ==false&& Time.timeScale ==1f)
        {
            Debug.Log("Pause");
            pauseMenu.PauseGame();
        }
        else if(context.performed && isPaused ==true && Time.timeScale ==0f)
        {
            Debug.Log("UnPause");
            pauseMenu.ResumeGame();
        }
    }

    void FixedUpdate()
    {
        MovePlayer(moveInput);
        
    }

    public void MovePlayer(Vector2 direction)
    {
        Vector2 movementDirection= new Vector2(0,0);
        Vector2 velocityDirection = new Vector2(moveInput.x, moveInput.y);
        if (checkCollision(moveInput)== false)
        {
            movementDirection = moveInput;
        }  
        //try left/right
        else if(checkCollision(new Vector2(moveInput.x, 0))==false)
        {
            movementDirection = new Vector2(moveInput.x, 0);
        }
        //try up/down
        else if(checkCollision(new Vector2(0, moveInput.y))==false)
        {
            movementDirection = new Vector2(0, moveInput.y);
        }
        Vector2 moveVector = movementDirection * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveVector);
        rb.velocity = velocityDirection*speed;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    public void UseAbility1(InputAction.CallbackContext context)
    {
        //Time.timeScale ==1f to check if a menu is open
        if( context.performed &&isDead == false && Time.timeScale==1f)
        {
            print("ability1 used");
            abilityManager.UseAbility1();
        }
    }
    public void UseAbility2(InputAction.CallbackContext context)
    {
        if( context.performed &&isDead == false && Time.timeScale==1f)
        {
            print("ability2 used");
            abilityManager.UseAbility2();
        }
    }
    public void UseAbility3(InputAction.CallbackContext context)
    {
        if( context.performed &&isDead == false && Time.timeScale==1f)
        {
            print("ability3 used");
            abilityManager.UseAbility3();
        }
    }
    public void TakeDamage(float damage)
    {
        if(playerStats.health + playerStats.defense - damage <=0)
        {
            playerStats.totalDamageTaken += (damage - playerStats.defense)-playerStats.health ;
            playerStats.health =0;
            isDead =true;
        }
        else if(playerStats.health + playerStats.defense - damage <20)
        {
            playerStats.closeCalls +=1;
            playerStats.totalDamageTaken += (damage - playerStats.defense);
            playerStats.health-=(damage - playerStats.defense);
        }
        else
        {
            playerStats.totalDamageTaken += (damage - playerStats.defense);
            playerStats.health-=(damage - playerStats.defense);
        }
    }

    public void UseMagika(float abilityCost)
    {
        if(playerStats.magika - abilityCost >=0)
        {
            playerStats.totalMagikaUsed += abilityCost;
            playerStats.magika-=abilityCost;
        }
    }

    public void HealHealth(float healAmount)
    {
        
        if (playerStats.health + healAmount >=playerStats.maxHealth)
           {
                playerStats.health = playerStats.maxHealth;
           }
        else
        {
            playerStats.health += healAmount;
        }
    }

    public void HealMagika(float healMagikaAmount)
    {
        if (playerStats.magika + healMagikaAmount >=playerStats.maxMagika)
           {
                playerStats.magika = playerStats.maxMagika;
           }
        else
        {
            playerStats.magika += healMagikaAmount;
        }
    }

    public void PassiveMagikaRegeneration()
    {
        
        if(rb.velocity.magnitude!=0f)
        {
            if (playerStats.magika + magikaRegen >=playerStats.maxMagika)
            {
                playerStats.magika = playerStats.maxMagika;
            }
            else
            {
                playerStats.magika += magikaRegen;
            }
        }
    }
    public void ResetPlayer()
    {
        isDead=false;
        playerStats.health = playerStats.maxHealth;
        playerStats.magika = playerStats.maxMagika;
        playerStats.totalDamageTaken = 0;
        playerStats.totalMagikaUsed = 0;
        // abilityManager.SetEquipedAbilites();
    }

    public void IncrementSpikeTileCount() {
        numSpikeTiles++;
    }

    public void DecrementSpikeTileCount() {
        numSpikeTiles--;
        if (numSpikeTiles < 0) {
            Debug.LogError("numSpikeTiles should never be negative");
            numSpikeTiles = 0;
        }
    }
}