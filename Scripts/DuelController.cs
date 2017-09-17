using GamepadInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DuelController : MonoBehaviour {

	public static DuelController Instance {get;private set;}

	[SerializeField] private CharacterManager cManager1P, cManager2P;
	[SerializeField] private Boy boyCtrl;

    public GamePad.Index player1P;
    public GamePad.Index player2P;
    public GamepadState state1P;
    public GamepadState state2P;

    private Vector3 pos1P,pos2P;

    //public Slider _slider;
    [SerializeField]
	private Text countdownText;
	[SerializeField]
	private int offsetCount;		//相殺数をカウント

	[SerializeField]private Animator duel_Anim;
	[SerializeField]private GameObject duel1P_Chara , duel2P_Chara;

/// <summary>
/// true=1PWin false=2PWin
/// </summary>
	private bool isWiner;//true=1PWin false=2PWin
	
	private float timer;
	private int boubleCount1P;
	private int boubleCount2P;
	public int limit;

    RectTransform rectra1P,rectra2P;

    public int OffsetCount{
		get{ return offsetCount; }
		set{ offsetCount = value; }
	}

	void Awake()
	{
		if(Instance != null)
		{
			enabled = false;
			DestroyImmediate (this);
			return;
		}
		Instance = this;
	}

	// Use this for initialization
	void Start () {
        rectra1P = duel1P_Chara.GetComponent<RectTransform>();
        rectra2P = duel2P_Chara.GetComponent<RectTransform>();
        player1P = GamePad.Index.One;
        player2P = GamePad.Index.Two;
	}
	
	// Update is called once per frame
	void Update () {

        if (offsetCount >= limit){
			GameController.Instance.gameState = GameController.GameState.DuelStandby;
			offsetCount = 0;

		}
		if(GameController.Instance.gameState == GameController.GameState.DuelStandby && countdownText.gameObject.activeSelf == false)
		{
			duel_Anim.gameObject.SetActive(true);
			SetUiChara();
			duel_Anim.enabled = true;
			
			boubleCount1P = 0;
			boubleCount2P = 0;

            StartCoroutine("CountdownCoroutine");
		}
		if(GameController.Instance.gameState == GameController.GameState.Bouble)//デュエルモードが連打
		{
			if(timer <= 3){
				BoubleTypeUpdate();
				return;
			}
			Result();
		}
	}

	private void BoubleTypeUpdate ()
	{
        state1P = GamePad.GetState(player1P,state1P);
        state2P = GamePad.GetState(player2P,state2P);
		Timer();
		if(Input.GetKeyDown(KeyCode.D) || state1P.A_Down){
			boubleCount1P++;
            pos1P.y += 10f;
            rectra1P.transform.position = pos1P;
        }
		if(Input.GetKeyUp(KeyCode.D) || state1P.A_Up){
            pos1P.y -= 10f;
            rectra1P.transform.position = pos1P;
        }
		if(Input.GetKeyDown(KeyCode.LeftArrow) || state2P.A_Down){
			boubleCount2P++;
            pos2P.y += 10f;
            rectra2P.transform.position = pos2P;

        }
		if(Input.GetKeyUp(KeyCode.LeftArrow) || state2P.A_Up){
            pos2P.y -= 10f;
            rectra2P.transform.position = pos2P;
        }
		if(boubleCount1P >= boubleCount2P){
			isWiner = true;
		}
		else
		{
			isWiner = false;
		}
        Debug.Log(boubleCount1P);
        Debug.Log(boubleCount2P);
	}

	private void Timer () {
		timer += Time.deltaTime;
	}

	private void SetUiChara () {
		if(cManager1P.lead.pname == "スミレ"){
			duel1P_Chara.GetComponent<Image>().sprite = cManager1P.spBlue;
		}else{
			Image d1P = duel1P_Chara.GetComponent<Image>();
			d1P.sprite = cManager1P.spPinc;
			d1P.transform.localScale = new Vector3 (-1,1,1);
		}

		if(cManager2P.lead.pname == "スミレ"){
			Image d2P = duel2P_Chara.GetComponent<Image>();
			d2P.sprite = cManager2P.spBlue;
			d2P.transform.localScale = new Vector3 (-1,1,1);
		}else{
			duel2P_Chara.GetComponent<Image>().sprite = cManager2P.spPinc;
		}
	}

	private void Result(){
		timer = 0;
		duel_Anim.SetBool("is1PWin",isWiner);
		duel_Anim.SetBool("is2PWin",!isWiner);

		Invoke("EndAnimation",5.0f);
		GameController.Instance.gameState = GameController.GameState.Shooting;
	}
	void EndAnimation () {
		if(isWiner){
			string tags = "Bullet1P";
			boyCtrl.StartCoroutine(boyCtrl.BoyMove(tags,10));
		}else{
			string tags = "Bullet2P";
			boyCtrl.StartCoroutine(boyCtrl.BoyMove(tags,10));
		}
		duel_Anim.Rebind();
		duel_Anim.gameObject.SetActive(false);
		Pause.MyResume();
	}

	IEnumerator CountdownCoroutine()
	{
		countdownText.gameObject.SetActive(true);
		yield return new WaitForSeconds(2.0f);

		countdownText.text = "レディ";
		yield return new WaitForSeconds(1.0f);
		
		countdownText.text = "GO!";
		yield return new WaitForSeconds(1.0f);
 
		countdownText.text = "";
		countdownText.gameObject.SetActive(false);
        pos1P = rectra1P.position;
        pos2P = rectra2P.position;
        GameController.Instance.gameState = GameController.GameState.Bouble;
	}

}
