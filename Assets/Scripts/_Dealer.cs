using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Dealer : MonoBehaviour{
	GenericCard TemplateCard;
	int cardIndex = 0;

	public GameObject card;  

	private void Awake(){
		TemplateCard = card.GetComponent<GenericCard>();
	}

	private void OnGUI(){
		if(GUI.Button(new Rect(10,10,100,28),"Hit me!")){
        
			if(cardIndex >= TemplateCard.face_images.Length){
				cardIndex = 0;
				TemplateCard.ToggleImage();
			} else {
				TemplateCard.cardIndex = cardIndex;
				TemplateCard.ToggleImage();
			}

			cardIndex++;
		}
	}
}
