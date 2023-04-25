using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour
{
    public Text ScoreText; //得点の文字の変数
	public HandingField field;
	[SerializeField]TextMeshProUGUI label;
	private string keystr;
    // Update is called once per frame
	private void Awake(){
	}
    void Update()
    {
		label.playerScores.TryGetValue(keystr, out int score);
        label.SetText("<size=50%>{0}}</size>\n{1}",keystr",score); //ScoreTextの文字をScore:Scoreの値にする
    }
}