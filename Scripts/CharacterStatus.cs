using UnityEngine;
using System.Collections;
using GamepadInput;
using System;
using UnityEngine.UI;

public class CharacterStatus : PlayerStatus {

    public enum Playernum
    {
        Player1,
        Player2
    }

    public Playernum playernum;

	private int status = 5;

	private Vector3 defCursorPos;

    public GameObject panelSpeed;
    public GameObject panelPower;
    public GameObject panelBulletSpeed;
	public GameObject panelBulletType;

	public Text statusName;
    public GameObject[] statusSpeed;
    public GameObject[] statusPower;
    public GameObject[] statusBulletSpeed;
	public Text statusBulletType;

	public GameObject[] charSprite;

    public int statePointMax = 8;
    public int nowStatePoint = 0;

	[Range(1,5)]
	private int name = 0, speed = 1, power = 1, bulletSpeed = 1, bulletType = 0;

	private string[] Type = { "ぴゅあ", "はつらつ", "おくて", "ふしぎちゃん" };
	private string[] Name = { "スミレ", "サクラ" };

	private int cursorCount = 0;
    Vector3 pos;
    float saY;

	private AudioSource selectSound;

	private bool IsDecision1P = false;//決定フラグ
	private bool IsDecision2P = false;//

	public bool IsDecision = false;

	// Use this for initialization
	void Start () {
		defCursorPos = this.transform.position;
        pos = transform.position;
        saY = panelSpeed.transform.position.y - panelPower.transform.position.y;
        selectSound = GetComponent<AudioSource>();

        switch(playernum)
        {
            case Playernum.Player1:
                player = GamePad.Index.One;
				//_playerDate1P = new PlayerDate ();
                break;
            case Playernum.Player2:
                player = GamePad.Index.Two;
				//_playerDate2P = new PlayerDate ();
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        state = GamePad.GetState(player, state);

        switch (playernum)
        {
            case Playernum.Player1:
                CursorPlayer1();
                break;
            case Playernum.Player2:
                CursorPlayer2();
                break;
        }
		UpdateStatus ();//一番下の更新まとめで更新される
		if (IsDecision1P || IsDecision2P) {
			IsDecision = true;
		}
	}

    void CursorPlayer1()
    {
        //決定処理
        if (Input.GetKeyDown(KeyCode.Space) || state.Start)
		{
			if (!IsDecision1P) {
				IsDecision1P = true;
				//SetStatus1P(Name[name],power,speed,bulletSpeed,bulletType);
				_playerDate1P = new PlayerDate(Name[name],power,speed,bulletSpeed,bulletType);
			} else {
				IsDecision1P = false;
			}
		}
		//決定されてたら戻る
		if (IsDecision1P) return;
		//下キー
        if ((Input.GetKeyDown(KeyCode.S) || state.Down_Down) && cursorCount < 3)
        {
			DownCursor ();
        }
		//上キー
        if ((Input.GetKeyDown(KeyCode.W) || state.Up_Down) && cursorCount > -1)
        {
			UpCursor ();
        }
        transform.position = pos;

		//右キー
		if (Input.GetKeyDown(KeyCode.D) || state.Right_Down)
		{
			RightCursor ();
		}
		//左キー
		if (Input.GetKeyDown(KeyCode.A) || state.Left_Down)
		{
			LeftCursor ();
		}
    }

    void CursorPlayer2()
    {
		//決定処理
		if(Input.GetKeyDown(KeyCode.RightShift) || state.Start)
		{
			if (!IsDecision2P) {
				IsDecision2P = true;
				//SetStatus2P(Name[name],power,speed,bulletSpeed,bulletType);
				_playerDate2P = new PlayerDate(Name[name],power,speed,bulletSpeed,bulletType);
			} else {
				IsDecision2P = false;
			}
		}

		//決定されてたら戻る
		if (IsDecision2P) return;

		//下キー
		if ((Input.GetKeyDown(KeyCode.DownArrow) || state.Down_Down) && cursorCount < 3)
		{
			DownCursor ();
		}

		//上キー
		if ((Input.GetKeyDown(KeyCode.UpArrow) || state.Up_Down) && cursorCount > -1)
		{
			UpCursor ();
		}

		transform.position = pos;

		//右キー
		if (Input.GetKeyDown(KeyCode.RightArrow) || state.Right_Down)
		{
			RightCursor ();
		}

		//左キー
		if (Input.GetKeyDown(KeyCode.LeftArrow) || state.Left_Down)
		{
			LeftCursor ();
		}
    }

	public void UpdateStatusIcon(int status,GameObject[] panel)
	{
		for (int i = 0; i < panel.Length; i++)
		{
			if (i < status) panel[i].SetActive(true);
			else panel[i].SetActive(false);
		}
	}
	public void UpdateStatus()
	{
		UpdateStatusIcon (speed, statusSpeed);
		UpdateStatusIcon (power, statusPower);
		UpdateStatusIcon (bulletSpeed, statusBulletSpeed);
		statusName.text = Name [name];
		statusBulletType.text = Type [bulletType];

		for (int i = 0; i < charSprite.Length; i++) 
		{
			charSprite [i].SetActive (false);
		}
		charSprite [name].SetActive (true);
	}
	private void UpCursor()//上キー
	{
		selectSound.Play();
		cursorCount -= 1;
		if (cursorCount == -1) {
			pos = statusName.transform.position;
		} else {
			pos.y += saY;
		}
	}
	private void DownCursor()//下キー
	{
		selectSound.Play();
		cursorCount += 1;
		if (cursorCount == 0) {
			pos = defCursorPos;
		} else {
			pos.y -= saY;
		}
	}
    private void RightCursor()//右キー
    {
		selectSound.Play();
        if (cursorCount == -1)
        {
            name = (int)Mathf.Repeat(name + 1, 2);
        }
        else if (cursorCount == 0 && nowStatePoint < statePointMax)
        {
            speed = (int)Mathf.Clamp(speed + 1, 1f, 5f);
        }
        else if (cursorCount == 1 && nowStatePoint < statePointMax)
        {
            power = (int)Mathf.Clamp(power + 1, 1f, 5f);
        }
        else if (cursorCount == 2 && nowStatePoint < statePointMax)
        {
            bulletSpeed = (int)Mathf.Clamp(bulletSpeed + 1, 1f, 5f);
        }
        else if (cursorCount == 3)
        {
            bulletType = (int)Mathf.Repeat(bulletType + 1, 4);
        }
        nowStatePoint = speed + power + bulletSpeed;
    }
    private void LeftCursor()//左キー
    {
		selectSound.Play();
        if (cursorCount == -1)
        {
            name = (int)Mathf.Repeat(name - 1, 2);
        }
        else if (cursorCount == 0 && nowStatePoint <= statePointMax)
        {
            speed = (int)Mathf.Clamp(speed - 1, 1f, 5f);
        }
        else if (cursorCount == 1 && nowStatePoint <= statePointMax)
        {
            power = (int)Mathf.Clamp(power - 1, 1f, 5f);
        }
        else if (cursorCount == 2 && nowStatePoint <= statePointMax)
        {
            bulletSpeed = (int)Mathf.Clamp(bulletSpeed - 1, 1f, 5f);
        }
        else if (cursorCount == 3)
        {
            bulletType = (int)Mathf.Repeat(bulletType - 1, 4);
        }
        nowStatePoint = speed + power + bulletSpeed;
    }
}
