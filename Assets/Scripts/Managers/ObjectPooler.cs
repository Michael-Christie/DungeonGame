//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public enum EntityID
//{
//    Base = -1,
//    Artefact = 1
//}

///// <summary>
///// The Object Pooler is responsible for pooling objects so that we don't have to create so many during gameplay
///// </summary>
//public class ObjectPooler : MonoBehaviour
//{
//    public static ObjectPooler Instance { get; private set; }

//    //[SerializeField] private Artefact artefactPrefab;

//    private Dictionary<int, List<IBaseEntity>> pooledEntityDictionary = new Dictionary<int, List<IBaseEntity>>(); //uses a poolID

//    private List<IBaseEntity> cachedList = new List<IBaseEntity>();

//    //
//    private void Awake()
//    {
//        if(Instance != null && Instance != this)
//        {
//            DestroyImmediate(this);
//            return;
//        }

//        Instance = this;
//    }

//    private void SetUp()
//    {

//    }

//    public GameObject GetEntityInstance(EntityID _id)
//    {
//        cachedList = pooledEntityDictionary[(int)_id];

//        for(int i = 0; i < cachedList.Count; i++)
//        {
//            if (!cachedList[i].gameObject.activeSelf)
//            {
//                return cachedList[i].gameObject;
//            }
//        }

//        //all the pooled objects are taken, we need to create another one
//        return null;
//    }
//}
