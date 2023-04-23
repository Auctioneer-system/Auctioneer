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
}
