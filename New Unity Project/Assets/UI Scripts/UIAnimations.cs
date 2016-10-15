using UnityEngine;
using System.Collections;

public class UIAnimations : MonoBehaviour {

	public GameObject GridGO;
	public GameObject StatsPanel;
	public Animator StatsPanelAnimator;
	public GameObject PausePanel;
	public Animator PausePanelAnimator;
	public GameObject InventoryPanel;
	public Animator InvPanelAnimator;

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
		Invoke ("TimeScaleZero", 1);
	}
	public void OnClickPlayButton () {
		Time.timeScale = 1;
		PausePanelAnimator.Play ("PausePanelAnimationReversed");
		Invoke ("PanelDisable", 1);
	}
	public void PanelDisable() {
		PausePanel.SetActive (false);
		InventoryPanel.SetActive (false);
	}
	public void TimeScaleZero () {
		Time.timeScale = 0;
	}
	public void OnClickInv () {
		InventoryPanel.SetActive (true);
		InvPanelAnimator.Play ("PausePanelAnimation");
		Invoke ("TimeScaleZero", 1);
	}
	public void OnClickInvBack () {
		Time.timeScale = 1;
		InvPanelAnimator.Play ("PausePanelAnimationReversed");
		Invoke ("PanelDisable", 1);
	}
}
