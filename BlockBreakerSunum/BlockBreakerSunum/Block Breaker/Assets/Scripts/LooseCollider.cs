using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseCollider : MonoBehaviour {
	private LevelManager levelManager;
	// Use this for initialization
 void OnTriggerEnter2D(Collider2D trigger){
	 levelManager = GameObject.FindObjectOfType<LevelManager>();
	 levelManager.LoadLevel("Loose Screen");
 }



}
