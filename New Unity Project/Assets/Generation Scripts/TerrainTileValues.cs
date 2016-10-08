using UnityEngine;
using System.Collections;

public class TerrainTileValues : MonoBehaviour {
    public float spawnChance = 100;
    public int sizeX = 1;
    public int sizeY = 1;
    public int bunchChance = 50;
    public float bunchMultiplier = 1.5f;
    public int code = 1000;
    public bool alive = false;
    public int unique;
    public float regenRate = 0;
    public float speed = 1;
    public bool regen = false;
    public bool food = false;
}
