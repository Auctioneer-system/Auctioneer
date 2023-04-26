public class GenericCardManager : MonoBehaviour
{
    public GameObject genericCardPrefab;
    public float slideInDuration = 1.0f;
    public float slideInDelay = 0.5f;
    public Vector2 slideInPosition;

    private Vector2 screenSize;
    private Vector2 cardSize;
    private float cardWidth;
    private float screenWidthRatio;
    private float slideInStartPosition;

    void Start()
    {
        // 画面の解像度を取得
        screenSize = new Vector2(Screen.width, Screen.height);

        // GenericCardのサイズを取得
        cardSize = genericCardPrefab.GetComponent<SpriteRenderer>().bounds.size;
        cardWidth = cardSize.x;

        // 解像度に応じてスケールを調整
        screenWidthRatio = screenSize.x / 1920.0f;
        genericCardPrefab.transform.localScale *= screenWidthRatio;

        // スライドインする前の水平位置を計算
        slideInStartPosition = -(screenSize.x / 2.0f + cardWidth / 2.0f);
    }

    public void SpawnCardAtSlideInPosition()
    {
        // GenericCardを生成
        GameObject card = Instantiate(genericCardPrefab);

        // スライドインする前の位置を設定
        card.transform.position = new Vector2(slideInStartPosition, slideInPosition.y);

        // スライドインのアニメーションを実行
        StartCoroutine(SlideInCard(card));
    }

    private IEnumerator SlideInCard(GameObject card)
    {
        yield return new WaitForSeconds(slideInDelay);

        float t = 0.0f;
        Vector2 startPos = card.transform.position;
        Vector2 endPos = slideInPosition;

        while (t < 1.0f)
        {
            t += Time.deltaTime / slideInDuration;
            card.transform.position = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }
    }
}