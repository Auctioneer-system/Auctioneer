using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class HandingField : MonoBehaviour
{
    public GameObject deck; // Inspector上でDeckオブジェクトを割り当てる
    public GameObject cardPrefab; // Inspector上でGenericCardのPrefabを割り当てる

    private bool isCardAccepted = false;
    private int acceptedCardIndex;
    private List<int> receivedCardIndices = new List<int>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GenericCard card = collision.gameObject.GetComponent<GenericCard>();
        if (card != null && card.cardIndex != 53 && !isCardAccepted)
        {
            isCardAccepted = true;
            acceptedCardIndex = card.cardIndex;
            DisableDraggableCards();
        }
    }

    private void DisableDraggableCards()
    {
        foreach (Transform child in deck.transform)
        {
            GenericCard card = child.GetComponent<GenericCard>();
            if (card != null)
            {
                card.isDraggable = false;
            }
        }
    }

    public void ResetAcceptance()
    {
        isCardAccepted = false;
        acceptedCardIndex = -1;
    }

    public void ReceiveOthersHanded(int playerID, int cardIndex)
    {
        receivedCardIndices.Add(cardIndex);
        GameObject cardObject = Instantiate(cardPrefab, transform.position, Quaternion.identity);
        GenericCard card = cardObject.GetComponent<GenericCard>();
        card.isFace = false;
        card.cardIndex = cardIndex;
        card.isDraggable = false;

        // プレイヤーの数が2名の場合、左側に1人目の提出されたカード、右側に2人目の提出されたカードを並べる
        float xOffset = (playerID == 1) ? -1.5f : 1.5f;
        cardObject.transform.position = new Vector3(xOffset, 4f, 0f) + transform.position + Vector3.up * receivedCardIndices.Count;
    }
}
