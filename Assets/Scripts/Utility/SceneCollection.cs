using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneCollection")]
public class SceneCollection : ScriptableObject
{
    [SerializeField] private string title;

    public GameConstants.Scenes[] scenes;

    public void OnValidate()
    {
        
    }
}
