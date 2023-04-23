using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Deck : MonoBehaviour
{
    public GameObject cardPrefab;
    public float spacing = 1f;

    void Start()
    {
        for (int i = 0; i < 13; i++)
        {
            GameObject card = Instantiate(cardPrefab, transform);
            card.GetComponent<GenericCard>().cardIndex = i;
            card.transform.localPosition = new Vector3(spacing * i, 0, 0);
        }
    }
}
