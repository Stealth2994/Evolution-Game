using UnityEngine;
using System.Collections;

public class SurvivalStats : MonoBehaviour {
    public int health = 100;
	public float hunger;
    public float thirst;
    public float rest;
    public int age = 0;
    public int gender = 0;
    public Hunger h;
    public Thirst t;
    public Energy e;
	void Start () {
     
        h = GetComponent<Hunger>();
        t = GetComponent<Thirst>();
        e = GetComponent<Energy>();
    }
    void Update()
    {
        hunger = h.currentHunger * 100;
        thirst = t.currentThirst * 100;
        rest = e.currentEnergy * 100;
    }
}
