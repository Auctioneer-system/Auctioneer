using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandingField : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isColliding = false;
    private int cardIndex = -1;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GenericCard card = eventData.pointerDrag.GetComponent<GenericCard>();
        if (card != null && !isColliding)
        {
            isColliding = true;
            cardIndex = card.cardIndex;
            card.enabled = false;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GenericCard card = eventData.pointerDrag.GetComponent<GenericCard>();
        if (card != null)
        {
            isColliding = false;
            card.enabled = true;
        }
    }

    public void NextTurn()
    {
        isColliding = false;
        cardIndex = -1;
    }

    public int GetCardNumber()
    {
        return cardIndex;
    }
}
