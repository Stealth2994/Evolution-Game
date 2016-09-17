using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateGrid : MonoBehaviour {
    public List<GameObject> gridObjects;
    public List<GameObject> foods;
    public int length;
    public int width;
    public static int chunkSize = 8;
    public List<PoolSystem> pools;
    public Dictionary<coords, TerrainTileValues> grid;
    // Use this for initialization
    void Awake() {
        grid = new Dictionary<coords, TerrainTileValues>();
        for (int i = 0; i < gridObjects.Count; i++)
        {
            SetTiles(i);
        }
        for (int i = 1; i < gridObjects.Count; i++)
        {
            BunchSpawns(i);
        }
        for (int i = 0; i < foods.Count; i++)
        {
            AddFood(i);
        }
        Chunk.MakeChunks(grid);
        StartCoroutine(CreateGrid());
    }
    TerrainTileValues u;

    void SetTiles(int layer)
    {
        GameObject gg = gridObjects[layer];
        if (gg.GetComponent<TerrainTileValues>())
        {
            u = gg.GetComponent<TerrainTileValues>();
        }
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {

                if (layer == 0)
                {
                    grid.Add(new coords(x, y, 1), u);
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
    ProcessGrid pg;
    Dictionary<coords,GameObject> created = new Dictionary<coords,GameObject>();
	IEnumerator CreateGrid()
    {
        while(true) { 
            pg = new ProcessGrid();
            pg.grid = chunkList;
            pg.x = player.transform.position.x;
            pg.y = player.transform.position.y;
            pg.render = renderdistance;
            pg.created = created;
            
            pg.Start();
            
            yield return StartCoroutine(pg.WaitFor());

            foreach (KeyValuePair<coords, Chunk> entry in pg.addTo)
            {

                //yield return new WaitForSecondsRealtime(0.01f);
                foreach (KeyValuePair<coords, TerrainTileValues> ggg in entry.Value.t)
                {
                    
                    foreach (PoolSystem p in pools)
                    {
                        if (p.code == ggg.Value.code)
                        {
                            GameObject g = p.GetPooledObject();
                            g.SetActive(true);
                            g.transform.position = new Vector3(ggg.Key.x, ggg.Key.y, 0);
                            g.transform.parent = transform;
                            created.Add(new coords(ggg.Key.x, ggg.Key.y, 2), g);
                        }
                    }
                }
            }
            foreach (KeyValuePair<coords, Chunk> entry in pg.removeFrom)
            {
              //  yield return new WaitForSecondsRealtime(0.01f);
                foreach (KeyValuePair<coords, TerrainTileValues> ggg in entry.Value.t)
                {
                    
                    foreach (PoolSystem p in pools)
                    {
                        if (p.code == ggg.Value.code)
                        {
                            GameObject hit;
                            if (created.TryGetValue(coords.withCreatedCoords(ggg.Key.x, ggg.Key.y), out hit))
                            {
                                created.Remove(coords.withCreatedCoords(ggg.Key.x, ggg.Key.y));
                                createdcoorder.Remove(coords.withCreatedCoords(ggg.Key.x, ggg.Key.y));
                                p.RecycleObject(hit);
                            }
                        }

                    }
                }
            }

            yield return new WaitForSeconds(0);
        }
        
        }
    public static List<coords> coorder = new List<coords>();
    public static List<coords> basecoorder = new List<coords>();
    public static List<coords> createdcoorder = new List<coords>();
    public static List<coords> chunkcoorder = new List<coords>();
    public static Dictionary<coords, Chunk> chunkList = new Dictionary<coords, Chunk>();
    public class Chunk
    {
        public static void MakeChunks(Dictionary<coords,TerrainTileValues> tiles)
        {
            foreach(KeyValuePair<coords, TerrainTileValues> entry in tiles)
            {
                if(!AddToChunk(entry))
                {
                    new Chunk(new coords(entry.Key.x, entry.Key.y, 3));
                    AddToChunk(entry);
                }
            }
        }
        public static bool AddToChunk(KeyValuePair<coords,TerrainTileValues> k)
        {
  
            foreach(KeyValuePair<coords, Chunk> u in chunkList)
            {
     
                if(u.Key.x > k.Key.x - chunkSize && u.Key.x < k.Key.x + chunkSize && u.Key.y > k.Key.y - chunkSize && u.Key.y < k.Key.y + chunkSize)
                {
                    u.Value.t.Add(k.Key,k.Value);
                    return true;
                }
            }
            return false;
        }
        public bool alive = false;
        public coords c;
        public Dictionary<coords, TerrainTileValues> t = new Dictionary<coords, TerrainTileValues>();
        public Chunk(coords d)
        {
           c = d;
            chunkList.Add(d,this);
        }
    }
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
            return new coords(-1,-1,-1);
        }
        public static coords withChunkCoords(int x, int y)
        {
            foreach (coords c in chunkcoorder)
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
            else if (add == 2)
                createdcoorder.Add(this);
            else if (add == 3)
                chunkcoorder.Add(this);
        }
    }
    }

