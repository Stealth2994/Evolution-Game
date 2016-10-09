using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayNight : MonoBehaviour {

	bool TimeIsZero;
	public Text Clock;
	public SpriteRenderer DarknessImage;
	public float secondsInFullDay = 120f;
	[Range(-1,1)]
	public float currentTimeOfDay = -1f;
	public float timeMultiplier = 1f;
	public float mins;
	public float hours;

	void Update() {
		if (TimeIsZero == true) {
			currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
			if (currentTimeOfDay >= 1) {
				TimeIsZero = false;
			}
		} else if (TimeIsZero == false) {
			currentTimeOfDay -= (Time.deltaTime / secondsInFullDay) * timeMultiplier;
			if (currentTimeOfDay <= -1) {
				TimeIsZero = true;
			}
		}
		if (currentTimeOfDay >= 0f) {
			DarknessImage.color = new Color (0, 0, 0, currentTimeOfDay);
		} 
		Clock.text = ("" + hours + ":" + mins);
	}
}
