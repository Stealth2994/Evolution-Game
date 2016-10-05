using UnityEngine;
using System.Collections;

public class Genes : MonoBehaviour {
    //Stores all of the genes/traits of the current AI

    //Priority Weights
    public int foodPriority;
    public int waterPriority;
    public int breedPriority;
    public int restPriority;

    //Trait Weights
    public int fleeLevel;
    public int fightLevel;
    public int camouflageLevel;

    //Physical Attributes
    public int speed;
    public int energy;
    public int health;
    public int sprintSpeed;
    public int hitDamage;
    public int camouflageSkill;
    public int swimSkill;
    public int landSkill;
}
