using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateGrid : MonoBehaviour {
    public List<GameObject> gridObjects;
    public List<GameObject> foods;
    public int length;
    public int width;
    public Dictionary<coords, GameObject> grid;
    bool done = false;
    // Use this for initialization
    void Update () {
        if (!done)
        {
            grid = new Dictionary<coords, GameObject>();
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
            CreateGrid();
            done = true;
        }
    }
    
	void SetTiles(int layer)
    {
        GameObject gg = gridObjects[layer];
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                
                if (gg.GetComponent<TerrainTileValues>())
                {
                    TerrainTileValues t = gg.GetComponent<TerrainTileValues>();
                    if (layer == 0)
                    {
                        grid.Add(new coords(x,y,1), gg);
                    }
                    else
                    {
                        DoBunchChance(t, x, y, gg, t.spawnChance);


                    }
                }
            }
        }
    }
    void BunchSpawns(int layer)
    {
        GameObject gg = gridObjects[layer];
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if (gg.GetComponent<TerrainTileValues>())
                {
                    TerrainTileValues t = gg.GetComponent<TerrainTileValues>();
                    GameObject hit;
                    GameObject hit2;
                    GameObject hit3;
                    GameObject hit4;


                    int b = 0;

                    if (grid.TryGetValue(coords.withCoords(x + 1, y), out hit))
                    {

                        if (hit.GetComponent<TerrainTileValues>().code == t.code)
                        {
                            b++;

                        }
                    }
                    if (grid.TryGetValue(coords.withCoords(x - 1, y), out hit2))
                    {

                        if (hit2.GetComponent<TerrainTileValues>().code == t.code)
                        {
                            b++;

                        }
                    }
                    if (grid.TryGetValue(coords.withCoords(x, y + 1), out hit3))
                    {

                        if (hit3.GetComponent<TerrainTileValues>().code == t.code)
                        {
                            b++;

                        }
                    }
                    if (grid.TryGetValue(coords.withCoords(x, y - 1), out hit4))
                    {
                        if (hit4.GetComponent<TerrainTileValues>().code == t.code)
                        {
                            b++;

                        }
                    }

                        DoBunchChance(t, x, y, gg, t.bunchChance * t.bunchMultiplier * b);
                    
                }
            }
        }
    }
    int grasscode = 500;
    void AddFood(int food)
    {
        GameObject gg = foods[food];
        for (int x = 0; x < length; x++)
        {
            for (int y = 0; y < width; y++)
            {

                if (gg.GetComponent<TerrainTileValues>())
                {
                    TerrainTileValues t = gg.GetComponent<TerrainTileValues>();
                    GameObject hit;
                    if (grid.TryGetValue(coords.withCoords(x, y), out hit))
                    {

                    }
                        else
                    {
                        DoBunchChance(t, x, y, gg, t.spawnChance);
                    }
                       


                    
                }
            }
        }
    }
    
    void DoBunchChance(TerrainTileValues t,int x, int y, GameObject gg, float chance)
    {
        if (Random.Range(0.0f, 100.0f) <= chance)
        {
            grid.Remove(coords.withBaseCoords(x, y));
            grid.Remove(coords.withCoords(x, y));
            grid.Add(new coords(x, y,0), gg);
        }
    }
	void CreateGrid()
    {
        foreach (KeyValuePair<coords, GameObject> entry in grid)
        {
            // do something with entry.Value or entry.Key

            GameObject go = Instantiate(entry.Value, new Vector3(entry.Key.x, entry.Key.y, 0), Quaternion.identity) as GameObject;
            go.transform.parent = transform;
        }
        }
    public static List<coords> coorder = new List<coords>();
    public static List<coords> basecoorder = new List<coords>();
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
        }
    }
    }

