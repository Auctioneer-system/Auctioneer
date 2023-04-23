using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject cardPrefab;
    public int numCards = 13;
    public float radius = 2.0f;

    void Start()
    {
        for (int i = 0; i < numCards; i++)
        {
            GameObject cardObject = Instantiate(cardPrefab, transform.position, Quaternion.identity, transform);
            GenericCard card = cardObject.GetComponent<GenericCard>();
            card.cardIndex = i;
            float angle = (float)i / numCards * Mathf.PI * 2.0f;
            Vector3 position = new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
            card.transform.SetParent(transform.Find("CardPosition"));
            card.transform.localPosition = position;
        }
    }
}
