using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNight : MonoBehaviour {

	bool TimeIsZero;
	public Text day;
	public Text month;
	public Text year;
	string monthString;
	string daySuffix;
	int daya;
	int dayb;
	public int days = 1;
	public int months = 0;
	public int years = 2000;
	public SpriteRenderer DarknessImage;
	public float secondsInHalfDay = 30f;
	[Range(-1,1)]
	public float currentTimeOfDay = -1f;
	public float timeMultiplier = 1f;
	public bool reachedFullDay;
	public Text ageText;

	void Start () {
		//call the first month so it isnt 0 :)
		CallNewMonth ();
		// Since the month changes 5 times in a single daynight cycle do /75 
		InvokeRepeating ("DayUp", secondsInHalfDay / 225, secondsInHalfDay / 225);
		//seconds in full day is = seconds in half day * 2.
		InvokeRepeating ("CallNewMonth", (secondsInHalfDay * 2) / 15, (secondsInHalfDay * 2) / 15);
	}

	void DayUp () {
		day.text = ("" + days + daySuffix);
		//if the day is equal to 30, it is a new month, so set it to 0.
		if (days == 30) {
			days = 0;
		}
		//add days
		days++;
		//modulo to check the suffix of the day
		daya = days % 10;
		dayb = days % 100;
		if (daya == 1 && dayb != 11) {
			daySuffix = ("st of");
		} else if (daya == 2 && dayb != 12) {
			daySuffix = ("nd of");
		} else if (daya == 3 && dayb != 13) {
			daySuffix = ("rd of");
		} else
			daySuffix = ("th of");
	}

	void CallNewMonth() {
		//switch cases for month names

		months++;
		switch (months) {
		case 1:
			monthString = ("January");
			break;
		case 2:
			monthString = ("February");
			break;
		case 3:
			monthString = ("March");
			break;
		case 4:
			monthString = ("April");
			break;
		case 5:
			monthString = ("May");
			break;
		case 6:
			monthString = ("June");
			break;
		case 7:
			monthString = ("July");
			break;
		case 8:
			monthString = ("August");
			break;
		case 9: 
			monthString = ("September");
			break;
		case 10:
			monthString = ("October");
			break;
		case 11:
			monthString = ("November");
			break;
		case 12:
			monthString = ("December");
			break;
		}
		month.text = (monthString);
	}

	void Update() {
		if (TimeIsZero == true) {
			currentTimeOfDay += (Time.deltaTime / secondsInHalfDay) * timeMultiplier;
			if (currentTimeOfDay >= 1) {
				TimeIsZero = false;
			}
		} else if (TimeIsZero == false) {
			currentTimeOfDay -= (Time.deltaTime / secondsInHalfDay) * timeMultiplier;
			if (currentTimeOfDay <= -1) {
				TimeIsZero = true;
			}
		}

		if (months == 13) {
			years++;
			monthString = ("January");
			year.text = ("" + years);
			month.text = (monthString);
			months = 1;
			Genes.age++;	//Everyone's age goes up by 1 every month!
			ageText.text = ("" + Genes.age);
		}
			
		if (currentTimeOfDay >= 0f) {
			DarknessImage.color = new Color (0, 0, 0, currentTimeOfDay);
		}
		//change calendar text on canvas
	}
}
