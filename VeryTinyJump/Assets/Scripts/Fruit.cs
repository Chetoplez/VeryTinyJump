using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Fruit : MonoBehaviour {
    
    /* Points given by the fruit*/
    [Range(5,100)]
    public int Point = 10;

    private SpriteRenderer renderer;
    private bool points_gived=false;

    void Start() {
       renderer=GetComponent<SpriteRenderer>();
    }
    
    void OnTriggerStay2D(Collider2D other) {
        if (GameController.Is_Player(other.gameObject) && !points_gived)
        {   
            GameController.Level_points += Point;
            points_gived = true;
            renderer.enabled = false;
        }
    }


}
