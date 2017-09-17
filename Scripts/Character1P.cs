using UnityEngine;
using System.Collections;
using GamepadInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class Character1P: PlayerStatus {

    public GameObject character1P;
    Vector3 charaTransform;

    public GameObject bulletHeart1P;

    public float charaSpeedY1P = 1.0f;
    public float bulletSpeed1P = 5.0f;

    //GamePad.Index player;
    //GamepadState state;

    public static bool win1P = false;

    //Blinker blin;

    const int bullet = 5;

    public static int bulletHold1P = bullet;
    private bool buttonAFlag = false;

    public GameObject[] icons1P;

	// Use this for initialization
	void Start () 
    {
       charaTransform = character1P.transform.position;
       blin = GetComponent<Blinker>();
       player = GamePad.Index.One;
       bulletHold1P = bullet;
	}
	
	// Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(player);

        UpdateLife1P(bulletHold1P);

        if ((Input.GetKey(KeyCode.S) || state.Down) && charaTransform.y >= -3.5f)
        {
            charaTransform.y -= charaSpeedY1P * 0.1f;
        }

        if ((Input.GetKey(KeyCode.W) || state.Up) && charaTransform.y <= 4f)
        {
            charaTransform.y += charaSpeedY1P * 0.1f;
        }

        if ((Input.GetKeyDown(KeyCode.D) || state.A) && blin.nowFlash == false)
        {
            if (buttonAFlag == false)
            {
                buttonAFlag = true;
                if (bulletHold1P > 0)
                {
                    bulletHold1P--;
                    Shot();
                }
            }
        }
        if (state.A == false)
        {
            buttonAFlag = false;
        }
        transform.position = charaTransform;
    }

    void Shot()
    {
        GameObject Heart1P = GameObject.Instantiate(bulletHeart1P, character1P.transform.position, Quaternion.identity) as GameObject;

        Heart1P.GetComponent<Rigidbody2D>().velocity = transform.right.normalized * bulletSpeed1P;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet2P")
        {
            blin.CharaBlinker();
            Destroy(col.gameObject);
            Character2P.bulletHold2P++;
        }
        if (col.tag == "Boy")
        {
            win1P = true;
            SceneManager.LoadScene("Clear");
        }
    }
    public void UpdateLife1P(int life)
    {
        for (int i = 0; i < icons1P.Length; i++)
        {
            if (i < life) icons1P[i].SetActive(true);
            else icons1P[i].SetActive(false);
        }
    }
}
