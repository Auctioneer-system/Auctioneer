using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject genericCardPrefab; // GenericCardプレハブ
    public int numCards = 13; // カードの枚数

    public float radius = 2f; // サイクロイド曲線の半径
    public float speed = 1f; // サイクロイド曲線の速度

    void Start()
    {
        // サイクロイド曲線上にGenericCardを配置する
        for (int i = 0; i < numCards; i++)
        {
            float t = i / (float)numCards;
            float theta = t * 2f * Mathf.PI;

            float x = radius * (theta - Mathf.Sin(theta));
            float y = radius * (1f - Mathf.Cos(theta));

            Vector3 position = new Vector3(x, y, 0f);
            Quaternion rotation = Quaternion.Euler(0f, 0f, -theta * Mathf.Rad2Deg);

            GameObject genericCard = Instantiate(genericCardPrefab, position, rotation);
            genericCard.transform.SetParent(transform.Find("Card"));
            genericCard.GetComponent<GenericCard>().cardIndex = i;
        }
    }
}
