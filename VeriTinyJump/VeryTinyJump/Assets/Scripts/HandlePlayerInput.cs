using UnityEngine;
using System.Collections;

public class HandlePlayerInput : MonoBehaviour {

    /* This is the player, is set by  the player himself when start */
    public static Player Main_player = null;

    void Update() { 
        
        if(Main_player!=null)
        {
            if (Jump_input())
                Main_player.Jump();
            if (Pause_Input())
            {
                GameController.Pause = !GameController.Pause;
                Time.timeScale = (GameController.Pause)?0:1;
            }
        }   
    }


    private bool Jump_input() {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private bool Pause_Input() {
        return Input.GetKeyDown(KeyCode.Escape);
    }

}
