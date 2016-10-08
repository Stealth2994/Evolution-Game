using UnityEngine;
using System.Collections;

public class SurvivalStats : MonoBehaviour {
    public int health;
	public float hunger;
    public float thirst;
    public float rest;
    public int age;
    public int gender;

	void Start () {
		hunger = GetComponent<Hunger> ().currentHunger * 100;
		thirst = GetComponent<Thirst> ().currentThirst * 100;
		rest = GetComponent<Energy> ().currentEnergy * 100;
	}
}
