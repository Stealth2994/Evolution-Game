using UnityEngine;
using System.Collections;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {

	float minColour = 0f;
	float maxColour = 1f;
	float posY;
	float posX;
	bool isWalking;
	public SpriteRenderer PlayerSpriteRenderer;
	Vector3 curpos;
	Vector3 lastpos;
	public Animator PlayerAnimator;
	public GameObject PlayerSprite;
	int movementSpeed = 5;
	Rigidbody2D rb;

	void Start () {
		PlayerSpriteRenderer.color = new Color (Random.Range(minColour,maxColour), Random.Range(minColour,maxColour), Random.Range(minColour,maxColour));
		isWalking = false;
		PlayerAnimator.Play ("PlayerAnimation");
		rb = this.GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		posY = Mathf.Abs (CrossPlatformInputManager.GetAxis("Vertical"));
		posX = Mathf.Abs (CrossPlatformInputManager.GetAxis("Horizontal"));
		transform.Translate (CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * movementSpeed, 0);

		if (posY > 0f || posX > 0f) {
			PlayerAnimator.Play ("PlayerAnimation");
		} else {
			PlayerAnimator.Play ("IdleAnimation");
		}

		float headingDir = Mathf.Atan2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
		PlayerSprite.transform.rotation = Quaternion.Inverse (Quaternion.Euler (0f, 0f, headingDir * Mathf.Rad2Deg));
	}
}
