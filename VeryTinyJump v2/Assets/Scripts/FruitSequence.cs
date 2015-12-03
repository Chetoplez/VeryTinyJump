using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class FruitSequence : MonoBehaviour {

    /* This will be filled with the Fruit children */
    private Fruit[] Fruit_Array;
    private bool player_detected = false;     /* Player detected? */
    private bool combo_gived = false;       /* Combo gived? */

	void Start () {
        Fruit_Array=GetComponentsInChildren<Fruit>();
        if (Fruit_Array.Length==0)
        {
            Debug.LogError("FruitSequence of object " + this.gameObject.name + " : There's no children with fruit script attached!");
        }
	}


    void OnTriggerEnter2D(Collider2D other) {
        if (GameController.Is_Player(other.gameObject) && !player_detected)
            player_detected = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (player_detected && !combo_gived)
        {
            bool all_taked = (Fruit_Array.Length>0);
            if (all_taked)
            {
                for (int i = 0; i < Fruit_Array.Length; i++)
                {
                    if (!Fruit_Array[i].Points_Gived)
                        all_taked = false;
                }
            }

            if (all_taked)
            {
                LevelHandler.Combos++;
                combo_gived = true;
            }
        }
    }

	
	
}
