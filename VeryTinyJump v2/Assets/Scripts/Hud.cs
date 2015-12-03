using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour
{

    #region INSPECTOR_VARIABLES
        public GameController GameController; /* GameController script. Needed for know the game status */ 
        public Player MainPlayer; /*  Main Player. Needed for knowing his state */
        public GameObject PausePanel;  /* Pause Panel. Showed on ESC button press */
        public GameObject ScorePanel;  /* Score Panel. Showed when died on game ended. */
        public UnityEngine.UI.Image JumpImage; /* Help to know if we can jump or not */
        public UnityEngine.UI.Text Seconds_text; /*Text for timing*/
        public UnityEngine.UI.Text Minutes_text;
        public UnityEngine.UI.Text Points; /* Point text displayed */
        public UnityEngine.UI.Text FinalScorePoints; /* Points displayed in the score panel */
        public UnityEngine.UI.Text Combos; /* Combo Points displayed in the score panel */
        public UnityEngine.UI.Scrollbar ProgressBar; /* ProgressBar of the game */
        public GameObject Tutorial; /* Image of the tutorial */
    #endregion
        /* Needed to change to color of the jump image during the game */
        private Color changed_color = new Color(1f, 1f, 1f, 0.5f);
        private Color initial_color;

        /* Just some timing... */
        private float seconds = 0;
        private int minutes = 0;


    /* Just some check...*/
    void Start () {
        if (GameController == null)
            Debug.LogError("HUD: please attach the gamecontrller script");
        if (MainPlayer == null)
            Debug.LogError("HUD: please attach the main player script");
        if (PausePanel==null)
            Debug.LogError("HUD: please attach the PausePanel gameobject");
        if (ScorePanel==null)
            Debug.LogError("HUD: please attach the ScorePanel gameobject");
        if (Points == null)
            Debug.LogError("Hud: please attach the points gameobject");
        if (FinalScorePoints == null)
            Debug.LogError("Hud: please attach the final score gameobject");
        
        if (JumpImage == null)
            Debug.LogError("HuD : please attach the Jumpbutton gameobject");
        else
            initial_color = JumpImage.color;
	}
	
	
	void Update () {
        PausePanel.SetActive(GameController.Game_Paused && MainPlayer.Alive);
        ScorePanel.SetActive(!MainPlayer.Alive);
        JumpImage.color = (!MainPlayer.Can_Jump) ? changed_color : initial_color;

        Tutorial.SetActive(!GameController.Tutorial_Showed);
        Seconds_text.text = seconds.ToString();
        Minutes_text.text = minutes.ToString();
        Points.text = LevelHandler.Level_Points.ToString();
        FinalScorePoints.text = Points.text;
        Combos.text = LevelHandler.Combos.ToString();

        ProgressBar.value = (LevelHandler.Progress / GameController.Planet_Number );

        /* Need this check if the player is alive. Death area doesn't have the reference to gamecontroller */
        if (!GameController.Game_Paused && !MainPlayer.Alive)
            GameController.Pause_Game();



        Advance_Time();
	}

    /* Tic tac tic tac... */
    void Advance_Time() {
        seconds += Time.deltaTime;
        if (seconds > 60)
        {
            seconds = 0;
            minutes++;
        }
    }

    public void Pause_Game() {
        if (GameController != null)
            GameController.Pause_Game();
    }

    public void Resume_Game() {
        if (GameController != null)
            GameController.Pause_Game(false);
    }

    public void Restart() {
        if (GameController != null)
        {
            MainPlayer.Alive = true;
            GameController.Pause_Game(false);
            GameController.Change_Level(GameController.Level);
        }
    }

    public void Main_Menu() {
        if (GameController != null)
            GameController.Back_To_Main_Menu();
    }

    public void Exit_Game() {
        if (GameController != null)
            GameController.Exit_Game();
    }
}
