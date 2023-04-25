using System.Collections.Generic;
using UnityEngine;

public class HandingField : MonoBehaviour
{
    // 各プレイヤーのスコアを保持するDictionary
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();

    // ターンごとに通知する番号を保持するList
    private List<int> turnNumbers = new List<int>();

    // カードを提出するフィールドのRigidbody 2DとBox Collider 2Dコンポーネント
    private Rigidbody2D fieldRigidbody;
    private BoxCollider2D fieldCollider;
    private int turnlimit;

    private void Start()
    {
        // フィールドのRigidbody 2DとBox Collider 2Dコンポーネントを取得
        fieldRigidbody = GetComponent<Rigidbody2D>();
        fieldCollider = GetComponent<BoxCollider2D>();
        turnlimit = 0;
    }

    // プレイヤーを登録する関数
    public void RegisterPlayer(string uid)
    {
        // プレイヤーのスコアを0で初期化して，Dictionaryに追加
        playerScores.Add(uid, 0);
    }

    // 各ターンの番号をランダムに決定する関数
    private void GenerateTurnNumbers()
    {
        // 1~13の番号を重複なくランダムに生成
        for (int i = 1; i <= 13; i++)
        {
            int index = Random.Range(0, turnNumbers.Count + 1);
            turnNumbers.Insert(index, i);
        }
    }

    // ターンを開始する関数
    public void StartTurn()
    {
        // ターンごとの番号をまだ生成していない場合は生成する
        if (turnNumbers.Count == 0)
        {
            GenerateTurnNumbers();
        }

        // ターン番号を取得して，FieldCardオブジェクトのGenericCardコンポーネントに通知
        int turnNumber = turnNumbers[0];
        turnNumbers.RemoveAt(0);
        GenericCard fieldCard = GameObject.Find("FieldCard").GetComponent<GenericCard>();
        fieldCard.cardIndex = turnNumber;
        fieldCard.isFace = false;
        fieldCard.StartTurn();
    }

    // カードを提出する関数
    public void SubmitCard(GameObject cardObject, string uid)
    {
        // フィールドにカードを置く
        cardObject.transform.position = transform.position;

        // プレイヤーのスコアを更新する
        int currentScore = playerScores[uid];
        int cardValue = cardObject.GetComponent<GenericCard>().cardValue;
        playerScores[uid] = currentScore + cardValue;
    }
}