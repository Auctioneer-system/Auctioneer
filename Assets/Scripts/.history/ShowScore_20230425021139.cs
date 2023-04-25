using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour
{

	public HandingField field;
	[SerializeField] TextMeshProUGUI label;
	[SerializeField] public string keystr;
	// Update is called once per frame
	private void Awake()
	{
	}
	void Update()
	{
		label.playerScores.TryGetValue(keystr, out int score);
		label.SetText("<size=50%>{0}}</size>\n{1}", keystr, score); //ScoreTextの文字をScore:Scoreの値にする
	}
}