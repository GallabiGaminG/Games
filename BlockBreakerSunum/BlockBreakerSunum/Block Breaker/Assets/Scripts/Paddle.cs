using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
	public bool autoplay;
	public float minX=1.04f, maxX=14.95f;
	private Ball ball;
	
	// Use this for initialization
	void Start () {
		ball=GameObject.FindObjectOfType<Ball>();
	}

	void MoveWithMouse(){
		Vector3 PaddlePos= new Vector3 (0.5f,this.transform.position.y,0f);
		float MousePos= Input.mousePosition.x/Screen.width*16;
		PaddlePos.x=Mathf.Clamp(MousePos,minX,maxX);
		this.transform.position=PaddlePos;

	
	}
	void Autoplay(){
		Vector3 PaddlePos= new Vector3 (0.5f,this.transform.position.y,0f);
		Vector3 Ballpos= ball.transform.position;
		PaddlePos.x=Mathf.Clamp(Ballpos.x,minX,maxX);
		this.transform.position=PaddlePos;

	}



	
	// Update is called once per frame
	void Update () {
		if(!autoplay){
		MoveWithMouse();
		}else{
			Autoplay();
		}
	
	}
}
