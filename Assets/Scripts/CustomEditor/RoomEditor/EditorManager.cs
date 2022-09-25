using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorManager : MonoBehaviour
{
    public static EditorManager Instance { get; private set; }

    [SerializeField] private Transform roomRoot;

    private Dictionary<Vector3Int, Transform> chunk = new Dictionary<Vector3Int, Transform>();

    private GameObject cachedBlock;

    private string path = "Assets/Prefabs/Rooms/";

    public string assetName = "prefab";

    public bool canMove = true;

    //
    private void Awake()
    {
        Instance = this;

        PlaceBlock(BlockType.Stone, Vector3.zero, new RaycastHit());
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

        //Find which Chunk to put the block in
        Vector3Int _chunkPos = new Vector3Int(Mathf.FloorToInt(_position.x / 8f), Mathf.FloorToInt(_position.y / 8f), Mathf.FloorToInt(_position.z / 8f));

        if (!chunk.ContainsKey(_chunkPos))
        {
            GameObject _newChunk = new GameObject();
            _newChunk.transform.parent = roomRoot;
            _newChunk.transform.position = (_chunkPos * 16) + new Vector3(7f, 7f, 7f);

            _newChunk.AddComponent<BoxCollider>().size = new Vector3(16f, 16f, 16f);

            _newChunk.layer = GameConstants.Editor.ChunkLayer;

            _newChunk.name = $"Chunk({_chunkPos.x},{_chunkPos.y},{_chunkPos.z})";

            chunk.Add(_chunkPos, _newChunk.transform);
        }

        if (cachedBlock)
        {
            Instantiate(cachedBlock, _position * GameConstants.Editor.blockSpace, _rotation, chunk[_chunkPos]);
        }
    }

    public void SaveAsPrefab()
    {
        string _localPath = $"{path}{assetName}.prefab";

        _localPath = AssetDatabase.GenerateUniqueAssetPath(_localPath);

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
