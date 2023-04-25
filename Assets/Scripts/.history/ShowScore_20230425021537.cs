using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour
{
	public HandingField field;
	[SerializeField]TextMeshProUGUI label;
	[SerializeField] string keystr;
    // Update is called once per frame
	private void Awake(){
	}
    void Update()
    {
        label.SetText("<size=50%>"+keystr+"</size>\n{1}",keystr, field.getScore(keystr) ); //ScoreTextの文字をScore:Scoreの値にする
    }
}