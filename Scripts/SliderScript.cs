using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {

	private AudioSource offsetSound;
	private Slider sl;
	public int lim = 10;
	[SerializeField]
	//private int nowMeter = 0;
	private int count = 0;
	
	static public SliderScript instans { get; private set;}

	void Awake () {
		if (instans == null) {
			instans = this;
		} else {
			DestroyImmediate (this);
		}
	}

	// Use this for initialization
	void Start () {
		sl = GetComponent<Slider> ();
		offsetSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(sl.value == 0) return;
		if(DuelController.Instance.OffsetCount == 0){
			sl.value = 0;
		}
	}

	public void AddMeter () {
		count++;
		offsetSound.Play();
		if (count == 2) {
			DuelController.Instance.OffsetCount++;
			StartCoroutine ("Smooth");
			count = 0;
		}
	}

	IEnumerator Smooth () {
		while(sl.value <= DuelController.Instance.OffsetCount){
			sl.value += 0.1f;
			yield return null;
		}
	}

}
