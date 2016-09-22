using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateGrid : MonoBehaviour {
    //Objects used in grid
    public List<GameObject> gridObjects;
    public List<GameObject> foods;
    public GameObject deepWater;
    public int continentMin = 2;
    public int continentMax = 10;
    public int minRadius = 5;
    public int maxRadius = 20;
    //Pool system setup
    public List<PoolSystem> pools;
    public List<int> poolCodes;
    //Must be a float or else it sinks
    float boat;

    //Player
    public GameObject player;
    
    //Dimensions
    public int length;
    public int width;
    public static int chunkSize = 6;
    public static int megaChunkSize = 64;
    public int renderdistance = 5;
    public bool fpscounter = true;
    //For stopping map gen
    Coroutine cc;

    //Contains every single tile, only have to loop this once to set up chunks
    public Dictionary<coords, TerrainTileValues> grid;
    float deltaTime = 0.0f;
    float fps = 0.0f;

    void Update()
    {

        fps = 1.0f / Time.deltaTime;
            if (fpscounter) {
            if (fps > 30)
                Debug.Log("FPS > 30");
            else if(fps > 25)
            {
                Debug.Log("25 < FPS < 30");
               
            }
            else if(fps > 20)
            {
                Debug.Log("20 < FPS < 25");
            }
            else if (fps > 15)
            {
                Debug.LogWarning("Low FPS: " + (int)fps);
            }
            else
            {
                Debug.LogError("Extremely Low FPS: " + (int)fps);
            }
            
        }
    }
    // Generates map
    void Awake() {

        grid = new Dictionary<coords, TerrainTileValues>();
        cc = StartCoroutine(GenerateMap());
        for (int i = 0; i < foods.Count; i++)
        {
            if (foods[i].GetComponent<TerrainTileValues>().regen)
            {
                StartCoroutine(RegenFood(i));
            }
        }
    }

    //Takes breaks so it doesnt look like it crashed
    IEnumerator GenerateMap()
    {
        float ok = Time.realtimeSinceStartup;

        ContinentPoints(gridObjects[0]);
        FillWater(gridObjects[2]);
        
        foreach (coords c in continentPoints)
        {
            MakeContinents(gridObjects[0], c, 1);
        }

       
        for (int i = 1; i < gridObjects.Count; i++)
          {
              //Creates all the grass and single other blocks
              SetTiles(2);
              yield return new WaitForSeconds(0);
          }
       
        for (int i = 0; i < gridObjects.Count; i++)
          {
              //Puts more blocks around each single block
              BunchSpawns(i);
              yield return new WaitForSeconds(0);
          }
       
        AddDeepWater(deepWater);
        
        for (int i = 0; i < foods.Count; i++)
          {
              AddFood(i);
              yield return new WaitForSeconds(0);
          }

        
        yield return new WaitForSeconds(0);
        //Takes every tile and makes it into chunks defined in chunkSize
        Chunk.MakeChunks(grid);
        
        //Takes every chunk and makes it into megachunks so further increase efficiency in the thread
        MegaChunk.MakeChunks(chunkList);

        Debug.Log("Startup Time: " + (Time.realtimeSinceStartup - ok));
        //Starts making the rendered map in repeat
        StartCoroutine(CreateGrid());
        //Its done generating
        StopCoroutine(cc);
    }
    List<coords> continentPoints = new List<coords>();
    void ContinentPoints(GameObject gg)
    {
        TerrainTileValues u = null;
        if (gg.GetComponent<TerrainTileValues>())
        {
            u = gg.GetComponent<TerrainTileValues>();
        }
        int continents = Random.Range(continentMin, continentMax);
        for(int i = 0; i < continents; i++)
        {
            int x = Random.Range(0, length);
            int y = Random.Range(0, width);
            grid.Add(new coords(x,y), u);
            continentPoints.Add(new coords(x, y));
        }
    }
    void MakeContinents(GameObject gg, coords c, int level)
    {
        int radius = Random.Range(minRadius, maxRadius);
        TerrainTileValues u = null;
        if (gg.GetComponent<TerrainTileValues>())
        {
            u = gg.GetComponent<TerrainTileValues>();
        }
        TerrainTileValues hit;
        TerrainTileValues hit2;
        TerrainTileValues hit3;
        TerrainTileValues hit4;
        if (grid.TryGetValue(new coords(c.x + 1, c.y), out hit))
        {
            //if its the same block increase b
            if (hit.code != 500)
            {
                if(DoBunchChance(u, c.x + 1, c.y, 100 - (100 / radius * level)))
                {
                    MakeContinents(gg, new coords(c.x + 1, c.y), level+1);
                }
            }
        }
        if (grid.TryGetValue(new coords(c.x - 1, c.y), out hit2))
        {
            //if its the same block increase b
            if (hit2.code != 500)
            {
                if (DoBunchChance(u, c.x - 1, c.y, 100 - (100 / radius * level)))
                {
                    MakeContinents(gg, new coords(c.x + 1, c.y), level + 1);
                }

            }
        }
        if (grid.TryGetValue(new coords(c.x, c.y + 1), out hit3))
        {
            //if its the same block increase b
            if (hit3.code != 500)
            {
                if (DoBunchChance(u, c.x, c.y + 1, 100 - (100 / radius * level)))
                {
                    MakeContinents(gg, new coords(c.x + 1, c.y), level + 1);
                }

            }
        }
        if (grid.TryGetValue(new coords(c.x, c.y - 1), out hit4))
        {
            //if its the same block increase b
            if (hit4.code != 500)
            {
                if (DoBunchChance(u, c.x + 1, c.y, 100 - (100 / radius * level)))
                {
                    MakeContinents(gg, new coords(c.x, c.y - 1), level + 1);
                }

            }
        }
    }
    void FillWater(GameObject water)
    {
        TerrainTileValues u = null;
        if (water.GetComponent<TerrainTileValues>())
        {
            u = water.GetComponent<TerrainTileValues>();
        }
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if(!grid.ContainsKey(new coords(x,y)))
                grid.Add(new coords(x, y), u);
            }
        }
    }
    void SetTiles(int layer)
    {
        TerrainTileValues u = null;
        //Gets the block that it will generate
        GameObject gg = gridObjects[layer];
        if (gg.GetComponent<TerrainTileValues>())
        {
            u = gg.GetComponent<TerrainTileValues>();
        }
        //Loops through every tile
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                //If its grass just do it cause 100% chance
                if (layer == 0)
                {
                    grid.Add(new coords(x, y), u);
                }
                else
                {
                    //Otherwise generate based on spawn chance
                    DoBunchChance(u, x, y, u.spawnChance);
                }

            }
        }
    }
    List<int> donex = new List<int>();
    List<int> doney = new List<int>();
    public bool bigwaters = true;
    void BunchSpawns(int layer)
    {
        //Gets the block that it will generate
        TerrainTileValues t = null;
        GameObject gg = gridObjects[layer];

        if (gg.GetComponent<TerrainTileValues>())
        {
            t = gg.GetComponent<TerrainTileValues>();
        }
        
        //Loops through every tile
        for (int tx = 0; tx < length; tx++)
        {
            for (int ty = 0; ty < width; ty++)
            {
                int x;
                int y;
               
                    x = tx;
                    y = ty;
               
                //Generates variables for trygetvalue output
                TerrainTileValues hit;
                TerrainTileValues hit2;
                TerrainTileValues hit3;
                TerrainTileValues hit4;

                //Stores how many objects around for bunch chance
                int b = 0;
                //Try to the right
                if (grid.TryGetValue(new coords(x + 1,y), out hit))
                {
                    //if its the same block increase b
                    if (hit.code == t.code)
                    {
                        b++;

                    }
                }
                //repeat
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
                

                //chance of it making object is based on how many around and bunchchance
                DoBunchChance(t, x, y, t.bunchChance * (t.bunchMultiplier * b));
              
            }

        }
    }
    
    public int waterCode;
    void AddDeepWater(GameObject gg)
    {
        //Gets deep water
        TerrainTileValues k = null;
        if (gg.GetComponent<TerrainTileValues>())
        {
            k = gg.GetComponent<TerrainTileValues>();
        }
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                //8 hits this time for diagnol
                TerrainTileValues hit;
                TerrainTileValues hit2;
                TerrainTileValues hit3;
                TerrainTileValues hit4;
                TerrainTileValues hit5;
                TerrainTileValues hit6;
                TerrainTileValues hit7;
                TerrainTileValues hit8;

                int b = 0;
                
                if (grid.TryGetValue(new coords(x + 1, y), out hit))
                {
                    
                    if (hit.code == waterCode || hit.code == k.code)
                    {
                       
                        b++;
                        
                    }
                }

                if (grid.TryGetValue(new coords(x - 1, y), out hit2))
                {

                    if (hit2.code == waterCode || hit2.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x, y + 1), out hit3))
                {

                    if (hit3.code == waterCode || hit3.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x, y - 1), out hit4))
                {
                    if (hit4.code == waterCode || hit4.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x + 2, y), out hit))
                {

                    if (hit.code == waterCode || hit.code == k.code)
                    {

                        b++;

                    }
                }

                if (grid.TryGetValue(new coords(x - 2, y), out hit2))
                {

                    if (hit2.code == waterCode || hit2.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x, y + 2), out hit3))
                {

                    if (hit3.code == waterCode || hit3.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x, y - 2), out hit4))
                {
                    if (hit4.code == waterCode || hit4.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x + 1, y + 1), out hit5))
                {

                    if (hit5.code == waterCode || hit5.code == k.code)
                    {
                        b++;

                    }
                }

                if (grid.TryGetValue(new coords(x - 1, y - 1), out hit6))
                {

                    if (hit6.code == waterCode || hit6.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x - 1, y + 1), out hit7))
                {

                    if (hit7.code == waterCode || hit7.code == k.code)
                    {
                        b++;

                    }
                }
                if (grid.TryGetValue(new coords(x + 1, y - 1), out hit8))
                {
                    if (hit8.code == waterCode || hit8.code == k.code)
                    {
                        b++;

                    }
                }
                //if all 12 hits are water make it deep water
                if (b == 12)
                {
					grid.Remove(new coords(x,y));
					grid.Add(new coords(x, y),k);
                }
            }
        }
    }
            
    
    void AddFood(int food)
    {
        TerrainTileValues g = null;
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
                   grid.TryGetValue(new coords(x,y), out hit);
                    //Only spawns wheat on grass :)
                    if(hit.code == 500) {
                      DoBunchChanceFood(g, x, y, g.spawnChance);
}





                
            }
        }
    }
    float time = 0;
    IEnumerator RegenFood(int food)
    {
        TerrainTileValues fodder = foods[food].GetComponent<TerrainTileValues>();
        TerrainTileValues g = null;
        GameObject gg = foods[food];
        if (gg.GetComponent<TerrainTileValues>())
        {
            g = gg.GetComponent<TerrainTileValues>();
        }
        while (true)
        {
            int tries = 0;
            yield return new WaitForSeconds(fodder.regenRate);
            bool doot = false;
            while (!doot)
            {
                int x = Random.Range(0, length);
                int y = Random.Range(0, width);
                TerrainTileValues hit;
                grid.TryGetValue(new coords(x, y), out hit);
                //Only spawns wheat on grass :)
                if (hit.code == 500 && !foodList.ContainsKey(new coords(x,y)))
                {
                   
                    foodList.Add(new coords(x, y), g);
                    doot = true;
                }
                else
                {
                    tries++;
                }
                if(foodList.Count > length * width / 50)
                {
                    doot = true;
                }
            }
           
        }
        
        
    }
    //Adds in objects
    bool DoBunchChance(TerrainTileValues t,int x, int y, float chance)
    {
        //If a number from 1-100 is less than the random chance
        if (Random.Range(0.0f, 100.0f) <= chance)
        {
            //Take out the grass and add the new block
            grid.Remove(new coords(x,y));
            grid.Add(new coords(x, y),t);
            return true;
        }
        return false;
    }
    void DoBunchChanceFood(TerrainTileValues t, int x, int y, float chance)
    {
        //If a number from 1-100 is less than the random chance
        if (Random.Range(0.0f, 100.0f) <= chance)
        {
            //Take out the grass and add the new block
            
			foodList.Remove(new coords(x, y));
            foodList.Add(new coords(x, y), t);
        }
    }
    //Threading
    ProcessGrid pg;
    //List of all currently rendered tiles
    Dictionary<coords,GameObject> created = new Dictionary<coords,GameObject>();
    //Renders the grid
	IEnumerator CreateGrid()
    {
        //Infinite, you always wanna be rendering it right :)
        while(true) { 
            //Makes the thread, sets the values
            pg = new ProcessGrid();
            pg.grid = megaChunkList;
            pg.x = player.transform.position.x;
            pg.y = player.transform.position.y;
            pg.render = renderdistance;
            pg.created = created;
            pg.foodList = foodList;
            pg.createdFoods = createdFoods;
            //Starts the thread
            pg.Start();
            //Waits for the thread to finish so we can use the values
            yield return StartCoroutine(pg.WaitFor());
            //The opposite of addTo, everything to be removed this frame
            foreach (KeyValuePair<coords, Chunk> entry in pg.removeFrom)
            {
                //Splits lag over multiple frames
             //   yield return new WaitForSecondsRealtime(0.01f);
                foreach (KeyValuePair<coords, TerrainTileValues> ggg in entry.Value.t)
                {

                    PoolSystem p;
                    int k = poolCodes.FindIndex(d => d == ggg.Value.code);
                    p = pools[k];
                        if (p.code == ggg.Value.code)
                        {
                            GameObject hit;
                            //Removes the object if it currently exists
                            if (created.TryGetValue(new coords(ggg.Key.x, ggg.Key.y), out hit))
                            {
                                created.Remove(new coords(ggg.Key.x, ggg.Key.y));
                                //Recycles the object to be used again
                                p.RecycleObject(hit);
                            }
                            else
                            {
                                Debug.LogWarning("An object wants to be deleted but doesn't exist!!!");
                            }
                        

                    }
                }
            }
            //addTo is everything the thread decides wants to be added to the render
            foreach (KeyValuePair<coords, Chunk> entry in pg.addTo)
            {

              //  yield return new WaitForSecondsRealtime(0.01f);
                //Loops through all the chunks and renders them
                foreach (KeyValuePair<coords, TerrainTileValues> ggg in entry.Value.t)
                {
                    //Finds the right pool system for the object
                    PoolSystem p;
                    int k = poolCodes.FindIndex(d => d == ggg.Value.code);
                    p = pools[k];
                    //if for some reason addTo was already generated
                    if (!created.ContainsKey(new coords(ggg.Key.x, ggg.Key.y)) && !updateList.ContainsKey(new coords(ggg.Key.x, ggg.Key.y)))
                            {
                                //Gets an object from the pool and turns it on
                                GameObject g = p.GetPooledObject();
                                g.SetActive(true);
                                g.transform.position = new Vector3(ggg.Key.x, ggg.Key.y);
                                if(g.transform.parent == null) {
                                    g.transform.parent = transform;
                                }
                                created.Add(new coords(ggg.Key.x, ggg.Key.y), g);
                            }
                            else
                            {
                              //  Debug.LogWarning("To be rendered object already exists!");
                           }
                        
                    
                }
            }
            
            //addTo is everything the thread decides wants to be added to the render
            foreach (KeyValuePair<coords, TerrainTileValues> ggg in pg.addFood)
            {


                //Finds the right pool system for the object
                PoolSystem p;
                int k = poolCodes.FindIndex(d => d == ggg.Value.code);
                Debug.Log(k);
                p = pools[k];
                //if for some reason addTo was already generated
                if (!createdFoods.ContainsKey(new coords(ggg.Key.x, ggg.Key.y)))
                            {
                                //Gets an object from the pool and turns it on
                                GameObject g = p.GetPooledObject();
                                g.SetActive(true);
                                g.transform.position = new Vector3(ggg.Key.x, ggg.Key.y,-0.1f);
                                g.transform.parent = transform;
                                createdFoods.Add(new coords(ggg.Key.x, ggg.Key.y), g);
                            }
                            else
                            {
                                  Debug.LogWarning("To be rendered object already exists!");
                            }
                        
                    
                
            }
            foreach (KeyValuePair<coords, TerrainTileValues> ggg in pg.removeFood)
            {
                //Splits lag over multiple frames
                PoolSystem p;
                int k = poolCodes.FindIndex(d => d == ggg.Value.code);
                p = pools[k];
                GameObject hit;
                        //Removes the object if it currently exists
                        if (createdFoods.TryGetValue(new coords(ggg.Key.x, ggg.Key.y), out hit))
                        {
                            createdFoods.Remove(new coords(ggg.Key.x, ggg.Key.y));
                            //foodList.Remove(new coords(ggg.Key.x, ggg.Key.y));
                            //Recycles the object to be used again
                            if (hit != null)
                            {
                                p.RecycleObject(hit);
                            }
                        }
                        else
                        {
                            Debug.LogError("INFINITE WHEAT");
                            createdFoods.Add(new coords(ggg.Key.x, ggg.Key.y), ggg.Value.gameObject);
                            // foodList.Remove(new coords(ggg.Key.x, ggg.Key.y));
                        }


                    

                
        }
            
            foreach (KeyValuePair<coords,GameObject> ggg in removeFoodList)
            {
                createdFoods.Remove(new coords(ggg.Key.x, ggg.Key.y));
                foodList.Remove(new coords(ggg.Key.x, ggg.Key.y));
                Destroy(ggg.Value);
            }
            removeFoodList = new Dictionary<coords, GameObject>();
            
            yield return new WaitForSeconds(0);
        }
    }
    public static Dictionary<coords, TerrainTileValues> foodList = new Dictionary<coords, TerrainTileValues>();
    public static Dictionary<coords, GameObject> createdFoods = new Dictionary<coords, GameObject>();
    public static Dictionary<coords, GameObject> removeFoodList = new Dictionary<coords, GameObject>();
    public static Dictionary<coords, Chunk> updateList = new Dictionary<coords, Chunk>();
    //Stores every chunk
    public static Dictionary<coords, Chunk> chunkList = new Dictionary<coords, Chunk>();
    //Stores every mega chunk
    public static Dictionary<coords,MegaChunk> megaChunkList = new Dictionary<coords, MegaChunk>();
    static float finalit = 0;
    public class Chunk
    {
        public static coords FindChunkCoords(coords pos)
        {
          
            return new coords(pos.x - (pos.x % chunkSize),pos.y - (pos.y % chunkSize));
        }
        public static void UpdateTile(coords chunk, coords tile, TerrainTileValues tiler)
        {
            Chunk hit;
            if(chunkList.TryGetValue(chunk,out hit))
            {
                TerrainTileValues kk;
                if(hit.t.TryGetValue(tile, out kk))
                {
                    TerrainTileValues t;
                    hit.t.TryGetValue(tile, out t);
                    hit.t.Remove(tile);
                    hit.t.Add(tile, tiler);
                    TerrainTileValues y;
                    hit.t.TryGetValue(tile, out y);
                    MegaChunk.UpdateChunk(MegaChunk.FindChunkCoords(chunk), chunk, hit);
                    if(updateList.ContainsKey(chunk))
                    {
                        updateList.Remove(chunk);
                    }
                    Chunk c;
                    chunkList.TryGetValue(chunk, out c);
                   updateList.Add(chunk, c);
                }
            }
        }
       
        //Sorts all of the tiles into chunks
        public static void MakeChunks(Dictionary<coords, TerrainTileValues> tiles)
        {
            
            chunkList = new Dictionary<coords, Chunk>();
            //Loops all the chunks (laggiest thing in this script!)
         
            foreach (coords key in tiles.Keys)
            {
                
                TerrainTileValues t = tiles[key];


                //Try to add it to a chunk, if you can't make a new chunk for it
                
                    AddToChunk(key, t);
                    


                
            }
            
           
        }
        public static bool AddToChunk(coords c, TerrainTileValues t)
        {
            Chunk cc;
               
            if (chunkList.TryGetValue(new coords(((int)c.x - (c.x % chunkSize)), ((int)c.y - (c.y % chunkSize))),out cc)) {
               
                    cc.t.Add(c, t);
                return true;
            }
            else
            {
                Chunk ok = new Chunk((new coords(((int)c.x - (c.x % chunkSize)), ((int)c.y - (c.y % chunkSize)))));
                ok.t.Add(c, t);
            }
            //sorts through the current chunks
            return false;
        }
        //coords of this chunk
        public coords c;
        //cooords of every tile in the chunk
        public Dictionary<coords, TerrainTileValues> t = new Dictionary<coords, TerrainTileValues>();
        
        public Chunk(coords d)
        {
           
            c = d;
            
            chunkList.Add(d, this);
            
        }
    }
    
    public class MegaChunk
    {
        public static coords FindChunkCoords(coords pos)
        {
            Debug.Log(pos.x - (pos.x % megaChunkSize) + "," + ( pos.y - (pos.y % megaChunkSize)));
            return new coords(pos.x - (pos.x % megaChunkSize), pos.y - (pos.y % megaChunkSize));
        }
        public static void UpdateChunk(coords megaChunk, coords chunk, Chunk chunker)
        {
            MegaChunk hit;
            
            if (megaChunkList.TryGetValue(megaChunk, out hit))
            {
                
                Chunk kk;
                if (hit.t.TryGetValue(chunk, out kk))
                {
                    
                    hit.t.Remove(chunk);
                    hit.t.Add(chunk,chunker);

                }
            }
        }
        //Sorts all of the chunks into megachunks
        public static void MakeChunks(Dictionary<coords, Chunk> tiles)
        {
            
            megaChunkList = new Dictionary<coords, MegaChunk>();
            //Loops all the chunks
            foreach (KeyValuePair<coords, Chunk> entry in tiles)
            {
                //Try to add it to a chunk, if you can't make a new chunk for it
                if (!AddToChunk(entry))
                {
                    new MegaChunk(new coords(entry.Key.x, entry.Key.y));
                    AddToChunk(entry);
                }
            }
        }
        public static bool AddToChunk(KeyValuePair<coords, Chunk> k)
        {
            //sorts through the current megachunks
            foreach (KeyValuePair<coords, MegaChunk> u in megaChunkList)
            {
                //checks for megachunk and if so add it in
                if (u.Key.x > k.Key.x - megaChunkSize && u.Key.x < k.Key.x + megaChunkSize && u.Key.y > k.Key.y - megaChunkSize && u.Key.y < k.Key.y + megaChunkSize)
                {   u.Value.t.Add(k.Key, k.Value);
                    return true;
                }
            }
            return false;
        }
        //Coords of megachunk
        public coords c;
        //All of the chunks and their coords
        public Dictionary<coords, Chunk> t = new Dictionary<coords, Chunk>();
        public MegaChunk(coords d)
        {
            c = d;
            megaChunkList.Add(d, this);
        }
    }
  

    public class coords
    {
        //HashCode and Equals are overrided so you can trygetvalue in dictionary with new coords(x,y)
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
      
    
        //all this really does is store x and y
        public int x;
        public int y;
        public coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    }

