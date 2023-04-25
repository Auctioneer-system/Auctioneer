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
	}
    private void Update(){
        if(turnlimit-- <= 0){
            // SubmitCard("local");
            // EndTurn();
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
		fieldCard.StartTurn();
        turnlimit=600;
	}
    public void EndTurn(){

    }

	// カードを提出する関数
	public void SubmitCard(GameObject cardObject, string uid)
	{
		// フィールドにカードを置く
		cardObject.transform.position = transform.position;
        cardObject.GetComponent<GenericCard>().isHanded=true;
        HandedScores.Add(uid, cardObject.GetComponent<GenericCard>().cardIndex%13+1);
	}

    private Collider2D CollidingField;
	void OnTriggerStay2D(Collider2D coll)
	{
        // 衝突オブジェクトがカードなら，そのカードを提出処理
		if(coll.gameObject.tag == "card"){
            SubmitCard(coll.gameObject, "local");
        }
	}
}