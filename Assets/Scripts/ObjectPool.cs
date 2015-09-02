using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class ObjectPool : MonoBehaviour
{

    [System.Serializable]
    public class StartupPool
    {
        public int size;
        public GameObject prefab;
    }

    public StartupPool[] startupPools;
    Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>();
    Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>();

    private bool startupPoolsCreated;

    private static ObjectPool _instance;
    public static ObjectPool instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ObjectPool>();

                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
        CreateStartupPools();
    }

    public static void CreateStartupPools()
    {
        if (!instance.startupPoolsCreated)
        {
            instance.startupPoolsCreated = true;

            var pools = instance.startupPools;
            if (pools != null && pools.Length > 0)
            {
                for (int i = 0; i < pools.Length; ++i)
                    CreatePool(pools[i].prefab, pools[i].size);
            }
        }
    }

    public static void CreatePool(GameObject prefab, int initialPoolSize)
    {
        if (prefab != null && !instance.pooledObjects.ContainsKey(prefab))
        {
            var list = new List<GameObject>();

            //Agrego la lista al Dictionary
            instance.pooledObjects.Add(prefab, list);

            if (initialPoolSize > 0)
            {
                bool active = prefab.activeSelf;
                prefab.SetActive(false);
                Transform parent = instance.transform;
                while (list.Count < initialPoolSize)
                {
                    var obj = (GameObject)Object.Instantiate(prefab);
                    obj.transform.parent = parent;
                    list.Add(obj);
                }
                prefab.SetActive(active);
            }
        }
    }

    public static GameObject Spawn(GameObject prefab, Transform parent, Vector3 position, Quaternion rotation)
    {
        List<GameObject> list;
        Transform trans;
        GameObject obj;

        //Busco el prefab en mi diccionario
        if (instance.pooledObjects.TryGetValue(prefab, out list))
        {
            obj = null;
            //si la lista de objetos de mi prefab no esta vacia
            if (list.Count > 0)
            {
                //obtengo un obj de mi lista y lo saco de esta
                while (obj == null && list.Count > 0)
                {
                    obj = list[0];
                    list.RemoveAt(0);
                }
                //retorno el objeto de mi lista con los parametros
                if (obj != null)
                {
                    trans = obj.transform;
                    trans.parent = parent;
                    trans.localPosition = position;
                    trans.localRotation = rotation;
                    obj.SetActive(true);
                    instance.spawnedObjects.Add(obj, prefab);
                    return obj;
                }
            }
            //si esta vacia instancio el prefab y lo agrego a mi lista de obj instanciados
            obj = (GameObject)Object.Instantiate(prefab);
            trans = obj.transform;
            trans.parent = parent;
            trans.localPosition = position;
            trans.localRotation = rotation;
            instance.spawnedObjects.Add(obj, prefab);
            return obj;
        }
        else //si no existe en mi diccionario instancio normalmente el prefab
        {
            obj = (GameObject)Object.Instantiate(prefab);
            trans = obj.GetComponent<Transform>();
            trans.parent = parent;
            trans.localPosition = position;
            trans.localRotation = rotation;
            return obj;
        }
    }

    public static void Recycle(GameObject obj)
    {
        GameObject prefab;
        if (instance.spawnedObjects.TryGetValue(obj, out prefab))
            Recycle(obj, prefab);
        else
            Object.Destroy(obj);
    }
    static void Recycle(GameObject obj, GameObject prefab)
    {
        instance.pooledObjects[prefab].Add(obj);
        instance.spawnedObjects.Remove(obj);
        obj.transform.parent = instance.transform;
        obj.SetActive(false);
    }
}

//extension methods
public static class ObjectPoolExtensions
{
    public static GameObject Spawn(this GameObject prefab)
    {
        return ObjectPool.Spawn(prefab, null, Vector3.zero, Quaternion.identity);
    }

    public static void Recycle(this GameObject obj)
    {
        ObjectPool.Recycle(obj);
    }
}


