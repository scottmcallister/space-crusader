using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public Button startButton;

	// Use this for initialization
	void Start () {
		startButton.onClick.AddListener (StartGame);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void StartGame(){
		Application.LoadLevel("Main");
	}
}
