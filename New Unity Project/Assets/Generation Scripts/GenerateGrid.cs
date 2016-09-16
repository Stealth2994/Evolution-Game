using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateGrid : MonoBehaviour {
    public List<GameObject> gridObjects;
    public int length;
    public int width;
    public List<GameObject> grid;
    // Use this for initialization
    void Start () {
        grid = new List<GameObject>();
        for(int i = 0; i < gridObjects.Count; i++)
        {
            SetTiles(i);
        }
        CreateGrid();
    }
	void SetTiles(int layer)
    {
        GameObject gg = gridObjects[layer];
        for (int x = 10; x  > 0; x++)
        {
            for (int y = 10; y  > 0; y++)
            {

                    if (gg.GetComponent<TerrainTileValues>())
                    {
                        TerrainTileValues t = gg.GetComponent<TerrainTileValues>();
                    if (layer == 0)
                    {
                        grid.Add(gg);
                    }
                    else
                    {
                        if (Random.Range(0, 100) <= t.spawnChance)
                        {
                            grid.RemoveAt(x * y);
                            grid.Insert(x * y, gg);


                        }
                    }
                             
                                }
                           

                             
                            
                                            
                    

                


            }
        }
    }
	void CreateGrid()
    {
        int tempX = 0;
        int tempY = 0;
        foreach (GameObject g in grid)
        {
            GameObject go = Instantiate(g, new Vector3(tempX,tempY, 0), Quaternion.identity) as GameObject;
            go.transform.parent = transform;
            tempX++;
            if (tempX >= length)
            {
                tempX = 0;
                tempY++;
            }
        }
    }
}
