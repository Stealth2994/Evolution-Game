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
	public float secondsInFullDay = 120f;
	[Range(-1,1)]
	public float currentTimeOfDay = -1f;
	public float timeMultiplier = 1f;
	public bool reachedFullDay;

	void Start () {
		InvokeRepeating ("DayUp", secondsInFullDay / 15, secondsInFullDay / 15);
	}

	void DayUp () {
		if (days == 30) {
			days = 0;
		}
		days++;
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
	}

	void Update() {

		if (TimeIsZero == true) {
			currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
			if (currentTimeOfDay >= 1) {
				months++;
				CallNewMonth ();
				TimeIsZero = false;
			}
		} else if (TimeIsZero == false) {
			currentTimeOfDay -= (Time.deltaTime / secondsInFullDay) * timeMultiplier;
			if (currentTimeOfDay <= -1) {
				months++;
				CallNewMonth ();
				days = 1;
				TimeIsZero = true;
			}
		}
		if (months == 12) {
			months = 0;
			years++;
		}
		if (currentTimeOfDay >= 0f) {
			DarknessImage.color = new Color (0, 0, 0, currentTimeOfDay);
		}
		day.text = ("" + days + daySuffix);
		month.text = (monthString);
		year.text = ("" + years);
	}
}
