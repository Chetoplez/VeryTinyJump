using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {

	public override void OnInspectorGUI()
    {
 	    DrawDefaultInspector();

        Planet planet = target as Planet;
        if (planet == null || planet.previous_planet==null) return;
        if (LevelHandler.Planet_number > 1)
        {
            if (!(Vector3.Distance(planet.transform.position,planet.previous_planet.transform.position)<LevelHandler.Max_Planet_Offset))
            {
                planet.transform.position = new Vector3(planet.previous_planet.transform.position.x + LevelHandler.Max_Planet_Offset, planet.transform.position.y, 0f);
                Debug.DrawLine(planet.transform.position, planet.previous_planet.transform.position, Color.red);
            }

            if (!(Vector3.Distance(planet.transform.position, planet.previous_planet.transform.position) > LevelHandler.Min_Planet_Offset))
            {
                planet.transform.position = new Vector3(planet.transform.position.x + LevelHandler.Min_Planet_Offset, planet.transform.position.y, 0f);
                Debug.DrawLine(planet.transform.position, planet.transform.position, Color.blue);
            }

        }
      

    }
}
