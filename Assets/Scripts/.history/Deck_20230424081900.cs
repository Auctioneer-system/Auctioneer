using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Deck : MonoBehaviour {
    private void Start(){
        Component[] components = gameObject.GetComponentsInChildren<GenericCard>();

        foreach (GenericCard component in components)
            {
                // 取得した子オブジェクトのコンポーネントに対して処理を行う
				component.isDraggable=true;
            }
    }
}