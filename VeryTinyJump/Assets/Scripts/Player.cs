using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    #region InspectorVariables
        [Range(1,50)]
        public float Jump_speed = 10f;
        [Range(1,20)]
        public float Max_Velocity=10f;    

    #endregion

    Rigidbody2D rigidbody;
    [NonSerialized]
    public bool Onground=false;

    private bool already_jumped = false;
    public bool Already_Jumped { set { already_jumped = value; } }
    private Vector3 jump_vector ;
    
    private bool jump_input;
    public bool Jump_input { set { jump_input = value; } }



    public static Vector3 Player_Position; /* Accessed by camera behavior */



	void Start () {
	    rigidbody=GetComponent<Rigidbody2D>();
        HandlePlayerInput.Main_player = this;
        jump_vector = this.transform.up;
        GameController.Alive = true;
	}

    void FixedUpdate() {
        Player_Position = this.transform.position;

        Debug.Log("Player velocity = " + rigidbody.velocity);

        if (!Onground)
        {
            Vector3 lookatvector = rigidbody.velocity;
            lookatvector = Vector3.Lerp(lookatvector,this.transform.up,0.2f);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, lookatvector);
            /* Limit the velocity */
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, Max_Velocity);
        }

        if (jump_input)
        {
            Jump();
            jump_input = false;
        }
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
    private void Jump() {
        if (!already_jumped)
        {
            if (!CameraBehavior.Player_Moving)
            {
                CameraBehavior.Player_Moving = true;
                GameController.camera_behavior.Can_Move = true;
            }
            
            jump_vector = (Onground)?this.transform.up : Vector3.up*1.2f;
            jump_vector.x = (!Onground)?0.5f:jump_vector.x;
           
            
            if (Onground)
            {
                rigidbody.velocity = Vector2.zero;
                rigidbody.angularVelocity = 0f;
                Change_Gravity(false);
                Onground = false;
            }
            else
            { 
                already_jumped = true;
                Hud.Alpha_Jump_Button_Changed = true;
            }
            
            AddForce(jump_vector * Jump_speed);
            
        }
    }

    /* Add force*/
    private void AddForce(Vector3 force){
         this.rigidbody.AddForce(force,ForceMode2D.Impulse);
    }


}
