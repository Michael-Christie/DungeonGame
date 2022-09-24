using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorManager : MonoBehaviour
{
    public static EditorManager Instance { get; private set; }

    [SerializeField] private Transform roomRoot;

    private GameObject cachedBlock;

    private string path = "Assets/Prefabs/Rooms/";

    public string assetName = "prefab";

    public bool canMove = true;

    //
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!canMove)
            return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            MenuManager.Instance.ShowMenu(0);
            canMove = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuManager.Instance.ShowMenu(1);
            canMove = false;
        }
    }

    public void PlaceBlock(BlockType _type, Vector3 _position, RaycastHit _hit)
    {
        cachedBlock = BlockManager.Instance.GetBlock(_type);

        Quaternion _rotation = BlockManager.Instance.GetBlockRotation(_type, _hit.point, _hit.normal);

        if (cachedBlock)
        {
            Instantiate(cachedBlock, _position * GameConstants.Editor.blockSpace, _rotation, roomRoot);
        }
    }

    public void SaveAsPrefab()
    {
        string _localPath = $"{path}{assetName}.prefab";

        _localPath = AssetDatabase.GenerateUniqueAssetPath(_localPath);

        Debug.Log(_localPath);

        PrefabUtility.SaveAsPrefabAsset(roomRoot.gameObject, _localPath, out bool _success);

        if (_success)
        {
            Debug.Log("<color=001100>Save Success</color>");
        }
    }

    public void LoadPrefab()
    {

    }
}
