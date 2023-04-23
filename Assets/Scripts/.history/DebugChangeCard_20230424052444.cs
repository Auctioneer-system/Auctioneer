
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugChangeCard : MonoBehaviour {

    GenericCard generic_card;

    public GameObject cardObject;


    private void Awake()
    {
        generic_card = cardObject.GetComponent<GenericCard>();

    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(100,100,1000,280), "Hit me!"))　　
         {
                 StartCoroutine(generic_card.DiscardToSide(125));　
                Debug.Log("Callled!");
         }

    }

}
