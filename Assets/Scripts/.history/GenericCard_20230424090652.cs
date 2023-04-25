using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GenericCard : MonoBehaviour {
	public Sprite[] face_images;
	public Sprite[] tail_images;
	public int cardIndex;
	public int tailIndex;

	RectTransform rectTransform;
	bool isFace=true;
	GenericCard model;
	SpriteRenderer spriteRenderer;

	public AnimationCurve scaleCurve;
	public float duration = 0.5f;
	
private IEnumerator ToggleImage()
{
    float tick = 0f;
    float scale;
    float default_scale = transform.localScale.x;
    Vector3 local_scale;
    Vector3 default_local_scale = transform.localScale;
    while (tick < 1.0f)
    {
        scale = scaleCurve.Evaluate(tick);
        tick += Time.deltaTime / duration;

        local_scale = transform.localScale;
        local_scale.x = scale * default_scale;
        transform.localScale = local_scale;

        if (tick >= 0.5f)
        {
            spriteRenderer.sprite = isFace ? face_images[cardIndex] : tail_images[tailIndex];
        }

        yield return null;
    }

    // スプライトの切り替えは最後に1回だけ行う
    spriteRenderer.sprite = isFace ? face_images[cardIndex] : tail_images[tailIndex];

    isFace = !isFace;
}
	
	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		model = GetComponent<GenericCard>();
	}
	private void Start(){
		if (this.isFace){
			spriteRenderer.sprite = face_images[cardIndex < face_images.Length-1 ? cardIndex : 0];

		}else{
			spriteRenderer.sprite = tail_images[cardIndex < tail_images.Length-1 ? cardIndex : 0];
		}
	}
	public void StartTurn(){
		StopCoroutine(ToggleImage());
		Debug.Log("Start Turn");
		StartCoroutine(ToggleImage());
	}
    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown() {
        transform.SetAsLastSibling(); // 最前面に移動する
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }
    public bool isDraggable;
    void OnMouseDrag() {
        if(!isDraggable) return ;
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }
	private Collider2D CollidingField;
	void OnTriggerStay2D(Collider2D coll){
        CollidingField = coll.gameObject.tag == "field"  ? coll : null;
    }

	void OnMouseUp(){
		if(CollidingField != null){
			// 場にカードを提出した時の処理
            isHanded = true;
            isDraggable = false;
            // 親オブジェクトを指定する
            // 子オブジェクトのコンポーネントを取得する
            Component[] components = gameObject.GetComponentsInChildren<GenericCard>();

            foreach (GenericCard component in components)
            {
                // 取得した子オブジェクトのコンポーネントに対して処理を行う
                component.isDraggable=false;
            }
		}
	}
    private bool isHanded;
    void Update(){
        if(isHanded){
            // カードが提出済みの時の処理
        }
    }
    public AnimationCurve moveCurve;
    public float moveDuration = 1.0f;

    public void DiscardToSide(float direction)
    {
        StartCoroutine(MoveAndHide(direction));
    }

    private IEnumerator MoveAndHide(float direction)
    {
        float tick = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + Quaternion.Euler(0f, 0f, direction) * Vector3.right * Screen.width;

        while (tick < 1.0f)
        {
            float t = moveCurve.Evaluate(tick);
            tick += Time.deltaTime / moveDuration;

            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        // this.gameObject.SetActive(false);
        Debug.Log("Discarded");
    }
}
