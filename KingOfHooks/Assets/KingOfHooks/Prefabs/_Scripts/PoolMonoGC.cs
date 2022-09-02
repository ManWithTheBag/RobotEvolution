using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMonoGC<T> where T : MonoBehaviour
{
    private Transform _container;
    private T _prefab;
    private List<T> _pool;
    private T _temraporyElement;

    public PoolMonoGC(T prefab, int poolCount, Transform container)
    {
        _prefab = prefab;
        _container = container;
        CreatePool(poolCount);
    }

    private void CreatePool(int poolCount)
    {
        _pool = new List<T>();

        for (int i = 0; i < poolCount; i++)
            _pool.Add(CreateObject());
    }

    private T CreateObject()
    {

        T element = Object.Instantiate(_prefab, _container);
        //element.gameObject.SetActive(false);
        return element;

    }

    public T GetFreeElement()
    {
        if (HasFreeElement())
        {
            return _temraporyElement;
        }

        throw new Exception($"This pool haven't free element, pool's name:{typeof(T)}");
    }
    private bool HasFreeElement()
    {
        foreach (T item in _pool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                _temraporyElement = item;
                _temraporyElement.gameObject.SetActive(true);
                return true;
            }
        }
        _temraporyElement = null;
        return false;
    }
    public List<T> GetAllElementsList()
    {
        return _pool;
    }
}
