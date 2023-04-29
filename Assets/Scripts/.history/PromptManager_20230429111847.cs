using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class PromptManager : MonoBehaviour{
	[SerializeField] Sprite win_image;
	[SerializeField] Sprite lose_image;
	[SerializeField] SpriteRenderer spriteRenderer;
	private void Update(){

	}
	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		model = GetComponent<GenericCard>();
	}
	private void Start(){
		spriteRenderer.sprite

	}
}