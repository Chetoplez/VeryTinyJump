using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/* This class manage our planets in the scene */
public class LevelHandler : MonoBehaviour
{

    #region INSPECTOR_VARIABLES

        public List<Planet> Planet_List = new List<Planet>();    /* List of the created planet */
        public GameObject Planet_Parent;                         /* All the planets created will be parented to this object (optional) */
        public Planet Planet_Model;                              /* This is the model of the planet that we use */
        [Range(1,10)]
        public float MinOffsetPlanet = 2f;                       /* This is the minimum offset between planets */
        [Range(5,15)]
        public float MaxOffsetPlanet = 10f;                      /* This is the maximum offset between planets */
        public GameController Game_Controller;                  /*  Needed for setting the planet number..*/
    #endregion

    private Vector3 Starting_Position = Vector3.zero;        /* Starting position of the created planet */

    
    #region STATIC_ATTRIBUTES
    
    public static int Level_Points = 0; /* Points of the level. Need static because on the script fruit we attach this variable. There's so many fruit! */
    public static int Combos = 0;  /* Just as level points */
    public static float Progress = 1f; /* Used by the scrollbar */
    
    #endregion

    #region EDITOR_FUNCTION
    /* Add a planet to our list, if possible */
    public void Add_Planet() {
        if (Planet_Model == null)
        {
            Debug.LogError("LevelHandler: PlanetModel is null, so i cannot instantiate an object of this type.");
            return;
        }
        Calculate_Starting_Position();

        /* Need some initialization before the add operation ...*/
        Planet instantiated_planet = Instantiate(Planet_Model, Starting_Position, Quaternion.identity) as Planet;
        if (Planet_List.Count > 0)
            instantiated_planet.previous_planet = Planet_List[Planet_List.Count - 1];
        instantiated_planet.list_index = Planet_List.Count;
        instantiated_planet.name = "Planet_" + Planet_List.Count;
        instantiated_planet.transform.parent = Planet_Parent.transform;
        
        Planet_List.Add(instantiated_planet);
    }
    
    /* Remove a planet from our list */
    public void Remove_Planet() {
        if (Planet_List.Count > 0)
        {
            Planet p = Planet_List[Planet_List.Count - 1] as Planet ?? null;
            if (p != null)
                DestroyImmediate(p.gameObject);
            Planet_List.RemoveAt(Planet_List.Count - 1);
        }
    }

    /* Check if someone has deleted some element in our list  */
    public bool Check_List_Integrity() {
        foreach (Planet p in Planet_List)
        {
            if (p == null) return false;
        }
        return true;
    }

    public void Rebuild_List() {
        List<Planet> temp_list = new List<Planet>();

        /* Must reset the id as well */
        int i = 0;
        foreach (Planet p in Planet_List)
        {
            if (p != null)
            {

                if (i > 1)
                {
                    p.previous_planet = temp_list[i - 1];
                    if (!Check_Planet_Distance(p, temp_list[i - 1]))
                        p.transform.position = new Vector3(temp_list[i - 1].transform.position.x + MinOffsetPlanet, p.transform.position.y, 0f);
                }
                p.list_index = i;

                temp_list.Add(p);
                i++;
            }
        }
        Planet_List = temp_list;
    }

    /* Calculate the starting position for the next planet that will be instantiated */
    public void Calculate_Starting_Position() {
        if (Planet_List.Count > 0)
        {
            Starting_Position = Planet_List[Planet_List.Count - 1].transform.position;
            Starting_Position.x += MinOffsetPlanet;
        }
    }

    /* Check distance between planets */
    public bool Check_Planet_Distance(Planet p1, Planet p2)
    {
        return Vector3.Distance(p1.transform.position, p2.transform.position) < this.MaxOffsetPlanet;
    }
    #endregion


    void Start() {
        Level_Points = 0;
        Combos = 0;
        Game_Controller.Planet_Number = this.Planet_List.Count;
    }

    
}
