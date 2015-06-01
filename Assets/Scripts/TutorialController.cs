using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	public string[] messages;
	public GameObject player;
	public GameObject instructions;
	public Text instructionText;
	private int step;
	private bool wentLeft;
	private bool wentRight;
	private bool shoot;
	private bool spin;

	// Use this for initialization
	void Start () {

		messages = new string[]{"Use the arrow keys to move left and right", 
								"Press and hold the \"F\" key to shoot", 
								"Do a barrel roll!!! \n\nPress the space bar",
								"Congradulations! \n\nYour tutorial is complete."};
		step = 0;
		wentLeft = false;
		wentRight = false;
		shoot = false;
		spin = false;
		instructionText = instructions.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		NextStepCheck ();
		if (player.transform.position.x > 5.0f) {
			wentRight = true;
		}
		if (player.transform.position.x < -5.0f) {
			wentLeft = true;
		}
		if (step == 1 && Input.GetKey(KeyCode.F)) {
			shoot = true;
		}
		if (step == 2 && Input.GetKey(KeyCode.Space)) {
			spin = true;
		}
	}

	void NextStepCheck(){
		switch (step) {
			case 0:
				instructionText.text = messages[step];
				if( wentLeft && wentRight){
					instructionText.text = messages[1];
					step++;
					
				}
				break;
			case 1:
				if(shoot){
					instructionText.text = messages[2];
					step++;
				}	
				break;
			case 2:
				if(spin){
					instructionText.text = messages[3];
					StartCoroutine(Congradulations());
				}	
				break;
		}
	}

	IEnumerator Congradulations(){
		yield return new WaitForSeconds (5);
		Application.LoadLevel ("Main");
	}
	
}
