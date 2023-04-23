using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericCard : MonoBehaviour {
	public Sprite[] face_images;
	public Sprite[] tail_images;
	public int cardIndex;
	public int tailIndex;

	RectTransform rectTransform;
	static bool isFace=true;
	GenericCard model;
	SpriteRenderer spriteRenderer;

	public AnimationCurve scaleCurve;
	public float duration = 0.5f;

	private IEnumerator ToggleImage(){

		spriteRenderer.sprite = !isFace ? face_images[cardIndex] : tail_images[tailIndex];
		
		float tick = 0f;
		float scale;	
		float default_scale = transform.localScale.x;
		Vector3 local_scale;
		while (tick < 1.0f){
			scale = scaleCurve.Evaluate(tick);
			tick += Time.deltaTime / duration;
				
			local_scale = transform.localScale;
			local_scale.x = scale * default_scale;
			transform.localScale = local_scale;

			if(tick >= 0.5f){
				spriteRenderer.sprite = isFace ? face_images[cardIndex] : tail_images[tailIndex];
			}

			yield return new WaitForFixedUpdate(); 
		}
	
		if(isFace){
			spriteRenderer.sprite = face_images[cardIndex];
		}
		else{
			spriteRenderer.sprite = tail_images[tailIndex];
			
		}
		isFace = !isFace;
	}
	
	private void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer>();
		model = GetComponent<GenericCard>();
	}
	private void Start(){
		if (isFace){
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
	
}
