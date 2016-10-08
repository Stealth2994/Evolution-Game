using UnityEngine;
using System.Collections;

public class SurvivalStats : MonoBehaviour {
    public int health = 100;
	public float hunger;
    public float thirst;
    public float rest;
    public int age = 0;
    public int gender = 0;
    Hunger h;
    Thirst t;
    Energy e;
	void Start () {
     
        h = GetComponent<Hunger>();
        t = GetComponent<Thirst>();
        e = GetComponent<Energy>();
    }
    void Update()
    {
        hunger = GetComponent<Hunger>().currentHunger * 100;
        thirst = GetComponent<Thirst>().currentThirst * 100;
        rest = GetComponent<Energy>().currentEnergy * 100;
    }
}
