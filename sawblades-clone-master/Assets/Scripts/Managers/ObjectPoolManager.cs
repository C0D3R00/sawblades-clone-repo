using UnityEngine;

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public enum ObjectPoolType
    {
        ENEMY   
    }

    protected ObjectPoolManager() { }

    [Serializable]
    public class ObjectPoolItem
    {
        public GameObject ObjectToPool;
        public ObjectPoolType Type;
        public int AmountToPool;
        public bool ShouldExpand;
    }

    [SerializeField]
    private List<ObjectPoolItem>
        _itemsToPool;

    private List<GameObject>
        _pooledObjects;

    private void Awake()
    {
        _pooledObjects = new List<GameObject>();

        foreach (var item in _itemsToPool)
        {
            for (var i = 0; i < item.AmountToPool; i++)
            {
                var obj = Instantiate(item.ObjectToPool);

                obj.name = item.Type.ToString();// + "_" + i;
                obj.SetActive(false);

                _pooledObjects.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(ObjectPoolType type)
    {
        var itemInPool = _pooledObjects.Where(i => !i.activeInHierarchy && i.name.ToUpper() == type.ToString().ToUpper()).FirstOrDefault();

        if (itemInPool != null)
        {
            itemInPool.SetActive(true);

            return itemInPool;
        }

        var objectPoolItem = _itemsToPool.Where(i => i.Type == type).FirstOrDefault();

        if (objectPoolItem != null && objectPoolItem.ShouldExpand)
        {
            var obj = Instantiate(objectPoolItem.ObjectToPool);

            obj.name = objectPoolItem.Type.ToString();// + "_" + _pooledObjects.Where(i => i.tag.ToUpper() == type.ToString().ToUpper()).Count();
            obj.SetActive(true);

            _pooledObjects.Add(obj);

            return obj;
        }

        return null;
    }
}
