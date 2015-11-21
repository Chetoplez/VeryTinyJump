using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelHandler))]
public class LevelEditor : Editor {

	public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelHandler level_handler = target as LevelHandler;
        if (GUILayout.Button("Add Planet"))
            level_handler.Create_Planet();
        if (GUILayout.Button("Delete Last Planet"))
            level_handler.Remove_Planet();

        if (level_handler.Check_Nullable_Elements())
            level_handler.Rebuild_Planet_List();

    }
	
}
