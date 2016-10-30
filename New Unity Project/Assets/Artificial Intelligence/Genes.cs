using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Genes : MonoBehaviour
{
    public int fireFighting;
    public int medical;
    public int cooking;
    public int social;
    public int growing;
    public int combat;
    public int mining;
    public int crafting;

    //Physical Attributes
    public int speed;
    public int sight;
    public int energy;
    public int health;
    public int sprintSpeed;
    public int hitDamage;
    public int goodLooks;
    public int swimSkill;
    public int landSkill;

	public static int age;

    public int gender;

    public Text CarnivorismText;
    public Text SpeedText;
    public Text SightText;
    public Text SprintSpeedText;
    public Text HitDmgText;
    public Text CamoSkillText;
    public Text SwimSkillText;
    public Text LandSkillText;

    public void CreateGenes(Genes mom, Genes dad)
    {
        if (mom != null && dad != null)
        {
            fireFighting = ((mom.fireFighting + dad.fireFighting) / 2) + Random.Range(-1, 2);
            medical = ((mom.medical + dad.medical) / 2) + Random.Range(-1, 2);
            cooking = ((mom.cooking + dad.cooking) / 2) + Random.Range(-1, 2);
            social = ((mom.social + dad.social) / 2) + Random.Range(-1, 2);

            growing = ((mom.growing + dad.growing) / 2) + Random.Range(-1, 2);
            combat = ((mom.combat + dad.combat) / 2) + Random.Range(-1, 2);
            mining = ((mom.mining + dad.mining) / 2) + Random.Range(-1, 2);

            crafting = ((mom.crafting + dad.crafting) / 2) + Random.Range(-1, 2);

            speed = ((mom.speed + dad.speed) / 2) + Random.Range(-1, 2);
            energy = ((mom.energy + dad.energy) / 2) + Random.Range(-1, 2);
            health = ((mom.health + dad.health) / 2) + Random.Range(-1, 2);
            sight = ((mom.sight + dad.sight) / 2) + Random.Range(-1, 2);
            goodLooks = ((mom.goodLooks + dad.goodLooks) / 2) + Random.Range(-1, 2);
            sprintSpeed = ((mom.sprintSpeed + dad.sprintSpeed) / 2) + Random.Range(-1, 2);
            hitDamage = ((mom.hitDamage + dad.hitDamage) / 2) + Random.Range(-1, 2);
            swimSkill = ((mom.swimSkill + dad.swimSkill) / 2) + Random.Range(-1, 2);
            landSkill = ((mom.landSkill + dad.landSkill) / 2) + Random.Range(-1, 2);

            gender = Random.Range(0, 2);
        }
        else
        {
			//Only AIs need these priorities since they decide what they need
        
                fireFighting = Random.Range(0, 20);
                medical = Random.Range(0, 20);
                cooking = Random.Range(0, 20);
                social = Random.Range(0, 20);
                growing = Random.Range(0, 20);
                combat = Random.Range(0, 20);
                mining = Random.Range(0, 20);
                crafting = Random.Range(0, 20);
            

            speed = Random.Range(3, 5);
            energy = Random.Range(0, 100);
            health = Random.Range(0, 100);
            sight = Random.Range(1, 10);
            goodLooks = Random.Range(0, 100);
            sprintSpeed = Random.Range(speed, speed + 10);
            hitDamage = Random.Range(0, 20);
            swimSkill = Random.Range(0, 5);
            landSkill = Random.Range(0, 5);
            gender = Random.Range(0, 2);
        }
		// Change texts only if the parent tag is player
        if (this.gameObject.CompareTag("Player"))
        {
            SpeedText.text = ("Speed: " + speed);
            SightText.text = ("Sight: " + sight);
            SprintSpeedText.text = ("Sprint Speed: " + sprintSpeed);
            HitDmgText.text = ("Hit Damage: " + hitDamage);
            SwimSkillText.text = ("Swimming Skill: " + swimSkill);
            LandSkillText.text = ("Land Skill: " + landSkill);
        }

    }

    public float needFood;
    public float needWater;
    public float needRest;
    public float needBreed;
    public List<string> makePriorityList(SurvivalStats s, bool inCombat)
    {
        if (!inCombat)
        {
            needFood = ((100.0f / (Mathf.Ceil(s.h.currentHunger * 100))) - 1);
                needWater = ((100.0f / (Mathf.Ceil(s.t.currentThirst * 100))) - 1);
                needBreed = (s.age / 10);
                needRest = s.e.currentEnergy < 25 ? 0 : 4;
                Dictionary<string, float> returnDictionary = new Dictionary<string, float>();

                returnDictionary.Add("needWater", needWater);
                returnDictionary.Add("needFood", needFood);
                returnDictionary.Add("needBreed", needBreed);
                returnDictionary.Add("needRest", needRest);
                List<string> returnList = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    string highest = null;
                    int currentHighest = -10000;
                    foreach (KeyValuePair<string, float> entry in returnDictionary)
                    {
                        if (entry.Value > currentHighest)
                        {
                            currentHighest = (int)entry.Value;
                            highest = entry.Key;

                        }
                    }
                    if (highest != null)
                    {

                        returnDictionary.Remove(highest);
                        returnList.Add(highest);
                    }

                }
                return returnList;
            
        }
        else
        {
            //combat needs
        }
        return null;
    }
}