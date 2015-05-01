using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Button startButton;
	public Button tutorialButton;

	// Use this for initialization
	void Start () {
		startButton.onClick.AddListener (StartGame);
		tutorialButton.onClick.AddListener (StartTutorial);
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
}
