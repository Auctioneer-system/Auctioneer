using System.Collections.Generic;
using UnityEngine;

public class HandingField : MonoBehaviour
{
	// 各プレイヤーのスコアを保持するDictionary
	private Dictionary<string, int> playerScores = new Dictionary<string, int>();
	private List<GameObject> HandedCards;

	// ターンごとの場の番号を保持するList
	private List<int> Yamahuda;

	// ターンごとの提出値を保持するList
	// 注意：cardIndexではなく，実数値としてのカードの数値
	private Dictionary<string, int> HandedScores;

	// local専用：敵の提出札リスト
	private List<int> Tekihuda = new List<int>();

	// カードを提出するフィールドのRigidbody 2DとBox Collider 2Dコンポーネント
	private Rigidbody2D rigidBody;
	private BoxCollider2D collidor;
	private int turnlimit;
	private int countplayer = 2;

	private GenericCard fieldCard;
	private void Awake()
	{
		GenerateYamahuda();
		RegisterPlayer("local");
		RegisterPlayer("enemy");
		fieldCard = fieldObject.GetComponent<GenericCard>();
	}
	public int getScore(string key)
	{
		playerScores.TryGetValue(key, out int score);
		return score;
	}
	private void Start()
	{
		// フィールドのRigidbody 2DとBox Collider 2Dコンポーネントを取得
		rigidBody = GetComponent<Rigidbody2D>();
		collidor = GetComponent<BoxCollider2D>();
		turnlimit = 0;

		// 敵札の生成処理
		for (int i = 1; i <= 13; i++)
			Tekihuda.Add(i);
		for (int i = Tekihuda.Count - 1; i > 0; i--)
		{
			int j = UnityEngine.Random.Range(0, i + 1);
			int temp = Tekihuda[i];
			Tekihuda[i] = Tekihuda[j];
			Tekihuda[j] = temp;
		}
		StartTurn();
	}
	private void Update()
	{
		if (turnlimit-- == 0)
		{
			// SubmitLocalScore("local");
			try
			{
				EndTurn();
			}
			catch (KeyNotFoundException e)
			{
				Debug.Log("not handed!!");
				turnlimit = 1;
			}
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
		Yamahuda = new List<int>();
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
		if (Yamahuda == null)
		{
			GenerateYamahuda();
		}
		HandedScores = new Dictionary<string, int>();
		submitCount = 0;

		// ターン番号を取得して，FieldCardオブジェクトのGenericCardコンポーネントに通知
		int turnNumber = Yamahuda[0];
		Yamahuda.RemoveAt(0);
		fieldCard.cardIndex = (turnNumber * 2) - 1;
		fieldCard.isFace = false;
		isReceiving = true;
		fieldCard.StartTurn();
		turnlimit = 6000;

		// local専用：敵のカードを提出する
		SubmitOthersScore(Tekihuda[0], "enemy");
		Tekihuda.RemoveAt(0);
	}
	public void EndTurn()
	{
		// ターン終了時に勝者を判定する
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
		// 勝者にScoreを加算する
		playerScores[winner] += fieldObject.GetComponent<GenericCard>().cardIndex % 13 + 1;

		// 提出済みカードの破棄
		for (int i = 0; i < HandedCards.Count; i++)
		{

			GenericCardManager.instance.SlideOutToLeft(HandedCards, 0.5f);
			StartCoroutine(DelayCoroutine(3, () =>
			{
				Destroy(HandedCards[0]);
				HandedCards.RemoveAt(0);
			}));

		}
		if (Yamahuda.Count == 0)
		{
			// StartTurnを呼び出して新しいターンを開始する
			Invoke(nameof(StartTurn), 5f);

		}
		else
		{
			// ゲーム全体の終了処理
		}
	}
	private IEnumerator DelayCoroutine(float seconds, Action action)
	{
		yield return new WaitForSeconds(seconds);
		action?.Invoke();
	}

	// カードを提出する関数
	public void SubmitLocalScore(GameObject cardObject, string uid)
	{
		// フィールドにカードを置く
		cardObject.transform.position = transform.position;
		GenericCard submittedCard = cardObject.GetComponent<GenericCard>();
		submittedCard.isHanded = true;
		submittedCard.isDraggable = false;
		// 注意：cardIndexではなく，実数値としてのカードの数値
		HandedScores.Add(uid, cardObject.GetComponent<GenericCard>().cardIndex % 13 + 1);
		Debug.Log("Handed XD");
		isReceiving = false;
	}

	private int submitCount;
	public void SubmitOthersScore(int cardnumber, string uid)
	{
		GameObject cardObject = GenericCardManager.instance.SpawnCardFromLeft(0.4f, 0.1f, cardnumber, 1);
		GenericCard submittedCard = cardObject.GetComponent<GenericCard>();
		submittedCard.isHanded = true;
		submittedCard.isDraggable = false;
		// 注意：cardIndexではなく，実数値としてのカードの数値を追加する．
		HandedScores.Add(uid, cardnumber);
		Debug.Log("Handed XD");
		HandedCards.Add(cardObject);
	}


	private Collider2D CollidingField;
	private bool isReceiving;
	void OnTriggerStay2D(Collider2D coll)
	{
		if (!isReceiving) return;
		// 衝突オブジェクトがカードなら，そのカードを提出処理
		if (coll.gameObject.tag == "Card")
		{
			SubmitLocalScore(coll.gameObject, "local");
			Debug.Log("collide");
		}
	}
}