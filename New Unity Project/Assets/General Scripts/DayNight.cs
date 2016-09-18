using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNight : MonoBehaviour {

	bool TimeIsZero;
	public Image DarknessImage;
	public float secondsInFullDay = 120f;
	[Range(0,1)]
	public float currentTimeOfDay = 0;
	public float timeMultiplier = 1f;

	void Update() {

		if (TimeIsZero == true) {
			currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
			if (currentTimeOfDay >= 1) {
				TimeIsZero = false;
			}
		} else if (TimeIsZero == false) {
			currentTimeOfDay -= (Time.deltaTime / secondsInFullDay) * timeMultiplier;
			if (currentTimeOfDay <= 0) {
				TimeIsZero = true;
			}
		}
		if (currentTimeOfDay <= 0.3f) {
			timeMultiplier = 0.5f;
		} else {
			timeMultiplier = 1f;
		}
			DarknessImage.color = new Color (0, 0, 0, currentTimeOfDay);
	}
}
