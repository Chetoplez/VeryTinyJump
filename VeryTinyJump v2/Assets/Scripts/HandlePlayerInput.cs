using UnityEngine;
using System.Collections;

public class HandlePlayerInput : MonoBehaviour
{

    #region INSPECTOR_VARIABLES
        public Player Main_Player = null;  /* Needed to set the input */
        public GameController Game_Controller = null;  /* needed to pause the game */
    #endregion

    void Start () {
        if (Main_Player == null)
            Debug.LogError("HandlePlayerInput : player is not referred by the inspector." );
        if (Game_Controller == null)
            Debug.LogError("HandlePlayerInput: gamecontroller not attached!");
	}
	
	
	void Update () {
        if (Main_Player != null)
        {
            /* If the tutorial is not showed, we cannot play the game! */
            if (!Game_Controller.Tutorial_Showed)
            {
                if (!Game_Controller.Game_Paused)
                    Game_Controller.Pause_Game();
                if (Input.anyKey)
                {
                    Game_Controller.Tutorial_Showed = true;
                    Game_Controller.Pause_Game(false);
                }
                return;
            }
            if (Jump_Input())
                Main_Player.Input_Received = true;

            if (Pause_Input())
                Game_Controller.Pause_Game(!Game_Controller.Game_Paused);

        }
	}


    /* Received a jump input? */
    bool Jump_Input() {
        return Input.GetKeyDown(KeyCode.Space);
    }

    /* Received a pause input? */
    bool Pause_Input() {
        return Input.GetKeyDown(KeyCode.Escape);
    }



}
