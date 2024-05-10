using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Basic_Enemy : Unit
{
    // public float chaseRange = 10f;
    // public float maxChaseRange = 2f;
    private Transform target;
    private float minAmbientRange = 2.5f;
    private Vector3 randomDirection;
    private Vector3 startingPosition;
    private Vector2 movementDirection;
    private GameObject player;

    public void Awake()
    {
        startingPosition = transform.position;
        randomDirection = Random.insideUnitCircle.normalized;
        player = GameObject.FindWithTag("Player");
        target = player.transform;
        // DontDestroyOnLoad(gameObject);
    }
    
    public void Update()
    {
        movementDirection = new Vector2(target.position.x -  transform.position.x, target.position.y -  transform.position.y );
        //Chase player if in chase range but no further than maxChaseRange and no Collisions
        //when I teleport, the distance gets added in the z diecpermanently to my position for some reason
        // print(target.position); 
       
        if (Vector3.Distance(transform.position, target.position) <= chaseRange && Vector3.Distance(transform.position, target.position)>= maxChaseRange  && checkCollision(movementDirection)==false )
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            startingPosition = transform.position;
        }
        //ambient movement
        else if(Vector3.Distance(transform.position, target.position)>= maxChaseRange)
        { 
            // Calculate the distance from the starting position
            float distanceFromStart = Vector3.Distance(transform.position, startingPosition);

            // Check if the unit is beyond the maximum range
            if (distanceFromStart > minAmbientRange)
            {
                // Move the unit back towards the starting position
                Vector3 moveDirection = (startingPosition - transform.position).normalized;
                MoveUnit(moveDirection * Time.deltaTime* speed*4);
            }
            else 
            {
                // Move the unit in a semi-random direction
                MoveUnit(randomDirection * Time.deltaTime* speed*4);
                // transform.position += randomDirection * Time.deltaTime * speed/2;
        
            }   
            // If the unit has been moving in the same direction for too long, generate a new random direction
            if (Random.Range(0f, 1f) < 0.05f)
            {
                randomDirection = Random.insideUnitCircle.normalized;
            }
        }
    }
    

}
// public void Update()
//     {
//         movementDirection = new Vector2(target.position.x -  transform.position.x, target.position.y -  transform.position.y );
//         //Chase player if in chase range but no further than maxChaseRange and no Collisions
//         //when I teleport, the distance gets added in the z diecpermanently to my position for some reason
//         // print(target.position); 
       
//         if (Vector3.Distance(transform.position, target.position) <= chaseRange && Vector3.Distance(transform.position, target.position)>= maxChaseRange  && checkCollision(movementDirection)==false )
//         {
//             transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
//             startingPosition = transform.position;
//         }
//         //ambient movement
//         else if(Vector3.Distance(transform.position, target.position)>= maxChaseRange)
//         { 
//             // Calculate the distance from the starting position
//             float distanceFromStart = Vector3.Distance(transform.position, startingPosition);

//             // Check if the unit is beyond the maximum range
//             if (distanceFromStart > minAmbientRange)
//             {
//                 // Move the unit back towards the starting position
//                 Vector3 moveDirection = (startingPosition - transform.position).normalized;
//                 transform.position += moveDirection * Time.deltaTime * speed/2;
//             }
//             else 
//             {
//                 // Move the unit in a semi-random direction
//                 transform.position += randomDirection * Time.deltaTime * speed/2;
        
//             }   
//             // If the unit has been moving in the same direction for too long, generate a new random direction
//             if (Random.Range(0f, 1f) < 0.05f)
//             {
//                 randomDirection = Random.insideUnitCircle.normalized;
//             }
//         }
//     }
