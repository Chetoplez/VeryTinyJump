using UnityEngine;
using System.Collections;

public class Hud : MonoBehaviour {

    public UnityEngine.UI.Text Points_Indicator;
    public UnityEngine.UI.Text Result_points;
    public GameObject Panel_Pause;
    public GameObject Panel_Points;
    public UnityEngine.UI.Scrollbar progress_bar;
    public UnityEngine.UI.Text Seconds_text;
    public UnityEngine.UI.Text Minutes_text;
    public UnityEngine.UI.Text Combo_text;
    public UnityEngine.UI.Image Jump_button;
    public GameObject Jump_tutorial;

    public static bool Alpha_Jump_Button_Changed = false;
    private Color changed_color = new Color(1f,1f,1f,0.5f);
    private Color initial_color;

    /* Just some timing... */
    private float seconds = 0;
    private int minutes = 0;
    

    void Start() {
        GameController.hud = this;
        initial_color = Jump_button.color;
    }

	void Update () {
       

        Points_Indicator.text = GameController.Level_points.ToString();
        Result_points.text = GameController.Level_points.ToString();
        Combo_text.text = GameController.Combo_number.ToString();

        Panel_Pause.SetActive(GameController.Pause && GameController.Alive);
        Panel_Points.SetActive(GameController.Pause && !GameController.Alive);
        Jump_button.color = (Alpha_Jump_Button_Changed)?  changed_color : initial_color;
        Jump_tutorial.SetActive(!GameController.Tutorial_Showed);
       

        seconds += Time.deltaTime;
        if (seconds > 60)
        {
            seconds = 0;
            minutes++;
        }

        Seconds_text.text = seconds.ToString();
        Minutes_text.text = minutes.ToString();
        
	}

    public void Advance_Progress_Bar_Level(float value) {
        progress_bar.value += value / 100f;
    }

    public void Set_Progress_Bar_Level(float level) {
        progress_bar.value = level / 100f;
    }

}
