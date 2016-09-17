using UnityEngine;
using System.Collections;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {

	int movementSpeed = 5;
	Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		//Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));

		transform.Translate (CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime, CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime, 0);
	}
}
