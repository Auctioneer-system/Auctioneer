using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    private int Score; //得点の変数
    public Text ScoreText; //得点の文字の変数

    // Start is called before the first frame update
    void Start()
    {
        Score = 0; //得点を0にする
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score:" + Score.ToString(); //ScoreTextの文字をScore:Scoreの値にする

        if (Input.GetKey(KeyCode.Space)) //もし、スペースキーがおされたら、
        {
            Score += 1000; //Scoreを1000ずつ変える
        }
    }
}