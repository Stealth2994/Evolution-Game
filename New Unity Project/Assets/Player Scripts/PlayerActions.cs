using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PlayerActions : MonoBehaviour {
    public Block nedgeBlock;
    public Block eedgeBlock;
    public Block sedgeBlock;
    public Block wedgeBlock;
    public Block blcornerBlock;
    public Block tlcornerBlock;
    public Block trcornerBlock;
    public Block brcornerBlock;
    public Block centerBlock;
    public Camera c;
    // Use this for initialization
    void Start () {
	    
	}
    int startx;
    int currentx;
    int starty;
    int currenty;
    Dictionary<GenerateGrid.coords, GameObject> createdBlocks = new Dictionary<GenerateGrid.coords, GameObject>();
    Dictionary<GenerateGrid.coords, Block> buildArea = new Dictionary<GenerateGrid.coords, Block>();
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Select();
        }
        else if(Input.GetMouseButton(0)) {
            Select();
            Build();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            buildArea = new Dictionary<GenerateGrid.coords, Block>();
            Build();
        }
    }
    Vector2 one = Vector2.zero;
    Vector2 two = Vector2.zero;
    int current = 0;
    void Select () {
        RaycastHit hit;
        Vector3 pos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin,ray.direction,Color.green,2);
        
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.transform.position;
            
        }
        Debug.Log(pos);
        switch(current)
        {

        }
        if (Input.GetMouseButtonDown(0))
        {
            buildArea = new Dictionary<GenerateGrid.coords, Block>();
            startx = Mathf.RoundToInt(pos.x);
            starty = Mathf.RoundToInt(pos.y);
        }
        buildArea = new Dictionary<GenerateGrid.coords, Block>();
        int currentx = Mathf.RoundToInt(pos.x);
            int currenty = Mathf.RoundToInt(pos.y);
            int lessX;
            int greatX;
            if(startx < currentx)
            {
                lessX = startx;
                greatX = currentx;
            }
             else
            {
                lessX = currentx;
                greatX = startx;
            }

            int lessY;
            int greatY;
            if (starty < currenty)
            {
                lessY = starty;
                greatY = currenty;
            }
            else
            {
                lessY = currenty;
                greatY = starty;
            }
      //  Debug.Log(lessX + "," + greatX + ":" + lessY + "," + greatY);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(lessX, lessY))))
                buildArea.Add(new GenerateGrid.coords(lessX, lessY), blcornerBlock);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(lessX, greatY))))
            buildArea.Add(new GenerateGrid.coords(lessX, greatY), tlcornerBlock);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(greatX, lessY))))
            buildArea.Add(new GenerateGrid.coords(greatX, lessY), brcornerBlock);
        if (!buildArea.ContainsKey((new GenerateGrid.coords(greatX, greatY))))
            buildArea.Add(new GenerateGrid.coords(greatX, greatY), trcornerBlock);
            for(int i = lessX; i < greatX; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(i, lessY))))
                buildArea.Add(new GenerateGrid.coords(i, lessY), sedgeBlock);
            }
            for (int i = lessY; i < greatY; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(lessX,i))))
                buildArea.Add(new GenerateGrid.coords(lessX, i), wedgeBlock);
            }
            for (int i = lessY; i < greatY; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(greatX, i))))
                buildArea.Add(new GenerateGrid.coords(greatX, i), eedgeBlock);
            }
            for (int i = lessX; i < greatX; i++)
            {
            if (!buildArea.ContainsKey((new GenerateGrid.coords(i, greatY))))
                buildArea.Add(new GenerateGrid.coords(i, greatY), nedgeBlock);
            }

            for (int x = lessX; x < greatX ; x++)
            {
                for(int y = lessY; y < greatY; y++)
                {
                if (!buildArea.ContainsKey((new GenerateGrid.coords(x, y))))
                    buildArea.Add(new GenerateGrid.coords(x, y), trcornerBlock);
                }
            
        }
	}
    void Build()
    {
        foreach(KeyValuePair<GenerateGrid.coords,Block> entry in buildArea)
        {
            if(!createdBlocks.ContainsKey(entry.Key))
            {
                GameObject b = Instantiate(entry.Value.gameObject,new Vector2(entry.Key.x,entry.Key.y),Quaternion.identity) as GameObject;
                createdBlocks.Add(entry.Key, b);
            }
        }
        List<GenerateGrid.coords> entrycoords = new List<GenerateGrid.coords>();
        foreach (KeyValuePair<GenerateGrid.coords, GameObject> entry in createdBlocks)
        {
            entrycoords.Add(entry.Key);
        }
        foreach(GenerateGrid.coords c in entrycoords)
        {
            
            if (!buildArea.ContainsKey(c))
            {
                Destroy(createdBlocks[c]);
                createdBlocks.Remove(c);
            }
        }
    }
}
