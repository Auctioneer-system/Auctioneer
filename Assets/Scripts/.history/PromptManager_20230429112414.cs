using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class PromptManager : MonoBehaviour{
	[SerializeField] Sprite win_image;
	[SerializeField] Sprite lose_image;
	[SerializeField] SpriteRenderer spriteRenderer;

	private bool isWon;
	private Dictionary<string,int> Scores = new Dictionary<string, int>;
	private void Update(){

	}
	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		model = GetComponent<GenericCard>();
		Scores.Add("local",PlayerPrefs.GetInt("local"));
		Scores.Add("enemy",PlayerPrefs.GetInt("enemy"));
	}
	private void Start(){
		isWon = Scores["local"]
		// spriteRenderer.sprite = 
	}
}