using UnityEngine;
using System.Collections;

/* When the player enter in this area, he dies */
[RequireComponent(typeof(Collider2D))]
public class DeathArea : MonoBehaviour
{
    private Player Main_Player;
    private bool player_killed=false;

    public void OnTriggerEnter2D(Collider2D other) {
        if (GameController.Is_Player(other.gameObject) && !player_killed)
        {
            Main_Player = other.gameObject.GetComponent<Player>();
            Main_Player.Alive = false;
            player_killed = true;
        }
    }
	
}
