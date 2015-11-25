using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class ShakeHandler : MonoBehaviour {

    private bool deactivated = false;
    void OnTriggerStay2D(Collider2D other) {
        if(GameController.Is_Player(other.gameObject))
        {
            if (!deactivated)
            {
                other.transform.parent = null;
                Rigidbody2D rigidbody_player = other.gameObject.GetComponent<Rigidbody2D>();
                rigidbody_player.isKinematic = true;
                GameController.Alive = false;
                GameController.Pause_Game();
            }
        }
    }
	
}
