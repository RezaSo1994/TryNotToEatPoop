using UnityEngine;
using UnityEditor;
using System.IO;

public class ClearData : EditorWindow
{

    [MenuItem("Game/Clear")]
    public static void ShowWindow()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Delet");
    }

}
