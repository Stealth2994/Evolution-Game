using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProcessGrid : ThreadedJob
{
    public Dictionary<GenerateGrid.coords, GenerateGrid.MegaChunk> grid;
    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> chunkGrid = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    public float x;
    public float y;
    public int render;
    public Dictionary<GenerateGrid.coords, GameObject> created;

    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> addTo = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> removeFrom = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    protected override void ThreadFunction()
    {
        foreach (KeyValuePair<GenerateGrid.coords, GenerateGrid.MegaChunk> entry in grid)
        {


            if (entry.Key.x > x - render - 64 && entry.Key.x < x + render + 64 && entry.Key.y > y - render - 64 && entry.Key.y < y + render + 64)
            {
                foreach (KeyValuePair<GenerateGrid.coords, GenerateGrid.Chunk> chunk in entry.Value.t)
                {
                    if (chunk.Key.x > x - render && chunk.Key.x < x + render && chunk.Key.y > y - render && chunk.Key.y < y + render)
                    {

                        GameObject hit;
                        if ((created.TryGetValue(new GenerateGrid.coords(chunk.Key.x, chunk.Key.y), out hit)))
                        {


                        }
                        else
                        {
                            addTo.Add(chunk.Key, chunk.Value);
                        }
                    }
                    else
                    {
                        GameObject hit;
                        if (created.TryGetValue(new GenerateGrid.coords(chunk.Key.x, chunk.Key.y), out hit))
                        {
                            removeFrom.Add(chunk.Key, chunk.Value);
                        }


                    }
                }
            }

        }

    }
}
 