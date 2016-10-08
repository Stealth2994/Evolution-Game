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
    public int carnivorism;
    //Willingness of non-dependant things
    public int doCamouflage;

    //Physical Attributes
    public int speed;
    public int sight;
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
            carnivorism = ((mom.carnivorism + dad.carnivorism) / 2) + Random.Range(-1, 1);
            fightLevel = ((mom.fightLevel + dad.fightLevel) / 2) + Random.Range(-1, 1);

            doCamouflage = ((mom.doCamouflage + dad.doCamouflage) / 2) + Random.Range(-1, 1);

            speed = ((mom.speed + dad.speed) / 2) + Random.Range(-1, 1);
            energy = ((mom.energy + dad.energy) / 2) + Random.Range(-1, 1);
            health = ((mom.health + dad.health) / 2) + Random.Range(-1, 1);
            sight = ((mom.sight + dad.sight) / 2) + Random.Range(-1, 1);
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
            carnivorism = Random.Range(0, 100);
            fightLevel = Random.Range(0, 100);

            doCamouflage = Random.Range(0, 100);

            speed = Random.Range(0, 100);
            energy = Random.Range(0, 100);
            health = Random.Range(0, 100);
            sight = Random.Range(0, 10);
            sprintSpeed = Random.Range(0, 100);
            hitDamage = Random.Range(0, 100);
            camouflageSkill = Random.Range(0, 100);
            swimSkill = Random.Range(0, 100);
            landSkill = Random.Range(0, 100);
        }
    }
    public List<string> makePriorityList(SurvivalStats s, bool inCombat)
    {
        if(!inCombat)
        {
            float needFood = (100.0f / s.hunger) * foodPriority;
            float needWater = (100.0f / s.thirst) * waterPriority;
            //  float needBreed = (s.age - 1) * breedPriority;
            //  float needRest = (100.0f / s.rest) * restPriority;
            Dictionary<string, float> returnDictionary = new Dictionary<string, float>();
            
            returnDictionary.Add("needWater", needWater);
            returnDictionary.Add("needFood", needFood);
            //  returnDictionary.Add("needBreed", needBreed);
            // returnDictionary.Add("needRest", needRest);
            List<string> returnList = new List<string>();
            for (int i = 0; i < 2; i++)
            {
                string highest = null;
                int currentHighest = -10000;
                foreach (KeyValuePair<string, float> entry in returnDictionary)
                {
                    if(entry.Value > currentHighest)
                    {
                        currentHighest = (int)entry.Value;
                        highest = entry.Key;
                        
                    }
                }
                if (highest != null)
                {
                    returnDictionary.Remove(highest);
                }
                returnList.Add(highest);
            }
            return returnList;
        }
        else
        {
            float flee = (100 / fleeLevel) * health;
        }
        return null;
    }
}
