using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

    public UnityEngine.UI.Text Points_Indicator;
    public GameObject Panel_Pause;
    

	
	void Update () {
        Points_Indicator.text = GameController.Level_points.ToString();
        Panel_Pause.SetActive(GameController.Pause);
        
	}
}
