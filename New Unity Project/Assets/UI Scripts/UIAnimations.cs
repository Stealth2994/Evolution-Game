using UnityEngine;
using System.Collections;

public class UIAnimations : MonoBehaviour {

	public GameObject GridGO;
	public GameObject StatsPanel;
	public Animator StatsPanelAnimator;
	public GameObject BPPanel;
	public Animator BPPanelAnimator;
	public GameObject PausePanel;
	public Animator PausePanelAnimator;
	public GameObject InventoryPanel;
	public Animator InvPanelAnimator;
	public Canvas mainCanvas;
	public Canvas buildCanvas;

	public void HideCanvas() {
		mainCanvas.enabled = false;
		buildCanvas.enabled = true;
	}
	public void DoneButton() {
		buildCanvas.enabled = false;
		mainCanvas.enabled = true;
	}
	public void OnClickBPHamburger () {
		BPPanel.SetActive (true);
		BPPanelAnimator.Play ("BPPanelAnimation");
	}
	public void OnClickBPBack () {
		BPPanelAnimator.Play ("BPPanelReversed");
		Invoke ("StatsPanelDisable", 1);
	}
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
		BPPanel.SetActive (false);
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
