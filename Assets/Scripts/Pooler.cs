using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    None,
    AppleFruit,
    OrangeFruit,
    PearFruit
}

[System.Serializable]
public class SubPool
{
    public EnemyType enemyType;
    public GameObject prefab;
    public Queue<GameObject> Pool;
    public List<GameObject> ActiveObjects;
    public Transform parentTransform;
}

public class Pooler : MonoBehaviour
{
    [SerializeField] private List<SubPool> _pools;
    private Dictionary<EnemyType, SubPool> PoolDictionary;

    void Awake()
    {
        PoolDictionary = new Dictionary<EnemyType, SubPool>();
        InitSubPool();
    }

    void InitSubPool()
    {
        foreach(SubPool pool in _pools)
        {
            pool.Pool = new Queue<GameObject>();
            pool.ActiveObjects = new List<GameObject>();
            for(int i = 0; i < 1; i++)
            {
                GameObject gameObject = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity, pool.parentTransform) as GameObject;
                ObjectType objectType = gameObject.GetComponent<ObjectType>();
                objectType.Type = pool.enemyType;
                pool.Pool.Enqueue(gameObject);
                gameObject.SetActive(false);
            }
            PoolDictionary.Add(pool.enemyType, pool);
        }
    }

    public GameObject GetGameObjectFromPool(EnemyType enemyType)
    {
        if (!PoolDictionary.ContainsKey(enemyType))
        {
            return null;
        }
        SubPool pool = PoolDictionary[enemyType];
        if(pool.Pool.Count != 0)
        {
            GameObject gameObject = pool.Pool.Dequeue();
            pool.ActiveObjects.Add(gameObject);
            return gameObject;
        }
        else
        {
            GameObject gameObject = AddObjectToSubPool(enemyType);
            pool.ActiveObjects.Add(gameObject);
            return gameObject;
        }
    }

    public void ReturnToPool(EnemyType enemyType, GameObject gameObject)
    {
        if (!PoolDictionary.ContainsKey(enemyType))
        {
            return;
        }
        SubPool pool = PoolDictionary[enemyType];
        pool.ActiveObjects.Remove(gameObject);
        pool.Pool.Enqueue(gameObject);
        gameObject.SetActive(false);
    }

    private GameObject AddObjectToSubPool(EnemyType enemyType)
    {
        if (!PoolDictionary.ContainsKey(enemyType))
        {
            return null;
        }
        SubPool pool = PoolDictionary[enemyType];
        GameObject gameObject = Instantiate(pool.prefab, Vector3.zero, Quaternion.identity, pool.parentTransform) as GameObject;
        ObjectType objectType = gameObject.GetComponent<ObjectType>();
        objectType.Type = pool.enemyType;
        gameObject.SetActive(false);
        return gameObject;
    }
}
