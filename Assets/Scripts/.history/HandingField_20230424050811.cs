using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandingField : MonoBehaviour, IPointerDownHandler
{
    private bool isCollision = false; // 衝突したかどうかのフラグ
    private int cardNumber; // 衝突したカードの番号を保持する変数

    // GenericCardコンポーネントの衝突を待機する関数
    public IEnumerator WaitForCollision()
    {
        while (!isCollision)
        {
            yield return null;
        }

        // 衝突したらGenericCardコンポーネントのcardIndexの値を保管
        GenericCard genericCard = GetComponentInChildren<GenericCard>();
        if (genericCard != null)
        {
            cardNumber = genericCard.cardIndex;
            // ここにFirebaseへの送信処理を置きたい！
        }
    }

    // 衝突を受け付けないようにする関数
    public void DisableCollision()
    {
        isCollision = false;
    }

    // 衝突を受け付けるようにする関数
    public void EnableCollision()
    {
        isCollision = true;
    }

    // GenericCardコンポーネントの衝突待機を開始する関数
    public void NextTurn()
    {
        StartCoroutine(WaitForCollision());
    }

    // 保管してあるcardIndexの値を返却する関数
    public int GetCardNumber()
    {
        return cardNumber;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 何もしない
    }
        // 他プレイヤーのカードを追加する関数
    public void AddOthersCard(int cardIndex)
    {
        // プレイヤーを区別できる形でカードを格納
        GameObject cardObject = Instantiate(Resources.Load("GenericCard")) as GameObject;
        cardObject.transform.SetParent(transform);
        cardObject.transform.localScale = new Vector3(1, 1, 1);
        GenericCard genericCard = cardObject.GetComponent<GenericCard>();
        if (genericCard != null)
        {
            genericCard.cardIndex = cardIndex;
            // ここにプレイヤーを区別する処理を実装
        }
    }

    // ターンの終了時に呼ばれる関数
    public void EndTurn()
    {
        // 最も大きい値のプレイヤーを判定
        int maxCardNumber = int.MinValue;
        GenericCard[] genericCards = GetComponentsInChildren<GenericCard>();
        foreach (GenericCard genericCard in genericCards)
        {
            if (genericCard.cardIndex > maxCardNumber)
            {
                maxCardNumber = genericCard.cardIndex;
            }
        }

        // 他プレイヤーのカードのToggle関数を呼び出し
        foreach (GenericCard genericCard in genericCards)
        {
            if (genericCard.cardIndex != maxCardNumber)
            {
                genericCard.ToggleImage();
            }
        }

        // 8秒待ってNextTurnを呼び出し
        StartCoroutine(WaitAndNextTurn());
    }

    // 8秒待ってNextTurnを呼び出すコルーチン
    private IEnumerator WaitAndNextTurn()
    {
        yield return new WaitForSeconds(8f);
        NextTurn();
    }


}
