using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCardManager : MonoBehaviour
{
    public GameObject genericCardPrefab;
    public float slideInDuration = 0.5f;

    private Sprite[] cardFaces;
    private Sprite[] cardTails;
    private float screenWidth;
    private float screenHeight;

    private void Start()
    {
        cardFaces = Resources.LoadAll<Sprite>("CardFaces");
        cardTails = Resources.LoadAll<Sprite>("CardTails");

        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    public void SpawnCardFromLeft(float xratio, float yratio ,int face, int tail)
    {
        GameObject cardObject = Instantiate(genericCardPrefab);
        GenericCard card = cardObject.GetComponent<GenericCard>();

        int randomIndex = Random.Range(0, cardFaces.Length);
        card.cardIndex=face;
		card.tailIndex=tail;

        float cardWidth = cardObject.GetComponent<SpriteRenderer>().bounds.size.x;
        float cardHeight = cardObject.GetComponent<SpriteRenderer>().bounds.size.y;

		float tarxPosition = -Screen.width / 2f + cardWidth / 2f + (xRatio * Screen.width);
        float taryPosition = -Screen.height / 2f + cardHeight / 2f + (yRatio * Screen.height);
		Vector3 slideInPosition = new Vector2(tarxPosition,taryPosition);

        float xPosition = -(screenWidth / 2) - (cardWidth / 2);
        float yPosition = slideInPosition.y;

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);
        cardObject.transform.position = spawnPosition;


        StartCoroutine(SlideIn(cardObject.transform, slideInPosition, slideInDuration));
    }
	    public void SpawnCardFromRight(float xratio, float yratio ,int face, int tail)
    {
        GameObject cardObject = Instantiate(genericCardPrefab);
        GenericCard card = cardObject.GetComponent<GenericCard>();

        int randomIndex = Random.Range(0, cardFaces.Length);
        card.cardIndex=face;
		card.tailIndex=tail;

        float cardWidth = cardObject.GetComponent<SpriteRenderer>().bounds.size.x;
        float cardHeight = cardObject.GetComponent<SpriteRenderer>().bounds.size.y;

		float tarxPosition = -Screen.width / 2f + cardWidth / 2f + (xRatio * Screen.width);
        float taryPosition = -Screen.height / 2f + cardHeight / 2f + (yRatio * Screen.height);
		Vector3 slideInPosition = new Vector2(tarxPosition,taryPosition);

        float xPosition = +(screenWidth / 2) + (cardWidth / 2);
        float yPosition = slideInPosition.y;

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);
        cardObject.transform.position = spawnPosition;


        StartCoroutine(SlideIn(cardObject.transform, slideInPosition, slideInDuration));
    }
	    public void SpawnCardFromTop(float xratio, float yratio ,int face, int tail)
    {
        GameObject cardObject = Instantiate(genericCardPrefab);
        GenericCard card = cardObject.GetComponent<GenericCard>();

        int randomIndex = Random.Range(0, cardFaces.Length);
        card.cardIndex=face;
		card.tailIndex=tail;

        float cardWidth = cardObject.GetComponent<SpriteRenderer>().bounds.size.x;
        float cardHeight = cardObject.GetComponent<SpriteRenderer>().bounds.size.y;

		float tarxPosition = -Screen.width / 2f + cardWidth / 2f + (xRatio * Screen.width);
        float taryPosition = -Screen.height / 2f + cardHeight / 2f + (yRatio * Screen.height);
		Vector3 slideInPosition = new Vector2(tarxPosition,taryPosition);

        float xPosition = slideInPosition.x;
        float yPosition = +(screenHeight / 2) + (cardHeight / 2);

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);
        cardObject.transform.position = spawnPosition;

        StartCoroutine(SlideIn(cardObject.transform, slideInPosition, slideInDuration));
    }

	    public void SpawnCardFromTop(float xratio, float yratio ,int face, int tail)
    {
        GameObject cardObject = Instantiate(genericCardPrefab);
        GenericCard card = cardObject.GetComponent<GenericCard>();

        int randomIndex = Random.Range(0, cardFaces.Length);
        card.cardIndex=face;
		card.tailIndex=tail;

        float cardWidth = cardObject.GetComponent<SpriteRenderer>().bounds.size.x;
        float cardHeight = cardObject.GetComponent<SpriteRenderer>().bounds.size.y;

		float tarxPosition = -Screen.width / 2f + cardWidth / 2f + (xRatio * Screen.width);
        float taryPosition = -Screen.height / 2f + cardHeight / 2f + (yRatio * Screen.height);
		Vector3 slideInPosition = new Vector2(tarxPosition,taryPosition);

        float xPosition = slideInPosition.x;
        float yPosition = -(screenHeight / 2) - (cardHeight / 2);

        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);
        cardObject.transform.position = spawnPosition;

        StartCoroutine(SlideIn(cardObject.transform, slideInPosition, slideInDuration));
    }

    private IEnumerator SlideIn(Transform transform, Vector2 slideInPosition, float duration)
    {
        float elapsedTime = 0f;
        float startX = transform.position.x;
        float endX = slideInPosition.x;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float currentX = Mathf.Lerp(startX, endX, t);
            transform.position = new Vector3(currentX, slideInPosition.y, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(slideInPosition.x, slideInPosition.y, 0f);
    }
}