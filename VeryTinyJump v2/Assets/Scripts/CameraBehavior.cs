using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour
{

    #region INSPECTOR_VARIABLES
        public LevelHandler LevelHandler;           /* Need the reference of LevelHandler, for the positioning between planets */
        public Player MainPlayer;                   /* Need the player reference, for following him */
        [Range(1,10)]
        public float CameraZoomSensitivity = 1f;    /* Used when zoom-in/out */
        [Range(1, 20)]
        public float MaxOrthographicsSize = 10f;    /* Used in calculation of desired ortho*/
        [Range(1,10)]
        public float CameraSpeed = 2f;              /* Used when the camera moves*/
        [Range(0,1)]
        public float CameraDistanceThreshold = 0.5f; /* Distance threshold from centroid */
        [Range(0, 0.5f)]
        public float CameraZoomThreshold = 0.0125f; /* Threshold with the zooming */
    #endregion


    private bool adjusting_frustum = false;        /* If true, the camera must zoom in/out */
    private bool centroid_calculated = false;      /* If false, we need to calculate next centroid*/
    private Vector3 next_centroid = Vector3.zero;  /* Next center between planets*/
    private Vector3 next_position = Vector3.zero;  /* Next position of the camera */
    private float desired_orthographic_size = 0f;  /* The desired ortho, when calculate the new centroid*/
    private float camera_orthographic;             /* Starting camera_orthographic */
    

    void Start () {
        if (LevelHandler == null)
            Debug.LogError("CameraBehavior: levelhandler not assigned! Please assign the reference");
        if (MainPlayer == null)
            Debug.LogError("CameraBehavior: player is not assigned! Please assign the reference");

        camera_orthographic = Camera.main.orthographicSize;
	}


    void FixedUpdate()
    {
       

        /* Follow the player */
        if (!MainPlayer.OnGround)
        {
            Move_Camera(MainPlayer.transform.position);
            /* Needed for start again the centroid calculation */
            if (centroid_calculated)
                centroid_calculated=false;

            if (adjusting_frustum)
                AdjustFrustum();
        }
        else /* ...or Go to the next centroid */
        {
            if (!Check_End_Movement())
            {
                /* If the centroid is not calculate, calculate the new one */
                if(!centroid_calculated)
                    Calculate_Centroid();
                if (Vector3.Distance(this.transform.position, next_centroid) > CameraDistanceThreshold)
                    Move_Camera(next_centroid);
               
            }
            else
            {
                MainPlayer.Alive = false;
                Debug.Log("End game!");
            }
        }
    }

    /* If we reach the end of the list, the camera cannot move and calculate a new center */
    bool Check_End_Movement() {
        return (MainPlayer.Planet_Index == (LevelHandler.Planet_List.Count -1));
    }

    /* Calculate the centroid between planets */
    void Calculate_Centroid() {
      Vector3 planet_1 = LevelHandler.Planet_List[MainPlayer.Planet_Index].gameObject.transform.position;
      Vector3 planet_2 = LevelHandler.Planet_List[MainPlayer.Planet_Index + 1].gameObject.transform.position;
      next_centroid =(planet_1 + planet_2) / 2;
      centroid_calculated = true;
      adjusting_frustum = true;
      Calculate_Desired_Frustum(LevelHandler.Planet_List[MainPlayer.Planet_Index], LevelHandler.Planet_List[MainPlayer.Planet_Index + 1]);  
    }

    /* Moves the camera in the direction passed */
    void Move_Camera(Vector3 direction) {
        next_position = Vector3.Lerp(this.transform.position, direction, Time.deltaTime * CameraSpeed);
        next_position.z = Camera.main.transform.position.z;
        this.transform.position = next_position;
    }

    /* Adjust camera frustum */
    void AdjustFrustum() {
        Camera.main.orthographicSize +=( (desired_orthographic_size> Camera.main.orthographicSize)? CameraZoomSensitivity : - CameraZoomSensitivity)* Time.deltaTime;
        if (Mathf.Abs(Camera.main.orthographicSize - desired_orthographic_size) < CameraZoomThreshold)
            adjusting_frustum = false;
    }

    /* Calculate the desired adjust frustum based on the planet distances */
    void Calculate_Desired_Frustum(Planet planet_1,Planet planet_2) {
        
        float planet_distance = Vector3.Distance(planet_1.transform.position,planet_2.transform.position);
        planet_distance += (planet_1.SpriteRender.bounds.size.x/2 ); /* need to consider the skin_width of the planets */
        planet_distance += (planet_2.SpriteRender.bounds.size.x/2);
        
        desired_orthographic_size = (planet_distance * camera_orthographic) / (MaxOrthographicsSize);
    }

}
