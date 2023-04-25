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
		keystr="";
	}
    void setScore(int num)
    {
		
        label.SetText("{0}}\n{1}","local",); //ScoreTextの文字をScore:Scoreの値にする
    }
}