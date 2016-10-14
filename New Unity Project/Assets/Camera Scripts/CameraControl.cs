using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {
    public GameObject character;
    public float lagSpeed = 3;
    GenerateGrid g;
	// Use this for initialization
	void Start () {
        g = GameObject.Find("Grid").GetComponent<GenerateGrid>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(character.transform.position.x, character.transform.position.y - 3.5f, character.transform.position.z), lagSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -15);
        
	}
    void LateUpdate()
    {
        if(transform.position.x > g.length - 10)
        {
            transform.position = new Vector3(g.length - 10, transform.position.y, transform.position.z);
        }
        if (transform.position.y > g.width - 6)
        {
            transform.position = new Vector3(transform.position.x, g.width -6, transform.position.z);
        }
        if (transform.position.x < 9)
        {
            transform.position = new Vector3(9, transform.position.y, transform.position.z);
        }
        if (transform.position.y < 5)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }
    }
}
