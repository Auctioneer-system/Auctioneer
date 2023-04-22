using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenericCard : MonoBehaviour {
	Image cardImage;
	public Sprite[] face_images;
	public Sprite[] tail_images;

	public int cardIndex;
	public int tailIndex;
	static float time = 8f;

	RectTransform rectTransform;
	public bool isFace;

	public IEnumerator ToggleImage(){
		float tick = 0f;
		Vector3 startScale = new Vector3(1f, 1f, 1f);
		Vector3 endScale = new Vector3(1f, 0f, 1f);
		Vector3 localScale = new Vector3();
		while (tick < time/2){
			tick += Time.deltaTime * time;
			localScale = Vector3.Lerp(startScale, endScale, tick);
			rectTransform.localScale = localScale;
			yield return null;
		}
	
		if(isFace){
			cardImage.sprite = face_images[cardIndex];
		}
		else{
			cardImage.sprite = tail_images[tailIndex];
			
		}
		isFace = !isFace;
		while (tick < time){
			tick += Time.deltaTime * time;
			localScale = Vector3.Lerp(startScale, endScale, tick);
			rectTransform.localScale = localScale;
			yield return null;
		}
	}
	
	private void Awake(){
		rectTransform = GetComponent<RectTransform>();
		cardImage = GetComponent<Image>();
	}
	private void Start(){
		if (isFace){
			cardImage.sprite = face_images[cardIndex];
		}else{
			cardImage.sprite = tail_images[tailIndex];
		}
	}
	public void StartTurn(){
		StartCoroutine(ToggleImage());
	}
}
