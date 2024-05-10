using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
//UseAbility(TextMeshProUGUI abilityCooldownText, Image abilityImageIcon)function: take in mouse position
//if within maxteleportdistance: Findteleportlocation( mouseposition):
//else: if outside of maxteleportdistance: FIndteleportlocation(mouseposition.normalized*maxtelportdistance)
//FindTeleportLocation(position)function: given position, return most suitable teleport location
//Given a ray casted out in the direction of the targetposition, check for a valid teleport location on that ray/line that is as close as possible to the target location

//CheckTeleportCollision()function: check for in tilemap bounds, doesnt collide with tilemap colliders,doesnt collide with 2d colliders

public class TeleportAbility : Ability
{
    public float maxTeleportDistance;

    public void UseAbility(TextMeshProUGUI abilityCooldownText, Image abilityImageIcon)
    {
        // Get the Tilemap component of the dungeon tilemap
        Tilemap dungeonTilemap = GameObject.Find("Tilemap").GetComponentInChildren<Tilemap>();
        if (abilityOnCD)
        {
            Debug.Log($"{abilityName} ability is on cooldown!");
            return;
        }
        if (abilityEquiped == true && playerStats.magika - abilityCost >= 0 && abilityOnCD == false && !PauseMenu.isPaused)
        {
            playerFunctions.UseMagika(abilityCost);

            Vector3 teleportDestination = mousePosition;
            Vector3 teleportDirection = (mousePosition - player.transform.position).normalized;
            float teleportDistance = Vector3.Distance(player.transform.position, mousePosition);
            if (teleportDistance > maxTeleportDistance)
            {
                print("outboundstp");
                print(player.transform.position);
                print(mousePosition);
                teleportDestination = FindTeleportLocation(player.transform.position, player.transform.position + teleportDirection * maxTeleportDistance, dungeonTilemap);
            }
            else
            {
                print("inboundstp");
                print(player.transform.position);
                print(mousePosition);
                teleportDestination = FindTeleportLocation(player.transform.position, mousePosition, dungeonTilemap);
            }
            if(CheckTeleportCollision(teleportDestination, dungeonTilemap))
            {
                player.transform.position = teleportDestination;
            }
            
            print(teleportDestination);
            StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
        }
        else if (!PauseMenu.isPaused)
        {
            Debug.Log("Not enough magika!!");
        }
    }

    private Vector3 FindTeleportLocation(Vector3 startPosition, Vector3 targetPosition, Tilemap dungeonTilemap)
    {
        Vector3 result = targetPosition;
        
        // Find the closest valid teleport location along the line from start to target position
        int numPoints = 8;
        for (int i = 0; i <= numPoints; i++)
        {
            
            // Calculate the t value based on the current iteration
            float t = (float)i / numPoints;
            // Use Vector3.Lerp() to calculate the interpolated position
            Vector3 point = Vector3.Lerp(targetPosition,startPosition, t);
            
            if(CheckTeleportCollision(point, dungeonTilemap))
            {
                print("valid tplocation");
                Vector3 teleportOffsetPoint = point- (point-startPosition).normalized * 0.15f;
                print(point);
                return teleportOffsetPoint;
            }

        }
        return startPosition;
    }

    private bool CheckTeleportCollision(Vector3 teleportPosition, Tilemap dungeonTilemap)
    {
        BoundsInt tilemapBounds = dungeonTilemap.cellBounds;
        dungeonTilemap.CompressBounds();
        if (!dungeonTilemap.HasTile(dungeonTilemap.WorldToCell(teleportPosition)))//- new Vector3(tilemapOrigin.x, tilemapOrigin.y, 0))
        {
            return false;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(teleportPosition, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent<TilemapCollider2D>(out TilemapCollider2D tilemapCollider))
            {
                return false;
            }
            else if (collider is BoxCollider2D || collider is CircleCollider2D)
            {
                return false;
            }
        }
        return true;
    }

}





