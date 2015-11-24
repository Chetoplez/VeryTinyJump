using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    private bool moving=false;
    private bool can_move = false;
    public bool Can_Move { set { can_move = value; } }
    private Vector3 next_center = Vector3.zero;
    private Vector3 next_position = Vector3.zero;
    private float desired_ortho = GameController.Main_Camera_Orthografic;
    private bool adjusting_frustum = false;

    public static Planet Planet_left=null;
    public static Planet Planet_right = null;


    /* If player is moving, we set this variable.. */
    public static bool Player_Moving = false;
    public float Distance_Threshold = 0.2f;  /* Threshold for stop the camera moving */
    private float orthografic_threshold = 0.125f; /* Threshold for stop the camera increasing/decreasing the ortho size */
    private float zoom_factor = 0.1f; /* increment or decrement this */



    void Start() {
        GameController.camera_behavior = this;
        GameController.Main_Camera_Orthografic = Camera.main.orthographicSize;
    }


	void Update () {
        if (can_move)
        {
            if (!Player_Moving)
            {
                if (Vector3.Distance(this.transform.position, next_center) > Distance_Threshold)
                {
                    next_position = Vector3.Lerp(this.transform.position, next_center, 0.5f);
                    this.transform.position = next_position;
                }
                else
                {
                    if (adjusting_frustum)
                    {

                        Camera.main.orthographicSize += (desired_ortho > Camera.main.orthographicSize) ? zoom_factor : -zoom_factor;
                        if (Mathf.Abs(Camera.main.orthographicSize - desired_ortho) < orthografic_threshold)
                        {
                            can_move = false;
                            adjusting_frustum = false;
                        }
                    }
                }
            }
            else
            {
                next_position = Vector3.Lerp(this.transform.position, Player.Player_Position, 0.3f);
                next_position.z = Camera.main.transform.position.z;
                this.transform.position = next_position;
            }
        }
        
	}

    /* Called By a planet, this tell the camera to move */
    public void Align_Camera_Planet() {
        can_move = LevelHandler.Instance.Get_Next_Center(ref next_center, transform.position);
        if (!can_move)
        {
            Debug.Log("Align Camera Planet : finish!");
            GameController.Finish_Game();
        }
        adjusting_frustum = true;
        Adjust_Frustum();
    }


    /* Resize camera frustum */
    public void Adjust_Frustum() {
        /*Not adjust if we don't have the planets */
        if (Planet_left == null || Planet_right == null) return;

        float planet_distance = Vector3.Distance(Planet_left.transform.position,Planet_right.transform.position);
        Calculate_Frustum(planet_distance);
    }

    /* Calculate the frustum in the game */
    private void Calculate_Frustum(float planet_distance) {
        /* I must consider even the skin_width, not only the centroid */
        planet_distance = planet_distance + (Planet_left.transform.localScale.x / 2);
        desired_ortho = (planet_distance * GameController.Main_Camera_Orthografic) / LevelHandler.Max_Planet_Offset;
        
    }


   


}
