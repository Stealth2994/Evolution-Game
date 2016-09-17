using UnityEngine;
using System.Collections;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {

	public GameObject PlayerSprite;
	int movementSpeed = 5;
	Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		transform.Translate (CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime, CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime, 0);

		float headingDir = Mathf.Atan2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
		PlayerSprite.transform.rotation = Quaternion.Inverse (Quaternion.Euler (0f, 0f, headingDir * Mathf.Rad2Deg));
	}
}
