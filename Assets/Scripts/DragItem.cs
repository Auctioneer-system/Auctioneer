using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems; 
using UnityEngine;

public class DragItem : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    private bool _isTouched = false; 
    private Vector3 _prevTouchPosi; 
    private　int _touchIndex = 0;  

    
    void Start()
    {
        
    }

    
    void Update()
    {
        Touch touchinfo;
        Vector3 nowposi;
        Vector3 diff;

        if (_isTouched) {
            touchinfo = Input.GetTouch(_touchIndex);
            if (touchinfo.phase != TouchPhase.Ended && touchinfo.phase != TouchPhase.Canceled) {
                
                
                nowposi = Camera.main.ScreenToWorldPoint(touchinfo.position);
                
                diff = nowposi - _prevTouchPosi;
                
                GetComponent<Transform>().position += diff;
                
                _prevTouchPosi = nowposi;
            } else {
                
                _isTouched = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Touch taouchinfo;
        Ray ray;
        RaycastHit[] hits = new RaycastHit[10]; 
        
        int hitNum;

        Debug.Log("Down" + GetComponent<Transform>().name);

        for (int i = 0; i < Input.touchCount; i++) {
            
            taouchinfo = Input.GetTouch(i);
            
            ray = Camera.main.ScreenPointToRay(taouchinfo.position);
            
            hitNum = Physics.RaycastNonAlloc(ray, hits);
            for (int j = 0; j < hitNum; j++) {
                
                
                if (hits[j].collider == GetComponent<BoxCollider>()) {
                    
                    _touchIndex = i;
                    
                    _isTouched = true;
                    
                    _prevTouchPosi = Camera.main.ScreenToWorldPoint(taouchinfo.position);
                    break;
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up" + GetComponent<Transform>().name);
    }
}
