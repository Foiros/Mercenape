using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lootDrop : MonoBehaviour
{
    //Made by Ossi Uusitalo
    // A rework on the loot item which has all the relevant elements of it in a single script. I like to keep things compact and NOT go through editor to find a separate script that carries out a single function.
    public int health = 0,upgrade = 0, karma = 0, money = 0;

    //This script simply stores the values for the player to pick up. I could add the collision scripts for pickup, but the Player Collision detection script has that covered, so there is no need to re-do that.

}