// private Vector3 FindTeleportLocation(Vector3 startPosition, Vector3 targetPosition, Tilemap dungeonTilemap)
//     {
//         Vector3 result = targetPosition;
//         float distance = Vector3.Distance(startPosition, targetPosition);
//         if (distance > maxTeleportDistance)
//         {
//             // Check the closest teleport cell that is within range
//             Vector3 teleportDirection = (targetPosition - startPosition).normalized;
//             Vector3 teleportDistance = teleportDirection * maxTeleportDistance;
//             Vector3 teleportPosition = startPosition + teleportDistance;
//             result = FindTeleportLocation(startPosition, teleportPosition, dungeonTilemap);
//         }
//         else
//         {
//             // Check if the target position is a valid teleport cell
//             if (!dungeonTilemap.HasTile(dungeonTilemap.WorldToCell(targetPosition)))
//             {
//                 // Search for the closest teleport cell
//                 float minDistance = Mathf.Infinity;
//                 foreach (var position in dungeonTilemap.cellBounds.allPositionsWithin)
//                 {
//                     // Check if the position is a valid teleport cell
//                     if (dungeonTilemap.HasTile(position) && CheckTeleportCollision(position, dungeonTilemap))
//                     {
//                         float d = Vector3.Distance(targetPosition, dungeonTilemap.CellToWorld(position));
//                         if (d < minDistance)
//                         {
//                             result = dungeonTilemap.CellToWorld(position);
//                             minDistance = d;
//                         }
//                     }
//                 }
//             }
//         }
//         return result;
//     }






























// public class TeleportAbility : Ability
// {
//     public float maxTeleportDistance;
//     public void UseAbility(TextMeshProUGUI abilityCooldownText, Image abilityImageIcon)
//     {
//         // Get the Tilemap component of the dungeon tilemap
//         Tilemap dungeonTilemap = GameObject.Find("Tilemap").GetComponentInChildren<Tilemap>();
//         if (abilityOnCD)
//         {
//             Debug.Log($"{abilityName} ability is on cooldown!");
//             return;
//         }
//         if (abilityEquiped == true && playerStats.magika - abilityCost >= 0 && abilityOnCD == false && !PauseMenu.isPaused)
//         {
//             playerStats.magika -= abilityCost;
//             Vector3 teleportDirection = (mousePosition - player.transform.position).normalized;
//             float teleportDistance = Vector3.Distance(player.transform.position, mousePosition);

//             // Get the tilemap boundaries
//             BoundsInt tilemapBounds = dungeonTilemap.cellBounds;
//             Vector3Int tilemapOrigin = dungeonTilemap.origin;

//             // Get the maximum teleport distance within the tilemap boundaries
//             float maxTeleportDistanceWithinTilemap = Mathf.Min(maxTeleportDistance, tilemapBounds.size.x, tilemapBounds.size.y);

//             // Calculate the teleport destination
//             Vector3 teleportDestination = mousePosition;
//             if (teleportDistance > maxTeleportDistanceWithinTilemap)
//             {
//                 teleportDestination = player.transform.position + teleportDirection * maxTeleportDistanceWithinTilemap;
//             }

//             // Check if the teleport destination is within the tilemap boundaries
//             if (dungeonTilemap.HasTile(dungeonTilemap.WorldToCell(teleportDestination )))//- new Vector3(tilemapOrigin.x, tilemapOrigin.y, 0)
//             {
//                 player.transform.position = teleportDestination;
//             }
//             else
//             {
//                 Debug.Log("Teleport destination is outside of the tilemap!");
//             }

//             StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
//         }
//         else if (!PauseMenu.isPaused)
//         {
//             Debug.Log("Not enough magika!!");
//         }
//     }
// }



// public void UseAbility( TextMeshProUGUI abilityCooldownText,Image abilityImageIcon)
//     {
//         if (abilityOnCD)
//         {
//             Debug.Log($"{abilityName} ability is on cooldown!");
//             return;
//         }
//         if (abilityEquiped==true &&playerStats.magika - abilityCost >= 0 && abilityOnCD == false && !PauseMenu.isPaused)
//         {
//             playerStats.magika -= abilityCost;
//             Vector3 teleportDirection = (mousePosition-player.transform.position).normalized;
//             float teleportDistance = Vector3.Distance(player.transform.position, mousePosition);
//             if (teleportDistance >maxTeleportDistance)
//             {
//                 player.transform.position = player.transform.position + teleportDirection *maxTeleportDistance ;
//             }
//             else
//             {
//                 player.transform.position = mousePosition;
//             }
//             StartCoroutine(AbilityCooldownCoroutine(abilityCooldownText, abilityImageIcon));
//         }
//         else if (!PauseMenu.isPaused)
//         {
//             Debug.Log("Not enough magika!!");
//         }
//     }