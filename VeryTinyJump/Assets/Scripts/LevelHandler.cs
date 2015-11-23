using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Handle the level! */
public class LevelHandler : MonoBehaviour {
    
    /* How many planet we create? Needed from other script */
    public static int Planet_number=0;

    public static float Max_Planet_Offset = 10f; /* Max distance between the planets */
    public static float Min_Planet_Offset = 5f; /* Min distance between the planets */


    public Planet Planet_Model; /* Needed for creating a planet */
    /* List of the created planet */
    public  List<Planet> myplanet_list=new List<Planet>();

    /* This is the first planet that the player must go */
    public static int Actual_Planet_Index = 0;

    
    /* Starting position of the planet */
    private Vector3 Starting_Position= Vector3.zero;


    private static LevelHandler instance;
    public static LevelHandler Instance { get { return instance ?? new LevelHandler(); } }


   
    private void Awake(){
        instance = this;
    }

    private void Start() {
        Planet_number = myplanet_list.Count;
        Debug.Log("Number of planets=" + Planet_number);
    }
    

    #region EditorMethods

    /* Create a planet */
    public void Create_Planet() {
        if (Planet_Model == null)
        {
            Debug.LogError("Please assign a correct planet model for the creation");
            return;
        }
        Calculate_Starting_Position();
        
        Planet instantiated_planet = Instantiate(Planet_Model, Starting_Position, Quaternion.identity) as Planet;
        if (myplanet_list.Count >= 1)
        {
            Debug.Log("Counter=" + (myplanet_list.Count -1));
            instantiated_planet.previous_planet = myplanet_list[myplanet_list.Count - 1]; 
        }
        instantiated_planet.id = myplanet_list.Count;
        instantiated_planet.name = "Planet_" + myplanet_list.Count;
        myplanet_list.Add(instantiated_planet);
        Planet_number = myplanet_list.Count;
    }

    /* Remove the last planet */
    public void Remove_Planet() {
        if (myplanet_list.Count > 0)
        {
            Planet p = myplanet_list[myplanet_list.Count - 1] as Planet ?? null;
            if(p!=null)
                DestroyImmediate(p.gameObject);
            myplanet_list.RemoveAt(myplanet_list.Count-1);
            Planet_number = myplanet_list.Count;
        }
    }

    /* Check if some element of the list is null*/
    public bool Check_Nullable_Elements() {
        int i = 0;
        foreach (Planet p in myplanet_list)
        {
            if (p == null)
                return true;
            i++;
        }
        return false;
    }


    /* Rebuild the list if some element is null */
    public void Rebuild_Planet_List() {
       List<Planet> temp_list = new List<Planet>();
        
       /* Must reset the id as well */
       int i = 0;
         foreach (Planet p in myplanet_list)
         {
              if (p != null)
              {

                  if (i > 1)
                  {
                      p.previous_planet= temp_list[i - 1];
                      if (!Check_Planet_Distance(p,temp_list[i - 1]))
                       p.transform.position = new Vector3( temp_list[i - 1].transform.position.x + Max_Planet_Offset, p.transform.position.y,0f);
                  }
                  p.id = i;
                  
                  temp_list.Add(p);
                  i++;
              }
         }
         myplanet_list = temp_list;
         Planet_number = myplanet_list.Count;
    }



    /* Check distance between planets */
    public static bool Check_Planet_Distance(Planet p1,Planet p2) {
        return Vector3.Distance(p1.transform.position,p2.transform.position) < Max_Planet_Offset;
    }



    /* Calculate the starting position of the next planet created */
    private void Calculate_Starting_Position() {
        float starting_x = (myplanet_list.Count>0) ?  myplanet_list[myplanet_list.Count - 1].transform.position.x  + Min_Planet_Offset: 0;
        Starting_Position.x = starting_x;
    }

    #endregion


    #region CameraMethods

    public bool Get_Next_Center(ref  Vector3 next_center, Vector3 camera_position) { 
        /* If we have only one planet we can't move the camera... */
        if (myplanet_list.Count <= 1) return false; 
        /* If we ended the planet list we finish */
        if (Actual_Planet_Index == (myplanet_list.Count )) return false;

        Debug.Log("Actual planet index=" + Actual_Planet_Index);
        CameraBehavior.Planet_left = myplanet_list[Actual_Planet_Index];
        CameraBehavior.Planet_right = (Actual_Planet_Index +1 < myplanet_list.Count )?myplanet_list[Actual_Planet_Index + 1] : null;

        if (CameraBehavior.Planet_right == null) return false;

        next_center = (myplanet_list[Actual_Planet_Index +1].transform.position + myplanet_list[Actual_Planet_Index].transform.position)/2   ;
        next_center.z = camera_position.z;
        return true;
    }


    #endregion

}
