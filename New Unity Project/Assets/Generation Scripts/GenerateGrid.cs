using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateGrid : MonoBehaviour {
    public List<GameObject> gridObjects;
    public int length;
    public int width;
    public Dictionary<coords, GameObject> grid;
    // Use this for initialization
    void Start () {
        grid = new Dictionary<coords, GameObject>();
        for(int i = 0; i < gridObjects.Count; i++)
        {
            SetTiles(i);
        }
        CreateGrid();
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
                        Debug.Log(x + "," +  y);
                        grid.Add(new coords(x,y), gg);
                    }
                    else
                    {
                        if (Random.Range(0, 100) <= t.spawnChance)
                        {
                            grid.Remove(new coords(x, y));
                            grid.Add(new coords(x, y), gg);
                        }
                    }
                }
            }
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
   public class coords
    {
        public int x;
        public int y;
        public coords(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    }

