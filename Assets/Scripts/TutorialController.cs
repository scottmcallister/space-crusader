using UnityEngine;
using System.Collections;

public class TutorialController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Return))
			Application.LoadLevel("Menu");
	}
}
