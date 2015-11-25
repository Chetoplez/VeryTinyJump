using UnityEngine;
using System.Collections;

public class HandlePlayerInput : MonoBehaviour {

    /* This is the player, is set by  the player himself when start */
    public static Player Main_player = null;

    /* Seconds between one jump!*/
    [Range(0,5)]
    public float Jump_Delay = 1f;

    private float jump_counter=0;
    private bool can_jump = true;
    public bool Can_jump { get { return can_jump; } }

    void Update() { 
        
        if(Main_player!=null)
        {
            jump_counter -= Time.deltaTime;
            can_jump = (jump_counter <= 0);

            if (Jump_input() && can_jump)
            {
                Main_player.Jump();
                jump_counter = Jump_Delay;
                can_jump = false;
            }
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
