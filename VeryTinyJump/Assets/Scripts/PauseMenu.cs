using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
   
    [Range(0,2)]
    public int level = 0;

    public void Continue() {
        GameController.Pause_Game(false);
    }

    public void Pause() {
        GameController.Pause_Game(true);
    }

    public void Restart() {
        GameController.Reset_Level_Points();
        Application.LoadLevel(level);
        GameController.Pause_Game(false);
    }

    public void Back_To_MainMenu() {
        GameController.Reset_Level_Points();
        Application.LoadLevel(GameController.Main_Menu_Scene);
        GameController.Pause_Game(false);
    }
    
    
}
