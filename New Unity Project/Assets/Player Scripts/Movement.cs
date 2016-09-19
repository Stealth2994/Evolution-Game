using UnityEngine;
using System.Collections;
using UnityStandardAssets;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {
    public float nutrition;
    float minColour = 0f;
	float maxColour = 1f;
	float posY;
	float posX;
    GameObject Player;
    bool isWalking;
	public SpriteRenderer PlayerSpriteRenderer;
	Vector3 curpos;
	Vector3 lastpos;
	public Animator PlayerAnimator;
	public GameObject PlayerSprite;
	public float movementSpeed = 5;
    public GenerateGrid grid;
	Rigidbody2D rb;
    PlayerHunger playerHunger;
    
    void Start () {
        Player = GameObject.FindWithTag("Player");
        playerHunger = Player.GetComponent<PlayerHunger>();
        grid = GameObject.Find("Grid").GetComponent<GenerateGrid>();
		PlayerSpriteRenderer.color = new Color (Random.Range(minColour,maxColour), Random.Range(minColour,maxColour), Random.Range(minColour,maxColour));
		isWalking = false;
		PlayerAnimator.Play ("PlayerAnimation");
		rb = this.GetComponent<Rigidbody2D> ();
	}
    public bool doit = false;
    public GameObject target;
	void FixedUpdate () {
        TerrainTileValues t;
        grid.grid.TryGetValue(new GenerateGrid.coords((int)transform.position.x, (int)transform.position.y), out t);

        float tempSpeed = movementSpeed * t.speed;
        if (doit)
        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x + 3, target.transform.position.y - 1), (tempSpeed * Time.deltaTime));
            if (Vector3.Distance(transform.position, new Vector3(target.transform.position.x + 3, target.transform.position.y - 1)) < 0.5f)
            {
                if (!GenerateGrid.removeFoodList.ContainsKey(new GenerateGrid.coords((int)target.transform.position.x + 3, (int)target.transform.position.y - 1)))
                {
                    playerHunger.currentHunger = playerHunger.currentHunger + nutrition;
                    GenerateGrid.removeFoodList.Add(new GenerateGrid.coords((int)target.transform.position.x + 3, (int)target.transform.position.y - 1), grid.gridObjects[0].GetComponent<TerrainTileValues>());
                    doit = false;
                }


            }


        }
      
		posY = Mathf.Abs (CrossPlatformInputManager.GetAxis("Vertical"));
		posX = Mathf.Abs (CrossPlatformInputManager.GetAxis("Horizontal"));
		transform.Translate (CrossPlatformInputManager.GetAxis("Horizontal") * Time.deltaTime * tempSpeed, CrossPlatformInputManager.GetAxis("Vertical") * Time.deltaTime * tempSpeed, 0);

		if (posY > 0f || posX > 0f) {
			PlayerAnimator.Play ("PlayerAnimation");
		} else {
			PlayerAnimator.Play ("IdleAnimation");
		}
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
		float headingDir = Mathf.Atan2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis ("Vertical"));
		PlayerSprite.transform.rotation = Quaternion.Inverse (Quaternion.Euler (0f, 0f, headingDir * Mathf.Rad2Deg));
	}
}
