using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {

    Vector3 new_position = Vector3.zero;
    LevelHandler level_handler = null;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (level_handler == null)
            level_handler = GameObject.FindGameObjectWithTag(GameController.TAG_LEVEL_HANDLER).GetComponent<LevelHandler>();

      
        Planet planet = target as Planet;
        if (planet.previous_planet != null && level_handler != null)
        {
            if (Vector3.Distance(planet.transform.position, planet.previous_planet.transform.position) < level_handler.MinOffsetPlanet)
            {
                new_position = planet.transform.position;
                new_position.x = planet.previous_planet.transform.position.x + level_handler.MinOffsetPlanet;
                planet.transform.position = new_position;
                Debug.DrawLine(planet.transform.position, planet.previous_planet.transform.position, Color.white);
            }

            if (Vector3.Distance(planet.transform.position, planet.previous_planet.transform.position) > level_handler.MaxOffsetPlanet)
            {
                new_position = planet.transform.position;
                new_position.x = planet.previous_planet.transform.position.x + level_handler.MaxOffsetPlanet;
                planet.transform.position = new_position;
                Debug.DrawLine(planet.transform.position, planet.previous_planet.transform.position, Color.red);
            }
        }
        else
        {
            if(level_handler==null)
                Debug.Log("PlanetEditor : Cannot check. LevelHandler is null");
        }

    }
}
