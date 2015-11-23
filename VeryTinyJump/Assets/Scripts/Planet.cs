using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Planet : MonoBehaviour
{

    #region InspectoriVariables
        [Range(1,50)]
        public float rotation_speed = 10f;
        public bool Clockwise = false;
        public int id = 0;
    #endregion


    private Player player = null;
    private bool player_blocked = false;
    private SpriteRenderer renderer;
    public SpriteRenderer Renderer { get { return renderer; } }
    /* Needed for correct positioning in the scene */
    public Planet previous_planet = null;



    #region MonoBehaviorStates

    void Start() { 
        renderer=GetComponent<SpriteRenderer>();
    }

    void Update() {
        Rotate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameController.Is_Player(collision.gameObject))
            player=collision.gameObject.GetComponent<Player>();
        
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (GameController.Is_Player(collision.gameObject))
        {
            if (player != null)
            {
                if (!player_blocked)
                    BlockPlayer();
            }
            else
                player= collision.gameObject.GetComponent<Player>();
        }
       
    }


    void OnCollisionExit2D(Collision2D collision) {
        if (GameController.Is_Player(collision.gameObject))
            player = null;
    }

    #endregion


    /* Rotate the planet */
    void Rotate() {
        float current_angle = transform.rotation.eulerAngles.z;
        transform.rotation = Quaternion.AngleAxis((current_angle + (Time.deltaTime * rotation_speed * ((Clockwise) ? -1 : 1))), Vector3.forward);
    }

	
    /* BlockPlayer */
    void BlockPlayer() {
        /* Must tell to the planet_index our id, so the camera can figure out where we are */
        LevelHandler.Actual_Planet_Index = this.id;
        GameController.camera_behavior.Align_Camera_Planet();
        CameraBehavior.Player_Moving = false;

        float advance_size =LevelHandler.Planet_number;
        GameController.hud.Advance_Progress_Bar_Level(advance_size);

        player.transform.parent = this.gameObject.transform;
        player_blocked = true;
        player.Change_Gravity(true);
        player.Already_Jumped = false;
        player.Onground = true;
        player.Flip(this.transform.position);
    }


}
