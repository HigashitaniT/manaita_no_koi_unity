using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static GameController Instance { get; private set;}

	public Text countText;

	[SerializeField] private GameObject character1PObj,character2PObj,boyObj;

	public List <Pause> pauseList = new List <Pause>();

	

	//float timer = 0;

	private bool isFirst = false;
	
	/// <summary>
	/// ゲームのステートマシン
	/// </summary>
	public enum GameState
    {
        Ready,
        Shooting,
		DuelStandby,
		Bouble,
		Command,
		Cutin,

    }
	public GameState gameState;

	void Awake(){
		if (Instance == null) {
			Instance = this;
		} else {
			DestroyImmediate (this);
		}
	}

	// Use this for initialization
	void Start () {
		gameState = GameState.Ready;
		//SetPause();
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameState) {
		case GameState.Ready:
			StateReady ();
			break;
		case GameState.Shooting:
			StateShooting();
			break;
		case GameState.DuelStandby:
			StateDuelStandby();
			break;
		case GameState.Bouble:
			break;
		case GameState.Command:
			break;
		case GameState.Cutin:
			StateCutin();
			break;
		}
	}

	void StateReady() {
		if (!isFirst) {
			StartCoroutine ("CountdownCoroutine");
			isFirst = true;
		}
	}

	void StateShooting () {
		Pause.MyResume();
	}

	void StateDuelStandby (){
		//timer += 0.01f;
		//Debug.Log(timer);
		//if(timer >= 10)gameState = GameState.Shooting;
		StopPause();
	}

	void StateBouble (){

	}

	void StateCommand (){

	}
	void StateCutin () {
		StopPause();
		
	}

	IEnumerator CountdownCoroutine()
	{
		countText.gameObject.SetActive(true);

		countText.text = "3";
		yield return new WaitForSeconds(1.0f);

		countText.text = "2";
		yield return new WaitForSeconds(1.0f);

		countText.text = "1";
		yield return new WaitForSeconds(1.0f);

		countText.text = "GO!";
		yield return new WaitForSeconds(1.0f);

		countText.text = "";
		countText.gameObject.SetActive(false);

		gameState = GameState.Shooting;
	}

	void SetPause (){

		if(!pauseList.Exists(x => x == character1PObj)){
			pauseList.Add(character1PObj.GetComponent<Pause>());
			pauseList.Add(character2PObj.GetComponent<Pause>());
			pauseList.Add(boyObj.GetComponent<Pause>());
		}
	}
	void StopPause () {
		Pause.MyPause();
		// for (int i = 0; i < pauseList.Count; i++){
		// 	pauseList[i].GetComponent<Pause>().m
		// }
	}

}
