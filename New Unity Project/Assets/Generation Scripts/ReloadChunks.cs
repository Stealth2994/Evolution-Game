using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//This class is based off a really confusing thread maker but the gist of it is that it runs the thread that calculates what chunks should be rendered when started :)
public class ReloadChunks : ThreadedJob
{
    //Put these variables into the machine
    public Dictionary<GenerateGrid.coords, GenerateGrid.MegaChunk> grid;
    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> chunkGrid = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    public float x;
    public float y;
    public int render;
    public Dictionary<GenerateGrid.coords, GameObject> created;

    //These variables will contain computed info once the thread is done
    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> addTo = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    public Dictionary<GenerateGrid.coords, GenerateGrid.Chunk> removeFrom = new Dictionary<GenerateGrid.coords, GenerateGrid.Chunk>();
    protected override void ThreadFunction()
    {
        //Loop through all megachunks
        foreach (KeyValuePair<GenerateGrid.coords, GenerateGrid.MegaChunk> entry in grid)
        {

            //If the megachunk is in range
            if (entry.Key.x > x - render - 64 && entry.Key.x < x + render + 64 && entry.Key.y > y - render - 64 && entry.Key.y < y + render + 64)
            {
                //loop through all of its chunks
                foreach (KeyValuePair<GenerateGrid.coords, GenerateGrid.Chunk> chunk in entry.Value.t)
                {
                    //if the chunk is in range
                    if (chunk.Key.x > x - render && chunk.Key.x < x + render && chunk.Key.y > y - render && chunk.Key.y < y + render)
                    {
                        //add it
                        GameObject hit;
                        if ((created.TryGetValue(new GenerateGrid.coords(chunk.Key.x, chunk.Key.y), out hit)))
                        {


                        }
                        else
                        {
                            addTo.Add(chunk.Key, chunk.Value);
                        }
                    }
                    //otherwise delete it if it exists
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
