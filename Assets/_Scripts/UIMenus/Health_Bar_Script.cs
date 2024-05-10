using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Bar_Script : MonoBehaviour
{
    Slider _healthSlider;
    [SerializeField] private player_controllert player;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
    }
    void Update()
    {
        SetHealth(player.GetComponent<Unit>().health);
    }

    public void SetMaxHealth(float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value  = maxHealth;
    }
    
     public void SetHealth(float health)
    {
        _healthSlider.value  = health;
    }

    
}
