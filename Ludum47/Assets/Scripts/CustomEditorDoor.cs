
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Door))]
public class CustomEditorDoor : Editor
{
    public override void OnInspectorGUI()
    {
        Door door = (Door)target;

        base.DrawDefaultInspector();

        GUILayout.Space(20f); 
        GUILayout.Label("Combinations", EditorStyles.boldLabel);

        //for (int i = 0; i < door.Combi; ++i)
        //{

            GUILayout.BeginHorizontal();
            GUILayout.Label("DoorOpen", GUILayout.Width(100));

        //GUILayout.TextField(door.Combi.DoorOpen.ToString());
        int p = -1;
        int.TryParse(GUILayout.TextField(door.Combi.DoorOpen.ToString()), out p);
        if(p != -1)
        {
            door.Combi.DoorOpen = p;
        }
            
        //}*/

        GUILayout.EndHorizontal();
    }
}
