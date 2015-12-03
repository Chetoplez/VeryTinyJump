using UnityEngine;
using System.Collections;
using System;
public class GameController : MonoBehaviour
{

    #region TAGS
        public static string TAG_PLAYER = "Player";
        public static string TAG_PLANET = "Planet";
        public static string TAG_FRUIT = "Fruit";
        public static string TAG_LEVEL_HANDLER = "LevelHandler";
    #endregion

       private const int main_menu_scene=0; /*Number of the first scene */
       private const int first_level = 1;   /* Number of the first level */
       [NonSerialized]
       public int Planet_Number = 0; /* Setted by Level Handler*/

       private bool game_paused = false; /* Game paused? */
       public bool Game_Paused { get { return game_paused; } }
       [NonSerialized]
       public bool Tutorial_Showed = false; /* Already showed the tutorial? */

       [Range(0,1)]
       public int Level = 0; /* Actual level of the game */

    /* Methods for general game : pause, exit, change level... */
    #region GAME_FLOW
        
        public void Begin_Game() {
           Application.LoadLevel(first_level);
        }

        public void Pause_Game(bool pause=true) {
            Time.timeScale = (pause)? 0 : 1;
            game_paused = pause;
        }

        public void Change_Level(int level) {
            if (Game_Paused)
                Pause_Game(false);
            Application.LoadLevel(level);
        }

        public void Back_To_Main_Menu() {
            if (Game_Paused)
                Pause_Game(false);
            Application.LoadLevel(main_menu_scene);
        }

        
        public void Exit_Game() {
            Application.Quit();
        }

    #endregion




    /* Useful methods for all the classes */
    public static bool Is_Player(GameObject other)
    {
        return other.tag == TAG_PLAYER;
    }



}
