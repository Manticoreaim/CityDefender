using System.Collections.Generic;
using UnityEngine;

public class PoolGameObjects<T> where T : MonoBehaviour
{
    public T Prefab { get; private set; }

    public bool AftoIncrease { get; set; }

    private List<T> Pool;

    public PoolGameObjects(T Prefab, int SizePool)
    {
        this.Prefab = Prefab;

        CreatePool(SizePool);
    }


    private void CreatePool(int SizePool)
    {
        this.Pool = new List<T>();
        for (int i = 0; i < SizePool; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool IsActiveInStart = false)
    {
        T NewObject = Object.Instantiate(Prefab);
        NewObject.gameObject.SetActive(IsActiveInStart);
        Pool.Add(NewObject);

        return NewObject;
    }

    public bool GetFreeObject(out T Object)
    {
        foreach (T ObjectInPool in Pool)
        {
            if (!ObjectInPool.gameObject.activeInHierarchy)
            {
                Object = ObjectInPool;
                return true;
            }
            
            if(AftoIncrease)
            {
                Object = ObjectInPool;
                return true;
            }
        }
        Debug.Log("Pool not has free objects and increase is disable");

        Object = null;
        return false;
    }

    public void DisableAllObjectInPool()
    {
        for(int i = 0; i < Pool.Count;i++)
        {
            Pool[i].gameObject.SetActive(false);
        }
    }

}
