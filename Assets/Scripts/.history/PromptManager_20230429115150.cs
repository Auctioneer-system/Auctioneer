using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
		Scores.Add("local",PlayerPrefs.GetInt("local"));
		Scores.Add("enemy",PlayerPrefs.GetInt("enemy"));
	}
	private void Start(){
		int maxScore = -1;
		string winner = "";
		foreach (KeyValuePair<string, int> playerScore in Scores)
		{
			Debug.Log(playerScore.Key + ":" + playerScore.Value);
			if (playerScore.Value > maxScore)
			{
				maxScore = playerScore.Value;
				winner = playerScore.Key;
			}
		}
		Scores.Add("local",43);
		Scores.Add("enemy",12);
		spriteRenderer.sprite = (isWon = winner == "local" ? ) ? win_image :lose_image;
		RegisterLabel();
	}
	private void RegisterLabel(){
		string text = "";
			text = "<size=50%>local:" + Scores["local"];
		foreach (KeyValuePair<string, int> playerScore in Scores) {
			if(playerScore.Key == "local")continue;
			text = text +"<size=40%>"+playerScore.Key+"</size>" + playerScore.Value;
		}
		label.SetText(text);
	}
}