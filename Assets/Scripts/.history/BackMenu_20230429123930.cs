using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BackMenuButton : MonoBehaviour
{	void OnMouseDown()
	{
		if (!isDraggable) return;
		prevPos = transform.position;
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		isHighlight = true;
		spriteRenderer.color = new Color(1f, 1f, 0f); // ハイライトの色を黄色に設定
		transform.SetAsFirstSibling();
	}

}