using UnityEngine;
using System.Collections;

public class WaveResizer : MonoBehaviour {

	public Sprite[] spriteArr;
	private float yPos;


	// Use this for initialization
	void Start () {
		yPos = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		yPos = transform.position.y;
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D> ();
		if (yPos <= -15) {
			renderer.sprite = spriteArr[0];
			collider.size = new Vector2(1.0f, 1.235725f);
		}
		else if (yPos <= -10){
			renderer.sprite = spriteArr[1];
			collider.size = new Vector2(1.5f, 1.235725f);
		}
		else if (yPos <= -5){
			renderer.sprite = spriteArr[2];
			collider.size = new Vector2(2.0f, 1.235725f);
		}
		else if (yPos <= 0){
			renderer.sprite = spriteArr[3];
			collider.size = new Vector2(2.5f, 1.235725f);
		}
		else if (yPos <= 5){
			renderer.sprite = spriteArr[4];
			collider.size = new Vector2(3.0f, 1.235725f);
		}
		else if (yPos <= 10){
			renderer.sprite = spriteArr[5];
			collider.size = new Vector2(3.5f, 1.235725f);
		}
		else if (yPos <= 15){
			renderer.sprite = spriteArr[6];
			collider.size = new Vector2(4.0f, 1.235725f);
		}
		else if (yPos <= 20){
			renderer.sprite = spriteArr[7];
			collider.size = new Vector2(4.5f, 1.235725f);
		}
		else if (yPos <= 25){
			renderer.sprite = spriteArr[8];
			collider.size = new Vector2(5.0f, 1.235725f);
		}
		else {
			renderer.sprite = spriteArr[9];
			collider.size = new Vector2(5.5f, 1.235725f);
		}
	}
}
