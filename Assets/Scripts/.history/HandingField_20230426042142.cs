using System.Collections.Generic;
using UnityEngine;

public class HandingField : MonoBehaviour
{
	// 各プレイヤーのスコアを保持するDictionary
	private Dictionary<string, int> playerScores = new Dictionary<string, int>();
	// ターンごとの場の番号を保持するList
	private List<int> Yamahuda = new List<int>();
    // ターンごとの提出値を保持するList
    private Dictionary<string,int> HandedScores;

	// local専用：敵の提出札リスト
	private List<int> Tekihuda = new List<int>();

	// カードを提出するフィールドのRigidbody 2DとBox Collider 2Dコンポーネント
	private Rigidbody2D rigidBody;
	private BoxCollider2D collidor;
	private int turnlimit;
    private int countplayer = 2;

    private void Awake(){
        GenerateYamahuda();
        RegisterPlayer("local");
        RegisterPlayer("enemy");
    }
    public int getScore(string key){
        playerScores.TryGetValue(key, out int score);
        return score;
    }
	private void Start()
	{
		// フィールドのRigidbody 2DとBox Collider 2Dコンポーネントを取得
		rigidBody = GetComponent<Rigidbody2D>();
		collidor = GetComponent<BoxCollider2D>();
		turnlimit = 0;
        StartTurn();
        // ローカル用で，敵にはターン開始と同時に札を出してもらいます
        for (int i = 1; i <= 13; i++)
		{
			Tekihuda.Add(i);
		}

		// Fisher-Yates shuffle
		for (int i = Tekihuda.Count - 1; i > 0; i--)
		{
			int j = UnityEngine.Random.Range(0, i + 1);
			int temp = Tekihuda[i];
			Tekihuda[i] = Tekihuda[j];
			Tekihuda[j] = temp;
		}
	}
    private void Update(){
        if(turnlimit-- <= 0){
            // SubmitLocalScore("local");
            EndTurn();
        }
    }

	// プレイヤーを登録する関数
	public void RegisterPlayer(string uid)
	{
		// プレイヤーのスコアを0で初期化して，Dictionaryに追加
		playerScores.Add(uid, 0);
	}

	// 各ターンの番号をランダムに決定する関数
	private void GenerateYamahuda()
	{
		for (int i = 1; i <= 13; i++)
		{
			Yamahuda.Add(i);
		}

		// Fisher-Yates shuffle
		for (int i = Yamahuda.Count - 1; i > 0; i--)
		{
			int j = UnityEngine.Random.Range(0, i + 1);
			int temp = Yamahuda[i];
			Yamahuda[i] = Yamahuda[j];
			Yamahuda[j] = temp;
		}

	}

    [SerializeField] GameObject fieldObject;
	// ターンを開始する関数
	public void StartTurn()
	{
		// ターンごとの番号をまだ生成していない場合は生成する
		if (Yamahuda.Count == 0)
		{
			GenerateYamahuda();
		}
        HandedScores = new Dictionary<string, int>();

		// ターン番号を取得して，FieldCardオブジェクトのGenericCardコンポーネントに通知
		int turnNumber = Yamahuda[0];
		Yamahuda.RemoveAt(0);
		GenericCard fieldCard = fieldObject.GetComponent<GenericCard>();
		fieldCard.cardIndex = (turnNumber*2)-1;
        fieldCard.isFace = false;
        isReceiving=true;
		fieldCard.StartTurn();
        turnlimit=6000;
	}
    public void EndTurn(){
    // ターン終了時に勝者を判定する
    int maxScore = -1;
    string winner = "";
    foreach (KeyValuePair<string, int> playerScore in HandedScores)
    {
        if (playerScore.Value > maxScore)
        {
            maxScore = playerScore.Value;
            winner = playerScore.Key;
        }
		Debug.Log(winner);
    }
    // 勝者にScoreを加算する
    playerScores[winner] += fieldObject.GetComponent<GenericCard>().cardIndex % 13 + 1;
    // StartTurnを呼び出して新しいターンを開始する
    StartTurn();
    }

	// カードを提出する関数
	public void SubmitLocalScore(GameObject cardObject, string uid)
	{
		// フィールドにカードを置く
		cardObject.transform.position = transform.position;
        GenericCard submittedCard = cardObject.GetComponent<GenericCard>();
        submittedCard.isHanded=true;
        submittedCard.isDraggable=false;
        HandedScores.Add(uid, cardObject.GetComponent<GenericCard>().cardIndex % 13+1);
        Debug.Log("Handed XD");
        isReceiving=false;
	}
	public void SubmitOthersScore(int cardnumber, string uid)
	{
		// フィールドにカードを置く
		cardObject.transform.position = transform.position;
        GenericCard submittedCard = cardObject.GetComponent<GenericCard>();
        submittedCard.isHanded=true;
        submittedCard.isDraggable=false;
        HandedScores.Add(uid, cardObject.GetComponent<GenericCard>().cardIndex % 13+1);
        Debug.Log("Handed XD");
        isReceiving=false;
	}
    	public void reGisterEnemyCard(int number, string uid)
	{
		// フィールドにカードを置く
        HandedScores.Add(uid, number);
        Debug.Log("Handed XD");
	}

    private Collider2D CollidingField;
    private bool isReceiving;
	void OnTriggerStay2D(Collider2D coll)
	{
        if(!isReceiving) return;
        // 衝突オブジェクトがカードなら，そのカードを提出処理
		if(coll.gameObject.tag == "Card"){
            SubmitLocalScore(coll.gameObject, "local");
            Debug.Log("collide");
        }
	}
}