using UnityEngine;
using System.Collections;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {

<<<<<<< HEAD
	public int movementSpeed = 5;
=======
	public GameObject PlayerSprite;
	int movementSpeed = 5;
>>>>>>> 02d97dac20b2b68678dd0e0c1ff17cf56b53ac40
	Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

<<<<<<< HEAD
	void Update () {
		if (Input.GetKey ("w")) {
			transform.Translate (Vector2.up * movementSpeed * Time.deltaTime);
		} else if (Input.GetKey ("s")) {
			transform.Translate (Vector2.down * movementSpeed * Time.deltaTime);
		} else if (Input.GetKey ("a")) {
			transform.Translate (Vector2.left * movementSpeed * Time.deltaTime);
		} else if (Input.GetKey ("d")) {
			transform.Translate (Vector2.right * movementSpeed * Time.deltaTime);
		}
=======
	void FixedUpdate () {
		transform.Translate (CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime, CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime, 0);

		float headingDir = Mathf.Atan2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
		PlayerSprite.transform.rotation = Quaternion.Inverse (Quaternion.Euler (0f, 0f, headingDir * Mathf.Rad2Deg));
>>>>>>> 02d97dac20b2b68678dd0e0c1ff17cf56b53ac40
	}
}
