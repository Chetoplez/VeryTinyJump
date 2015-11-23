using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    #region InspectorVariables
        [Range(1,50)]
        public float jump_speed = 10f;
        
    #endregion

    Rigidbody2D rigidbody;
    public bool Onground=false;
    private bool already_jumped = false;
    public bool Already_Jumped { set { already_jumped = value; } }
    private Vector3 jump_vector ;

    public static Vector3 Player_Position; /* Accessed by camera behavior */



	void Start () {
	    rigidbody=GetComponent<Rigidbody2D>();
        HandlePlayerInput.Main_player = this;
        jump_vector = this.transform.up;
        GameController.Alive = true;
	}

    void Update() {
        Player_Position = this.transform.position;
    }


    /* Change the gravity scale from planet */
    public void Change_Gravity(bool set=true) {
        this.rigidbody.isKinematic = set;
    }

    /* Flip the player */
    public void Flip(Vector3 center)
    {
        Vector3 lookatvector = this.transform.position - center;
        transform.rotation = Quaternion.LookRotation(Vector3.forward,lookatvector);
    }

    /* Jump! */
    public void Jump() {
        if (!already_jumped)
        {
            if (!CameraBehavior.Player_Moving)
            {
                CameraBehavior.Player_Moving = true;
                GameController.camera_behavior.Can_Move = true;
            }
            
            jump_vector = (Onground)?this.transform.up : Vector3.up * 1.5f;
            if (Onground)
            {
                Change_Gravity(false);
                Onground = false;
            }
            else
                already_jumped = true;

            AddForce(jump_vector * jump_speed);
            
        }
    }

    /* Add force*/
    private void AddForce(Vector3 force){
        this.rigidbody.AddForce(force,ForceMode2D.Impulse);
    }


}
