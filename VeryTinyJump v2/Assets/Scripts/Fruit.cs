using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Fruit : MonoBehaviour
{

    #region INSPECTOR_VARIABLES
        [Range(5,50)]
        public int Points = 10;                  /* Points value! */
        public SpriteRenderer Sprite_Renderer; /* Must disappear after gave the points */
        public AudioSource Point_Audio;    /* Sounds of the points */
    #endregion

        private bool points_gived = false; /* Already gave the points to the player? */
        public bool Points_Gived { get { return points_gived; } }

    void Start () {
	    if(Sprite_Renderer==null)
            Sprite_Renderer=GetComponent<SpriteRenderer>();
	}

    void OnTriggerStay2D(Collider2D other) {
        if (GameController.Is_Player(other.gameObject))
        {
            if (!points_gived)
            {
                LevelHandler.Level_Points += Points;
                Sprite_Renderer.enabled = false;
                AudioHandler.Instance.PlayAudio(Point_Audio);
                points_gived = true;
            }
        }
    }

}
