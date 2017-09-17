using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pause : MonoBehaviour {

	static List<Pause> targets = new List<Pause>();	// ポーズ対象のスクリプト
  	Behaviour[] pauseBehavs = null;	// ポーズ対象のコンポーネント

	// 初期化
	void Start() {
		// ポーズ対象に追加する
		targets.Add(this);
	}

	// 破棄されるとき
	void OnDestory() {
		// ポーズ対象から除外する
		targets.Remove(this);
	}

	// ポーズされたとき
	void OnPause() {
		if ( pauseBehavs != null ) {
		return;
		}

		// 有効なBehaviourを取得
		pauseBehavs = Array.FindAll(GetComponentsInChildren<Behaviour>(), (obj) => { 
		if(obj == null){
		return false;
		}
		return obj.enabled; });

		foreach ( var com in pauseBehavs ) {
		com.enabled = false;
		}
	}

	// ポーズ解除されたとき
	void OnResume() {
		if ( pauseBehavs == null ) {
		return;
		}

		// ポーズ前の状態にBehaviourの有効状態を復元
		foreach ( var com in pauseBehavs ) {
			if(com != null){
				com.enabled = true;
			}
		}
		pauseBehavs = null;
	}

	// ポーズ
	public static void MyPause() {
		foreach ( Pause obj in GameObject.FindObjectsOfType<Pause>() ) {
		//Debug.Log (obj.gameObject.name);
		if (obj != null) {
			obj.OnPause ();
		}
		}
	}

	// ポーズ解除
	public static void MyResume() {
		foreach ( var obj in targets ) {
		obj.OnResume();
		}
	}
}
