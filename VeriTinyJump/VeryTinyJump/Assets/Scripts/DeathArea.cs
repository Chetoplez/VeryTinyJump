using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BoxCollider2D))]
public class DeathArea : MonoBehaviour {

    Player player;
    Vector3 vector_player=Vector3.zero;

    void Start() {
        player = HandlePlayerInput.Main_player;
        vector_player = this.transform.position;
    }

    void Update() {
        vector_player.x = player.transform.position.x;
        this.transform.position = vector_player;
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (GameController.Is_Player(collider.gameObject))
        {
            GameController.Alive = false;
            GameController.Pause_Game();
        }
    }

}
