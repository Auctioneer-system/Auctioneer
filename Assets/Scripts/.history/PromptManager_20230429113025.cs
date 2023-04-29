using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;

public class PromptManager : MonoBehaviour{
	[SerializeField] Sprite win_image;
	[SerializeField] Sprite lose_image;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField]TextMeshProUGUI label;

	private bool isWon;
	private Dictionary<string,int> Scores = new Dictionary<string, int>();
	private void Update(){

	}
	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		model = GetComponent<GenericCard>();
		Scores.Add("local",PlayerPrefs.GetInt("local"));
		Scores.Add("enemy",PlayerPrefs.GetInt("enemy"));
	}
	private void Start(){
		int maxScore = -1;
		string winner = "";
		foreach (KeyValuePair<string, int> playerScore in HandedScores)
		{
			Debug.Log(playerScore.Key + ":" + playerScore.Value);
			if (playerScore.Value > maxScore)
			{
				maxScore = playerScore.Value;
				winner = playerScore.Key;
			}
		}
		spriteRenderer.sprite = isWon ? win_image :lose_image;
	}
	
}