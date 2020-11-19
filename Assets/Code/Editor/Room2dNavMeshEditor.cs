using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Room2dNavMeshEditor : EditorWindow
{

    [MenuItem(Room2dNavMeshReferences.NavMeshMenuBarDirectory)]
    public static void OpenEditorWindow() 
    {
        var window = GetWindow<Room2dNavMeshEditor>();
        window.title = Room2dNavMeshReferences.NavMeshWindowTitlle;
    }


    private bool ShowAStar=false;
    private bool ShowFloydWarshall = false;
    private void OnGUI()
    {
        GUILayout.Label("Options");
        if (GUILayout.Button("A*")) { ShowAStar = !ShowAStar; }
        if (GUILayout.Button("Floyd Warshall")) { ShowFloydWarshall = !ShowFloydWarshall; }


        ShowAStar = EditorGUILayout.Foldout(ShowAStar,"A* Settings");

        if (ShowAStar) {
            GUILayout.Label("A* Setting:"); 
        
        }
        ShowFloydWarshall = EditorGUILayout.Foldout(ShowFloydWarshall, "FloydWarshall Settings");

        if (ShowFloydWarshall) { GUILayout.Label("FloydWarshall Setting:"); }
    }

}
