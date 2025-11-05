using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
	private int timesHit;
	private bool isBreakable;
	private LevelManager levelManager;
	public Sprite[] HitSprites;
	public static int breakableNumber=0;
	public GameObject smoke;


	public int maxHit;
	// Use this for initialization
	void Start () {
		isBreakable= (this.tag=="Breakable");
		if(isBreakable){
			breakableNumber++;

		}
		levelManager= GameObject.FindObjectOfType<LevelManager>();
		print(breakableNumber);
		
		
	
		timesHit=0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D collision){
		if(isBreakable){
		HitTheBrick();
		}
			
		
		
	}
	public void HitTheBrick(){
		timesHit++;
		int maxHit = HitSprites.Length+1;

		if (timesHit>=maxHit){
			breakableNumber--;
			Destroy(gameObject);
			levelManager.BrickDestroyed();
			PuffSmoke();

		}else
		LoadSprite();
	}

	public void PuffSmoke(){
		GameObject smokepuff=Instantiate(smoke,transform.position,Quaternion.identity) as GameObject;
		smokepuff.GetComponent<ParticleSystem>().startColor=gameObject.GetComponent<SpriteRenderer>().color;
			


	}
	void LoadSprite(){
		int spriteInfo =timesHit-1;
		 	if(HitSprites[spriteInfo]!=null){
				 this.gameObject.GetComponent<SpriteRenderer>().sprite=HitSprites[spriteInfo];


			 }else{
				 Debug.LogError("Brick sprite eksik");

			 }


	}
}
