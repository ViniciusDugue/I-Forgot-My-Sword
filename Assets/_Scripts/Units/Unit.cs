using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float defense;
    public float attackDamage;
    public float attackSpeed;
    public float chaseRange;
    public float maxChaseRange;
    public float collisionOffset = 0.05f;
    public float magika;
    public float maxMagika;
    public float magikaRegen;
    public int closeCalls;
    public int goldCollected;
    [HideInInspector] public float totalDamageTaken; 
    [HideInInspector] public float totalMagikaUsed;

    public ContactFilter2D movementFilter;
    public List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public Rigidbody2D rb;
    
    // returns true/false if there is Collisions when moving in direction
    public bool checkCollision(Vector2 direction)
    {
        rb =  GetComponent<Rigidbody2D>(); 
        int count = rb.Cast(
            direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
            movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
            castCollisions, // List of collisions to store the found collisions into after the Cast is finished
            speed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
 
        if (count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void MoveUnit(Vector2 moveVector)
    {
        // Check for collisions in the desired movement direction
        if (checkCollision(moveVector.normalized) == true)
        {
            // Try moving along the x-axis only
            Vector2 horizontalMoveVector = new Vector2(moveVector.x, 0).normalized * speed * Time.fixedDeltaTime;
            if (checkCollision(horizontalMoveVector.normalized) == false)
            {
                moveVector = horizontalMoveVector;
            }
            // Try moving along the y-axis only
            else
            {
                Vector2 verticalMoveVector = new Vector2(0, moveVector.y).normalized * speed * Time.fixedDeltaTime;
                if (checkCollision(verticalMoveVector.normalized) == false)
                {
                    moveVector = verticalMoveVector;
                }
                // No available movement direction - don't move
                else
                {
                    moveVector = Vector2.zero;
                }
            }
        }
        // Move the unit in the chosen direction
        rb.MovePosition(rb.position + moveVector);
    }

}