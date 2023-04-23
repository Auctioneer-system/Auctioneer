using UnityEngine;
using System.Collections;

public class deleteCollisionOutofRange : MonoBehaviour {

  //メインカメラに付いているタグ名
  public const string MAIN_CAMERA_TAG_NAME = "MainCamera";

  //カメラに表示されているか
  private bool _isRendered = false;

  private void Update () {
    //カメラに写っていれば当り判定有効
    if(GetComponent<Collider> ()){
      GetComponent<Collider> ().enabled = _isRendered;
    }
    if(GetComponent<Collider2D> ()){
      GetComponent<Collider2D> ().enabled = _isRendered;
    }
    _isRendered = false;
  }

  private void OnWillRenderObject(){
    //メインカメラに映った時だけ_isRenderedを有効に
    if(Camera.current.tag == MAIN_CAMERA_TAG_NAME){
      _isRendered = true;
    }
  }

}