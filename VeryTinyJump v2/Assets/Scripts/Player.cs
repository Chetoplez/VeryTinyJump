using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{

    #region INSPECTOR_VARIABLES
        [Range(1, 20)]
        public float JumpSpeed = 5f;  /* Jump Speed of the player */
        [Range(0,5)]
        public float JumpDelay = 0.5f; /* Time between one jump and another */

    #endregion

    private Rigidbody2D rigidbody; /* Rigidbody2d of the player */

    private bool input_received = false; /* Setted true by HandlePlayerInput */
    public bool Input_Received { set { input_received = value; } }

    private bool already_jumped = false; /* If true the character cannot jump until he is grounded */

    [NonSerialized]
    public bool OnGround = false;  /*Setted true by the planet */
    [NonSerialized]
    public int Planet_Index = 0; /* Setted by the planet when the player remains stucked */
    [NonSerialized]
    public bool Alive = true; /* Setted false by death area */


    private bool can_jump = true;
    public bool Can_Jump { get { return can_jump; } }


    private float jump_time_counter;
    private Vector3 jump_vector = Vector3.zero;

    void Start() { 
       rigidbody= GetComponent<Rigidbody2D>();
       jump_time_counter = JumpDelay;
       Alive = true;
    }
	
	void FixedUpdate () {
        
        /* Cannot jump if the counter does not expired */
        if(jump_time_counter>0)
            jump_time_counter -= Time.deltaTime;

        can_jump= (jump_time_counter <=0);
        if (input_received)
        {
            if (can_jump && !already_jumped)
               Jump();
        }

        /* When on air, it must look at his up vector, just like an arrow */
        if (!OnGround)
        {
            Vector3 lookatvector = rigidbody.velocity;
            lookatvector = Vector3.Lerp(lookatvector, this.transform.up, Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, lookatvector);
        }

	}


    /* Add the force to the player, if input has been received */
    void AddForce(Vector3 force) {
        rigidbody.AddForce(force,ForceMode2D.Impulse);
    }

    /* If we can, jump! */
    void Jump() {
       
        input_received = false;

        jump_time_counter = JumpDelay;  /* Reset the counter */
        jump_vector = (OnGround) ? this.transform.up : Vector3.up; /* If we are on ground, take the up component */

        /* The second jump must have a x force compoenent! */
        if (!OnGround)
            jump_vector.x = (transform.localScale.x > 0) ? 0.5f : -0.5f;

        /* Must detach the parent relationship... */
        if (!OnGround)
            already_jumped = true;
        else
        {
            OnGround = false;
            this.transform.parent = null;
            Change_Gravity(false);
        }
        
        

        AddForce(jump_vector * JumpSpeed);
    }


    /* Called by the planet initially, in order to block the player. Then called by the player himself */
    public void Change_Gravity(bool set = true)
    {
        this.rigidbody.isKinematic = set;
    }


    /* Flip makes the player turn inside out of the planet. Center is the position of the planet */
    public void Flip(Vector3 center) {
        Vector3 lookatvector = this.transform.position - center;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookatvector);
    }

    /* Called by a planet, makes the player stucked in the planet */
    public void Block(GameObject other,int index=0) {
        Change_Gravity();
        transform.parent = other.transform; /* In this way the player will take the same rotation of planet */
        already_jumped = false;
        Planet_Index = index; /* Need for the camera! */
        OnGround = true;
        Flip(other.transform.position);
    }


}
