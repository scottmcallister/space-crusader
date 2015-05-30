using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Button startButton;
	public Button tutorialButton;
	public Text startText;
	public Text tutorialText;

	// Use this for initialization
	void Start () {
		startButton.onClick.AddListener (StartGame);
		tutorialButton.onClick.AddListener (StartTutorial);
		/*startButton.OnPointerEnter (HighlightStart);
		startButton.OnPointerExit (UnHighlightStart);
		tutorialButton.OnPointerEnter (HighlightTutorial);
		tutorialButton.OnPointerEnter (UnHighlightTutorial);*/
	}
	
	// Update is called once per frame
	void Update () {

	}

	void StartGame(){
		Application.LoadLevel("Main");
	}

	void StartTutorial(){
		Application.LoadLevel ("Tutorial");
	}

	void HighlightStart(){
		startText.color = Color.yellow;
	}

	void UnHighlightStart(){
		startText.color = Color.black;
	}

	void HighlightTutorial(){
		tutorialText.color = Color.yellow;
	}
	
	void UnHighlightTutorial(){
		tutorialText.color = Color.black;
	}

}