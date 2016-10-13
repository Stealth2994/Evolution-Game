using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.localRotation = new Quaternion (-180f, Random.Range(0f, 360f), 0f, this.transform.rotation.w);
	}
}
