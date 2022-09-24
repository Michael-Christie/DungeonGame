using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType
{
    Null,
    //Stone
    Stone,
    CobbleStone,
    MossyCobble,
    Andisite,
    //Wood
    OakPlank,
    OakLog,
    OakStrippedLog,
    OakSlab
}

public class BlockManager : MonoBehaviour
{
    public static BlockManager Instance { get; private set; }


    private Dictionary<BlockType, GameObject> loadeddBlocks = new Dictionary<BlockType, GameObject>();

    //
    private void Awake()
    {
        Instance = this;
    }

    private GameObject LoadBlock(BlockType _type)
    {
        GameObject _cachedObject = Resources.Load<GameObject>(GetPath(_type));

        loadeddBlocks.Add(_type, _cachedObject);

        return _cachedObject;
    }

    public GameObject GetBlock(BlockType _type)
    {
        //if the block is already loaded, just return it...
        if (loadeddBlocks.ContainsKey(_type))
        {
            return loadeddBlocks[_type];
        }

        //else load that block...
        return LoadBlock(_type);
    }

    private string GetPath(BlockType _type)
    {
        switch (_type)
        {
            #region Stones
            case BlockType.Stone:
                return EditorResources.Blocks.stone;

            case BlockType.CobbleStone:
                return EditorResources.Blocks.cobblestone;

            case BlockType.MossyCobble:
                return EditorResources.Blocks.mossyCobblestone;

            case BlockType.Andisite:
                return EditorResources.Blocks.andisite;

            #endregion

            #region Wood
            case BlockType.OakPlank:
                return EditorResources.Blocks.oakPlank;

            case BlockType.OakLog:
                return EditorResources.Blocks.oakLog;

            case BlockType.OakStrippedLog:
                return EditorResources.Blocks.oakLogStripped;

            case BlockType.OakSlab:
                return EditorResources.Blocks.oakSlab;
            #endregion
        }

        return string.Empty;
    }

    public Quaternion GetBlockRotation(BlockType _type, Vector3 _hitPoint, Vector3 _normal)
    {
        //Slabs
        if(_type == BlockType.OakSlab)
        {
            if(_hitPoint.y - Mathf.FloorToInt(_hitPoint.y) < 0.5f)
            {
                return Quaternion.identity;
            }

            return Quaternion.Euler(180, 0, 0);
        }

        //Logs
        if(_type == BlockType.OakLog
            || _type == BlockType.OakStrippedLog)
        {
            return Quaternion.Euler(_normal.z * 90, 0, _normal.x * 90);
        }

        return Quaternion.identity;
    }
}
