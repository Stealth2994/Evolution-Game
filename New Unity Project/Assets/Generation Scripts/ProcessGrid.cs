using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProcessGrid : ThreadedJob {
    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> grid;
    public float x;
    public float y;
    public int render;
    public Dictionary<GenerateGrid.coords, GameObject> created;

    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> addTo = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> removeFrom = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    protected override void ThreadFunction()
    {
        foreach (KeyValuePair<GenerateGrid.coords,GenerateGrid.Chunk> entry in grid)
        {

     
            if (entry.Key.x > x - render && entry.Key.x < x + render && entry.Key.y > y - render && entry.Key.y  < y + render)
            {
                
                GameObject hit;
                if ((created.TryGetValue(GenerateGrid.coords.withCreatedCoords(entry.Key.x, entry.Key.y), out hit)))
                {
          

                }
                else
                {
                    entry.Value.alive = true;
                    addTo.Add(entry.Key, entry.Value);
                }
            }
            else
            {
                GameObject hit;
                if (created.TryGetValue(GenerateGrid.coords.withCreatedCoords(entry.Key.x, entry.Key.y), out hit))
                {
                    entry.Value.alive = false;
                    removeFrom.Add(entry.Key, entry.Value);
                }
                
            }
            
        }
    }
}
 