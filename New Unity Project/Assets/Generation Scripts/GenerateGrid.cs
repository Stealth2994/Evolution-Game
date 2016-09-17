using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateGrid : MonoBehaviour {
    public float start;
    public float mid;
    public float last;
    public List<GameObject> gridObjects;
    public List<GameObject> foods;
    public int length;
    public int width;
    public List<PoolSystem> pools;
    public Dictionary<coords, TerrainTileValues> grid;
    // Use this for initialization
    void Awake () {
            start = Time.deltaTime;
            grid = new Dictionary<coords, TerrainTileValues>();
            for (int i = 0; i < gridObjects.Count; i++)
            {
                SetTiles(i);
            }
            for (int i = 1; i < gridObjects.Count; i++)
            {
                BunchSpawns(i);
            }
            for(int i = 0; i < foods.Count; i++)
            {
                AddFood(i);
            }
            last = Time.deltaTime - mid;
            Debug.Log(start + "," + mid + "," + last);
            StartCoroutine(CreateGrid()); 
        
        
    }
    TerrainTileValues u;

    void SetTiles(int layer)
    {
        GameObject gg = gridObjects[layer];
        if (gg.GetComponent<TerrainTileValues>())
        {
           u  = gg.GetComponent<TerrainTileValues>();
        }
            for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
             
                    if (layer == 0)
                    {
                        grid.Add(new coords(x,y,1), u);
                    }
                    else
                    {
                        DoBunchChance(u, x, y, u.spawnChance);


                    }
               
            }
        }
    }
    TerrainTileValues t;
    void BunchSpawns(int layer)
    {
        GameObject gg = gridObjects[layer];
        
        if (gg.GetComponent<TerrainTileValues>())
        {
            t = gg.GetComponent<TerrainTileValues>();
        }
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
             
                    
                    TerrainTileValues hit;
                    TerrainTileValues hit2;
                    TerrainTileValues hit3;
                    TerrainTileValues hit4;


                int b = 0;

                    if (grid.TryGetValue(coords.withCoords(x + 1, y), out hit))
                    {

                        if (hit.code == t.code)
                        {
                            b++;

                        }
                    }
                    if (grid.TryGetValue(coords.withCoords(x - 1, y), out hit2))
                    {

                        if (hit2.code == t.code)
                        {
                            b++;

                        }
                    }
                    if (grid.TryGetValue(coords.withCoords(x, y + 1), out hit3))
                    {

                        if (hit3.code == t.code)
                        {
                            b++;

                        }
                    }
                    if (grid.TryGetValue(coords.withCoords(x, y - 1), out hit4))
                    {
                        if (hit4.code == t.code)
                        {
                            b++;

                        }
                    }

                        DoBunchChance(t, x, y, t.bunchChance * t.bunchMultiplier * b);
                    
                }
            
        }
    }
    int grasscode = 500;
    TerrainTileValues g;
    void AddFood(int food)
    {
        GameObject gg = foods[food];
        if (gg.GetComponent<TerrainTileValues>())
        {
            g = gg.GetComponent<TerrainTileValues>();
        }
           
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {

             
                    TerrainTileValues hit;
                    if (grid.TryGetValue(coords.withCoords(x, y), out hit))
                    {

                    }
                    else
                    {
                        DoBunchChance(g, x, y, g.spawnChance);
                    }




                
            }
        }
    }

    void DoBunchChance(TerrainTileValues t,int x, int y, float chance)
    {
        if (Random.Range(0.0f, 100.0f) <= chance)
        {
            grid.Remove(coords.withBaseCoords(x, y));
            grid.Remove(coords.withCoords(x, y));
            grid.Add(new coords(x, y,0),t);
        }
    }
    public GameObject player;
    public int renderdistance = 5;
    
    Dictionary<coords,GameObject> created = new Dictionary<coords,GameObject>();
	IEnumerator CreateGrid()
    {
        while(true) {
            
            foreach (KeyValuePair<coords, TerrainTileValues> entry in grid)
            {
                
                if (entry.Key.x > player.transform.position.x - renderdistance && entry.Key.x < player.transform.position.x + renderdistance && entry.Key.y > player.transform.position.y - renderdistance && entry.Key.y < player.transform.position.y + renderdistance)
                {
                    GameObject hit;
                       if((created.TryGetValue(coords.withCreatedCoords(entry.Key.x, entry.Key.y),out hit))) {
             

                        
                     } else {
                        foreach(PoolSystem p in pools)
                        {
                            if(p.code == entry.Value.code)
                            {
                                GameObject g = p.GetPooledObject();
                                g.SetActive(true);
                                g.transform.position = new Vector3(entry.Key.x, entry.Key.y, 0);
                                g.transform.parent = transform;
                                created.Add(new coords(entry.Key.x, entry.Key.y, 2), g);
                            }
                        }
                  
                    
                        }

                }
                else {
                    foreach (PoolSystem p in pools)
                    {
                        if (p.code == entry.Value.code)
                        {
                            GameObject hit;
                            if (created.TryGetValue(coords.withCreatedCoords(entry.Key.x, entry.Key.y), out hit))
                            {
                                created.Remove(coords.withCreatedCoords(entry.Key.x, entry.Key.y));
                                p.RecycleObject(hit);
                            }
                    }
                   
}
                    
}
                
            }

            yield return new WaitForSeconds(1f);
        }
        
        }
    public static List<coords> coorder = new List<coords>();
    public static List<coords> basecoorder = new List<coords>();
    public static List<coords> createdcoorder = new List<coords>();
    public class coords
    {
        public static coords withCoords(int x, int y)
        {
            foreach(coords c in coorder)
            {
                if(c.x == x && c.y == y)
                {
                    return c;
                }
            }
            return new coords(-1, -1,-1);
        }
        public static coords withBaseCoords(int x, int y)
        {
            foreach (coords c in basecoorder)
            {
                if (c.x == x && c.y == y)
                {
                    return c;
                }
            }
            return new coords(-1, -1, -1);
        }
          public static coords withCreatedCoords(int x, int y)
        {
            foreach (coords c in createdcoorder)
            {
                if (c.x == x && c.y == y)
                {
                    return c;
                }
            }
            return new coords(-1, -1, -1);
        }
        public int x;
        public int y;
        public coords(int x, int y, int add)
        {
            this.x = x;
            this.y = y;
            if (add == 0)
                coorder.Add(this);
            else if (add == 1)
                basecoorder.Add(this);
            else if(add == 2)
                createdcoorder.Add(this);
        }
    }
    }

