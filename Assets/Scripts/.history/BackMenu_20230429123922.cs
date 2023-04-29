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
	public bool isDraggable;
	void OnMouseDrag()
	{
		if (!isDraggable) return;
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
		transform.position = currentPosition;

	}


	void OnMouseUp()
	{
		if (isHighlight)
		{
			isHighlight = false;
			spriteRenderer.color = new Color(1f, 1f, 1f); // カラーを元に戻す
		}

		if (!isHanded)
		{
			transform.position = prevPos;
		}
		else
		{
			isDraggable = false;
		}
	}
	public bool isHanded;
	void Update()
	{
		if (isHanded)
		{
			// カードが提出済みの時のカード固有の処理
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

		this.gameObject.SetActive(false);
		Debug.Log("Discarded");
	}

	public void toTop()
	{
		transform.SetAsFirstSibling();
		Debug.Log("toTop" + cardIndex);
	}
}