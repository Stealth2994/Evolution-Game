using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public int movementSpeed = 5;
	Rigidbody2D rb;

	void Start () {
		rb = this.GetComponent<Rigidbody2D> ();
	}

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
	}
}
