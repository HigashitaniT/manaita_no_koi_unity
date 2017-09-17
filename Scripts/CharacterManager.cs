using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterManager : PlayerStatus {

	public GameObject bulletHeartSpr;

    [SerializeField] private Slider sPSlider;
    [SerializeField] private int sPLim = 5;

    [SerializeField] private Animator myCutinAnimator;
    private int blinCount = 0;

	public enum PlayerSelects
	{
		Player1,
		Player2
	}
	public PlayerSelects playerSelects;

    private AudioSource shotSound;

	private int bulletHold = BULLET_MAX;
	public GameObject[] icons;

	private float charY;
	private string bulletTag;

	public static bool IsWin1P = false;
	public static bool IsWin2P = false;

	private PlayerDate _playerDate;

    public CharacterManager enemyCManeger;

	private bool IsAButton;

    public Sprite spBlue ,spPinc;

	public int BulletNum {
		get{ return bulletHold; }
		set{ bulletHold = value; }
	}

    public PlayerDate lead
    {
        get {return _playerDate; }
        set { _playerDate = value; }
    }


	// Use this for initialization
	void Start () {

		//_playerDate1P = new PlayerDate ();
        shotSound = GetComponent<AudioSource>();
		charY = gameObject.transform.position.y;

		switch(playerSelects)
		{
		case PlayerSelects.Player1:
			    player = GamePad.Index.One;
			    bulletTag = "Bullet2P";
                _playerDate = GetStatus1P ();

                if (_playerDate == null) break;
                if (_playerDate.pname == "スミレ")
                {
                    SpriteRenderer _SprRenderer = this.GetComponent<SpriteRenderer>();
                    _SprRenderer.sprite = spBlue;
                    _SprRenderer.flipX = false;
                }
                else
                {
                    SpriteRenderer _SprRenderer = this.GetComponent<SpriteRenderer>();
                    _SprRenderer.sprite = spPinc;
                    _SprRenderer.flipX = true;
                }
                break;

		case PlayerSelects.Player2:
			    player = GamePad.Index.Two;
			    bulletTag = "Bullet1P";
			    _playerDate = GetStatus2P ();

                if (_playerDate == null) break;
                if (_playerDate.pname == "スミレ")
                {
                    SpriteRenderer _SprRenderer = this.GetComponent<SpriteRenderer>();
                    _SprRenderer.sprite = spBlue;
                    _SprRenderer.flipX = true;
                }
                else
                {
                    SpriteRenderer _SprRenderer = this.GetComponent<SpriteRenderer>();
                    _SprRenderer.sprite = spPinc;
                    _SprRenderer.flipX = false;
                }
                break;
		}

		if (_playerDate == null) 
		{
			_playerDate = new PlayerDate ("サクラ", 1, 5, 1, 2);
		}
	}
	
	// Update is called once per frame
	void Update () {
		state = GamePad.GetState (player);

        Debug.Log(lead);

        UpdateLife (bulletHold);
        sPSlider.value = blinCount;
        if(sPLim <= blinCount)
        {
            blinCount = 0;
            Special();
        }

        if(GameController.Instance.gameState == GameController.GameState.Shooting){
            switch (playerSelects)
            {
                case PlayerSelects.Player1:
                    InputKey1P();
                    break;
                case PlayerSelects.Player2:
                    InputKey2P();
                    break;
            }
        }

		transform.position = new Vector3(transform.position.x,charY,0);
	}

	void Shot()
	{
        shotSound.Play();
		GameObject Heart = Instantiate (bulletHeartSpr,
						 this.transform.position,
			             Quaternion.identity
						);
        GameController.Instance.pauseList.Add(Heart.GetComponent<Pause>());
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == bulletTag)
		{
			blin.CharaBlinker();
            blinCount++;
			//Destroy(col.gameObject);
			//bulletHold++;
		}
		if (col.tag == "Boy")
		{
			SceneManager.LoadScene("Clear");

			switch (playerSelects) {
			case PlayerSelects.Player1:
				IsWin1P = true;
				break;
			case PlayerSelects.Player2:
				IsWin2P = true;
				break;
			}
		}
	}

	public void UpdateLife(int life)
	{
		for (int i = 0; i < icons.Length; i++)
		{
			if (i < life) icons[i].SetActive(true);
			else icons[i].SetActive(false);
		}
	}

    public void InputKey1P()
    {

        if ((Input.GetKey(KeyCode.S) || state.Down) && charY >= -3.5f)
        {
            charY -= (_playerDate.speed + 1) * Time.deltaTime;
        }

        if ((Input.GetKey(KeyCode.W) || state.Up) && charY <= 4f)
        {
            charY += (_playerDate.speed + 1) * Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.D) || state.A) && blin.nowFlash == false)
        {
            if (IsAButton == false)
            {
                IsAButton = true;
                if (bulletHold > 0)
                {
                    bulletHold--;
                    Shot();
                }
            }
        }
        if (state.A == false)
        {
            IsAButton = false;
        }
    }

    public void InputKey2P()
    {
        if ((Input.GetKey(KeyCode.DownArrow) || state.Down) && charY >= -3.5f)
        {
            charY -= (_playerDate.speed + 1) * Time.deltaTime;
        }

        if ((Input.GetKey(KeyCode.UpArrow) || state.Up) && charY <= 4f)
        {
            charY += (_playerDate.speed + 1) * Time.deltaTime;
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || state.A) && blin.nowFlash == false)
        {
            if (IsAButton == false)
            {
                IsAButton = true;
                if (bulletHold > 0)
                {
                    bulletHold--;
                    Shot();
                }
            }
        }
        if (state.A == false)
        {
            IsAButton = false;
        }
    }
    public void Special() {
        GameController.Instance.gameState = GameController.GameState.Cutin;
        myCutinAnimator.SetBool("isCutin",true);
        StartCoroutine(SpecialWeapon.Instance.SpecialStart(this,enemyCManeger));
        Invoke("AnimStop",2f);
        //SpecialWeapon.Instance.Invocation(this, enemyCManeger);
    }

    void AnimStop (){
        myCutinAnimator.SetBool("isCutin",false);
    }

}
