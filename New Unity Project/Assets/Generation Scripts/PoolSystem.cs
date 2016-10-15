using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PoolSystem : MonoBehaviour {
    //pool variables
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;
    public int code;
    public List<GameObject> pooledObjects;
    public int listSize;
    void Start()
    {
        //creates the base pool based on poolamount and primes them
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        listSize = pooledObjects.Count;
    }
    //ACTIVE INACTIVE
    public GameObject GetPooledObject()
    {
        //looks for one thats not null and grabs it and gives it to the caller
        for (int i = 0; i < listSize; i++)
        {
            if (pooledObjects[i] == null)
            {
                pooledObjects.RemoveAt(i);
                goto done;
            }
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }

            done:;
        }
        

        
        //if oops we ran out but it can grow just make a new one and give it over
        if (willGrow)
        {
        GameObject obj = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj);
            listSize++;
            return obj;
        }

        return null;
    }
    public void Exterminate(GameObject g)
    {

        pooledObjects.Remove(g);
    }
    //Put it back in the pool
    public void RecycleObject(GameObject g)
    {
        
        pooledObjects.Insert(0,g);
        g.SetActive(false);
    }
}
