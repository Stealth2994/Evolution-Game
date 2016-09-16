using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    public GameObject character;
    public float lagSpeed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position, character.transform.position, lagSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
	}
    
}
