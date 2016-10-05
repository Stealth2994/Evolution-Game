using UnityEngine;
using System.Collections.Generic;

public class Genes : MonoBehaviour {
    //Stores all of the genes/traits of the current AI
    //NOTE: All numbers are from 0-100, unless otherwise noted
    //Priority Weights
    public int foodPriority;
    public int waterPriority;
    public int breedPriority;
    public int restPriority;

    //Trait Weights
    public int fleeLevel;
    public int fightLevel;

    //Willingness of non-dependant things
    public int doCamouflage;

    //Physical Attributes
    public int speed;
    public int energy;
    public int health;
    public int sprintSpeed;
    public int hitDamage;
    public int camouflageSkill;
    public int swimSkill;
    public int landSkill;

    public void CreateGenes(Genes mom, Genes dad)
    {
        if (mom != null && dad != null)
        {
            foodPriority = ((mom.foodPriority + dad.foodPriority) / 2) + Random.Range(-1, 1);
            waterPriority = ((mom.waterPriority + dad.waterPriority) / 2) + Random.Range(-1, 1);
            breedPriority = ((mom.breedPriority + dad.breedPriority) / 2) + Random.Range(-1, 1);
            restPriority = ((mom.restPriority + dad.restPriority) / 2) + Random.Range(-1, 1);

            fleeLevel = ((mom.fleeLevel + dad.fleeLevel) / 2) + Random.Range(-1, 1);
            fightLevel = ((mom.fightLevel + dad.fightLevel) / 2) + Random.Range(-1, 1);

            doCamouflage = ((mom.doCamouflage + dad.doCamouflage) / 2) + Random.Range(-1, 1);

            speed = ((mom.speed + dad.speed) / 2) + Random.Range(-1, 1);
            energy = ((mom.energy + dad.energy) / 2) + Random.Range(-1, 1);
            health = ((mom.health + dad.health) / 2) + Random.Range(-1, 1);
            sprintSpeed = ((mom.sprintSpeed + dad.sprintSpeed) / 2) + Random.Range(-1, 1);
            hitDamage = ((mom.hitDamage + dad.hitDamage) / 2) + Random.Range(-1, 1);
            camouflageSkill = ((mom.camouflageSkill + dad.camouflageSkill) / 2) + Random.Range(-1, 1);
            swimSkill = ((mom.swimSkill + dad.swimSkill) / 2) + Random.Range(-1, 1);
            landSkill = ((mom.landSkill + dad.landSkill) / 2) + Random.Range(-1, 1);
        }
        else
        {
            foodPriority = Random.Range(0,100);
            waterPriority = Random.Range(0, 100);
            breedPriority = Random.Range(0, 100);
            restPriority = Random.Range(0, 100);

            fleeLevel = Random.Range(0, 100);
            fightLevel = Random.Range(0, 100);

            doCamouflage = Random.Range(0, 100);

            speed = Random.Range(0, 100);
            energy = Random.Range(0, 100);
            health = Random.Range(0, 100);
            sprintSpeed = Random.Range(0, 100);
            hitDamage = Random.Range(0, 100);
            camouflageSkill = Random.Range(0, 100);
            swimSkill = Random.Range(0, 100);
            landSkill = Random.Range(0, 100);
        }
    }
    public Dictionary<int,int> makePriorityList(SurvivalStats s)
    {
        return null;
    }
}
