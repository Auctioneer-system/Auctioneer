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

        local_scale = default_local_scale;
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
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag() {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }
	private Collider2D CollidingField;
    
	void OnTriggerStay2D(Collider2D coll){
        CollidingField = (coll.gameObject.tag == "field" ) : coll ? null;
　　}
	void OnMouseUp(){
		if(CollidingField != null){
			//処理
		}
	}
}
