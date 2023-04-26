using UnityEngine;

public class GenericCardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public float fadeDuration = 1f;

    private RectTransform canvasRect;

    private void Start()
    {
        canvasRect = GetComponent<RectTransform>();
        StartCoroutine(SpawnCard());
    }

    private IEnumerator SpawnCard()
    {
        yield return new WaitForSeconds(1f); // 初めの1秒間は何もしない

        GameObject cardObj = Instantiate(cardPrefab, transform);

        // 画面外のランダムな位置から生成する
        Vector2 spawnPos = new Vector2(canvasRect.rect.width / 2f + cardObj.GetComponent<RectTransform>().rect.width / 2f, Random.Range(0f, canvasRect.rect.height));
        cardObj.GetComponent<RectTransform>().anchoredPosition = spawnPos;

        // フェードインのアニメーション
        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            cardObj.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0f, 1f, t);
            cardObj.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(spawnPos, Vector2.zero, t);
            yield return null;
        }
    }
}
