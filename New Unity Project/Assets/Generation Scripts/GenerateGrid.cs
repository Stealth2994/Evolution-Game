using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateGrid : MonoBehaviour {
    public List<GameObject> gridObjects;
    public List<GameObject> foods;
    public int length;
    public int width;
    public static int chunkSize = 8;
    public static int megaChunkSize = 64;
    bool dynamic = false;
    public List<PoolSystem> pools;
    Coroutine c;
    public Dictionary<coords, TerrainTileValues> grid;
    // Use this for initialization
    void Awake() {
        grid = new Dictionary<coords, TerrainTileValues>();
        c = StartCoroutine(GenerateMap());
       
    }
    void Update()
    {
        Debug.Log(grid.Count);
    }
    TerrainTileValues u;
    IEnumerator GenerateMap()
    {
        for (int i = 0; i < gridObjects.Count; i++)
        {
            
            SetTiles(i);
            yield return new WaitForSeconds(0);
        }
        for (int i = 1; i < gridObjects.Count; i++)
        {
            
            BunchSpawns(i);
            yield return new WaitForSeconds(0);
        }
        for (int i = 0; i < foods.Count; i++)
        {
            AddFood(i);
            yield return new WaitForSeconds(0);
        }
        Chunk.MakeChunks(grid);
        MegaChunk.MakeChunks(chunkList);
        StartCoroutine(CreateGrid());
        StopCoroutine(c);
    }
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
                    grid.Add(new coords(x, y), u);
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
                
                if (grid.TryGetValue(new coords(x + 1,y), out hit))
                {

                    if (hit.code == t.code)
                    {
                        b++;

                    }
                }
                
                if (grid.TryGetValue(new coords(x - 1, y), out hit2))
                {

                    if (hit2.code == t.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x, y + 1), out hit3))
                {

                    if (hit3.code == t.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x,y - 1), out hit4))
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
                    if (grid.TryGetValue(new coords(x,y), out hit))
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
            
            grid.Remove(new coords(x,y));
            grid.Add(new coords(x, y),t);
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
            pg.grid = megaChunkList;
            pg.x = player.transform.position.x;
            pg.y = player.transform.position.y;
            pg.render = renderdistance;
            pg.created = created;
            
            pg.Start();
            
            yield return StartCoroutine(pg.WaitFor());

            foreach (KeyValuePair<coords, Chunk> entry in pg.addTo)
            {

                yield return new WaitForSecondsRealtime(0.01f);
                foreach (KeyValuePair<coords, TerrainTileValues> ggg in entry.Value.t)
                {
                    
                    foreach (PoolSystem p in pools)
                    {
                        if (p.code == ggg.Value.code)
                        {
                            GameObject g = p.GetPooledObject();
                            g.SetActive(true);
                            g.transform.position = new Vector3(ggg.Key.x, ggg.Key.y);
                            g.transform.parent = transform;
                            created.Add(new coords(ggg.Key.x, ggg.Key.y), g);
                        }
                    }
                }
            }
            foreach (KeyValuePair<coords, Chunk> entry in pg.removeFrom)
            {
                yield return new WaitForSecondsRealtime(0.01f);
                foreach (KeyValuePair<coords, TerrainTileValues> ggg in entry.Value.t)
                {
                    
                    foreach (PoolSystem p in pools)
                    {
                        if (p.code == ggg.Value.code)
                        {
                            GameObject hit;
                            if (created.TryGetValue(new coords(ggg.Key.x, ggg.Key.y), out hit))
                            {
                                created.Remove(new coords(ggg.Key.x, ggg.Key.y));
                                createdcoorder.Remove(new coords(ggg.Key.x, ggg.Key.y));
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
    public static Dictionary<coords,MegaChunk> megaChunkList = new Dictionary<coords, MegaChunk>();
  public class MegaChunk
    {
        public static void MakeChunks(Dictionary<coords, Chunk> tiles)
        {
            
            megaChunkList = new Dictionary<coords, MegaChunk>();
            foreach (KeyValuePair<coords, Chunk> entry in tiles)
            {

                if (!AddToChunk(entry))
                {
                    new MegaChunk(new coords(entry.Key.x, entry.Key.y));
                    AddToChunk(entry);
                }
            }
        }
        public static bool AddToChunk(KeyValuePair<coords, Chunk> k)
        {

            foreach (KeyValuePair<coords, MegaChunk> u in megaChunkList)
            {

                if (u.Key.x > k.Key.x - megaChunkSize - 1 && u.Key.x < k.Key.x + megaChunkSize + 1 && u.Key.y > k.Key.y - megaChunkSize - 1 && u.Key.y < k.Key.y + megaChunkSize + 1)
                {   u.Value.t.Add(k.Key, k.Value);
                    return true;
                }
            }
            return false;
        }
        public bool alive = false;
        public coords c;
        public Dictionary<coords, Chunk> t = new Dictionary<coords, Chunk>();
        public MegaChunk(coords d)
        {
            c = d;
            megaChunkList.Add(d, this);
        }
    }
    public class Chunk
    {
        public static void MakeChunks(Dictionary<coords,TerrainTileValues> tiles)
        {
            float finalit = 0;
            chunkList = new Dictionary<coords, Chunk>();
       foreach(coords key in tiles.Keys)
            {
                float ok = Time.realtimeSinceStartup;
                TerrainTileValues t = tiles[key];
                if (!AddToChunk(key,t))
                {

                    new Chunk(new coords(key.x, key.y));


                    AddToChunk(key,t);

                }
                finalit += Time.realtimeSinceStartup - ok;
            }
   

           
             
                
            
            Debug.Log(finalit);
        }
        public static bool AddToChunk(coords c, TerrainTileValues t)
        {
  
            foreach(KeyValuePair<coords, Chunk> u in chunkList)
            {
     
                if(u.Key.x > c.x - chunkSize && u.Key.x < c.x + chunkSize && u.Key.y > c.y - chunkSize && u.Key.y < c.y + chunkSize)
                {
                    u.Value.t.Add(c,t);
                    return true;
                }
            }
            return false;
        }
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
        public override int GetHashCode()
        {
            return GetHashCodeInternal(x.GetHashCode(), y.GetHashCode());
        }
        //this function should be move so you can reuse it
        private static int GetHashCodeInternal(int key1, int key2)
        {
            unchecked
            {
                //Seed
                var num = 0x7e53a269;

                //Key 1
                num = (-1521134295 * num) + key1;
                num += (num << 10);
                num ^= (num >> 6);

                //Key 2
                num = ((-1521134295 * num) + key2);
                num += (num << 10);
                num ^= (num >> 6);

                return num;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            coords p = obj as coords;
            if (p == null)
                return false;

            // Return true if the fields match:
            return (x == p.x) && (y == p.y);
        }
      
    
        
        public int x;
        public int y;
        public coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    }

