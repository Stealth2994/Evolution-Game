using UnityEngine;
using System.Collections;

public class FriendContext : MonoBehaviour {

	public GameObject ContextMenu;
	public bool isFriends;
	public GameObject FriendsText3D;

	public void OnMouseDown() {
		isFriends = true;
		FriendsText3D.SetActive (true);
		ContextMenu.SetActive (false);
		Time.timeScale = 1f;
	}

}
