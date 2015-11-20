using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    #region Tags

        public static string Player_tag = "Player";
        public static string Planet_tag = "Planet";
        public static string Fruit_tag = "Fruit";

    #endregion



    public static int Level_points = 0;  /* Points until we die. If restart, this come back to 0 */
    public static int Global_points = 0; /* Global points, updated when finish the level */

    private static GameController instance;
    public static GameController Instance { get { return instance ?? new GameController(); } }

    public static CameraBehavior camera_behavior;
    public static bool Pause = false; /* The game is in pause state */
    public static bool Alive = true; /* The player is alive? */
    public static int Main_Menu_Scene = 0; /* Number of the scene */
    public static float Main_Camera_Orthografic = 5f; /* Default value */

    private void Awake() {
        if (Instance != null) GameObject.Destroy(this.gameObject);
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public static void Reset_Level_Points() {
        Level_points = 0;
    }


    public static bool Is_Player(GameObject other) { return other.tag == Player_tag; }
    public static void Pause_Game(bool pause=true){
        GameController.Pause = pause;
        Time.timeScale = (pause) ? 0 : 1;
    }

}
