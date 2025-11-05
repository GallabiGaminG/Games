using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public void LoadLevel(string name){
		Debug.Log("New Level Loaded"+ name);
		Application.LoadLevel(name);
	}	
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel(){
		Brick.breakableNumber=0;
		Application.LoadLevel(Application.loadedLevel+1);

	}
	public void BrickDestroyed(){
		if(Brick.breakableNumber<=0){
			LoadNextLevel();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
	