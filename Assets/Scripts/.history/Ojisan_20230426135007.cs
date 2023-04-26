using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Ojisan : MonoBehaviour
{
	private struct SpriteStructure{
		public Sprite[] sprite;
		public int index;
	}
	public Sprite[] Body;
	public Sprite[] Face;
	public Sprite[] Beard;
	public Sprite[] Glasses;
	public Sprite[] Hat;
	public Sprite[] Clothes;
	public Sprite[] Mouth;
	public int bodyIndex;
	public int faceIndex;
	public int beardIndex;
	public int glassesIndex;
	public int hatIndex;
	public int ClothesIndex;
	public int mouthesIndex;
	RectTransform rectTransform;
	public bool isFace = true;
	bool isHighlight;
	Ojisan model;
	SpriteRenderer spriteRenderer;

	public AnimationCurve scaleCurve;
	public float duration = 0.5f;

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		model = GetComponent<Ojisan>();
	}
	private void Start()
	{
			// spriteRenderer.sprite = face_images[cardIndex < face_images.Length - 1 ? cardIndex : 0];
			// spriteRenderer.sprite = tail_images[cardIndex < tail_images.Length - 1 ? cardIndex : 0];
	}
	void OnMouseDown()
	{
	}
	void OnMouseDrag()
	{
	}


	void OnMouseUp()
	{
	}
	public bool isHanded;
	void Update()
	{
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

}