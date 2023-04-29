using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BackMenuButton : MonoBehaviour
{	void OnMouseDown()
	{
		 SceneManager.LoadScene("MainManu");
	}
}