using UnityEngine;
using System.Collections;

public class FruitCurve : BezierCurve {

    public Fruit Fruit_1;
    public Fruit Fruit_2;
    public Fruit Fruit_3;
    public Fruit Fruit_4;


    private Player player;
    private bool player_presence = false;

    void Start(){
        player = HandlePlayerInput.Main_player;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (GameController.Is_Player(collider.gameObject))
        {
            if (!player_presence)
                player_presence = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (GameController.Is_Player(collider.gameObject))
        {
            if (!player_presence)
                player_presence = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (GameController.Is_Player(collider.gameObject))
        {
            if (player_presence)
            {
                if (Fruit_1.Points_gived && Fruit_2.Points_gived && Fruit_3.Points_gived && Fruit_4.Points_gived)
                {
                    GameController.Combo_number++;
                    Debug.Log("Combo!");
                }
                else
                    Debug.Log("Not a combo!"); 
                
            }
        }
    }

}
