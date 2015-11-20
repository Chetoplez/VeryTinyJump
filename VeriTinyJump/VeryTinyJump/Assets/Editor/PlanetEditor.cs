using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {

	public override void OnInspectorGUI()
    {
 	    DrawDefaultInspector();

        Planet planet = target as Planet;

        if (LevelHandler.Planet_number > 1)
        {
            if (!(Vector3.Distance(planet.transform.position,planet.previous_planet_position)<LevelHandler.Max_Planet_Offset))
            {
                planet.transform.position = new Vector3(planet.previous_planet_position.x + LevelHandler.Max_Planet_Offset, planet.transform.position.y, 0f);
                Debug.DrawLine(planet.transform.position, planet.previous_planet_position, Color.red);
            }

            if (!(Vector3.Distance(planet.transform.position, planet.previous_planet_position) > LevelHandler.Min_Planet_Offset))
            {
                planet.transform.position = new Vector3(planet.previous_planet_position.x + LevelHandler.Min_Planet_Offset, planet.transform.position.y, 0f);
                Debug.DrawLine(planet.transform.position, planet.previous_planet_position, Color.red);
            }

        }
      

    }
}
