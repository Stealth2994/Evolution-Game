using UnityEngine;
using System.Collections;

public class UIAnimations : MonoBehaviour {

	public GameObject GridGO;
	public GameObject StatsPanel;
	public Animator StatsPanelAnimator;
	public GameObject PausePanel;
	public Animator PausePanelAnimator;

	public void OnClickStatsHamburger () {
		StatsPanel.SetActive (true);
		StatsPanelAnimator.Play ("StatsPanelAnimation");
	}
	public void OnClickStatsBack () {
		StatsPanelAnimator.Play ("StatsPanelAnimationReversed");
		Invoke ("StatsPanelDisable", 1);
	}
	public void StatsPanelDisable() {
		StatsPanel.SetActive (false);
	}

	public void OnClickPause () {
		PausePanel.SetActive (true);
		PausePanelAnimator.Play ("PausePanelAnimation");
	}
	public void OnClickPlayButton () {
		PausePanelAnimator.Play ("PausePanelAnimationReversed");
		Invoke ("PausePanelDisable", 1);
	}
	public void PausePanelDisable() {
		PausePanel.SetActive (false);
	}
}
