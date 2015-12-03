using UnityEngine;
using System.Collections;
using System;
[RequireComponent(typeof(Collider2D)),RequireComponent(typeof(SpriteRenderer))]
public class Planet : MonoBehaviour {


    #region InspectorVariables
        [Range(1, 50)]
        public float Rotation_Speed = 10f; /* Rotation of the planet */
        public bool Clockwise = false;     /* Clockwise?  */
        public SpriteRenderer SpriteRender; /* Needed for camera distance calculation */
    #endregion

    private bool player_blocked = false;  /* Setted true if the player is stuked in the planet */
    private Player player = null;         /* This will be setted when the player will hit the planet */

    [NonSerialized]
    public Planet previous_planet = null;  /* Needed for correct positioning in the scene */
    
    public int list_index=0;             /* Used for adressing the planet in the common planet list */


    #region MohobehaviorMethods


    void Update () {
        Rotate();
	}


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameController.Is_Player(collision.gameObject))
            player = collision.gameObject.GetComponent<Player>();

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (GameController.Is_Player(collision.gameObject))
        {
            if (player != null)
            {
                if (!player_blocked)
                    Block_Player();
            }
            else
                player = collision.gameObject.GetComponent<Player>();
        }

    }


    void OnCollisionExit2D(Collision2D collision)
    {
        if (GameController.Is_Player(collision.gameObject))
            player = null;
    }

    #endregion


    /* Rotate the planet */
    void Rotate()
    {
        float current_angle = transform.rotation.eulerAngles.z;
        transform.rotation = Quaternion.AngleAxis((current_angle + (Time.deltaTime * Rotation_Speed * ((Clockwise) ? -1 : 1))), Vector3.forward);
    }

    /* Block the player! */
    void Block_Player() {
        player_blocked = true;
        player.Block(this.gameObject,this.list_index);
        LevelHandler.Progress = this.list_index ;
    }

}
