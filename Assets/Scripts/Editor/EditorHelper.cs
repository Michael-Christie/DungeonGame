using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorHelper
{
    [MenuItem("Editor/RotateObjects")]
    public static void RotateObject()
    {
        for (int i = 0; i < Selection.count; i++)
        {
            Debug.Log(Selection.transforms[i].name);

            Vector3 _pos = Selection.transforms[i].position;

            if (Selection.transforms[i].GetComponentInChildren<Renderer>())
            {
                _pos = Selection.transforms[i].GetComponentInChildren<Renderer>().bounds.center;
            }

            Selection.transforms[i].RotateAround(_pos, Vector3.up, Random.Range(0, 4) * 90);
            Undo.RegisterCreatedObjectUndo(Selection.gameObjects[i], $"Rotated {Selection.transforms[i].name}");
        }
    }
}
