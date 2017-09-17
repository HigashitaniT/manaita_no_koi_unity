using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectStatusController : MonoBehaviour {

	public CharacterStatus _charaSta1P;
	public CharacterStatus _charaSta2P;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (_charaSta1P.IsDecision && _charaSta2P.IsDecision) {
			SceneManager.LoadScene ("Main");
		}
	}
}
