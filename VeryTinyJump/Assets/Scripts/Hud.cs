using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

    public UnityEngine.UI.Text Points_Indicator;
    public GameObject Panel_Pause;
    public UnityEngine.UI.Scrollbar progress_bar;

    void Start() {
        GameController.hud = this;
    }

	void Update () {
        Points_Indicator.text = GameController.Level_points.ToString();
        Panel_Pause.SetActive(GameController.Pause);
	}

    public void Advance_Progress_Bar_Level(float value) {
        progress_bar.value += value / 100f;
    }


}
