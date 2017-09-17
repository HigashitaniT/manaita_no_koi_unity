using UnityEngine;
using System.Collections;
using GamepadInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character2P: MonoBehaviour {

    public GameObject character2P;
    Vector3 charaTransform;

    public GameObject bulletHeart2P;

    public float charaSpeedY2P = 1.0f;
    public float bulletSpeed2P = 5.0f;

    GamePad.Index player;
    GamepadState state;

    public static bool win2P = false;

    Blinker blin;

    const int bullet = 5;

    public static int bulletHold2P;
    private bool buttonAFlag = false;

    public GameObject[] icons2P;

	// Use this for initialization
	void Start ()
    {
       charaTransform = character2P.transform.position;
       blin = GetComponent<Blinker>();
       player = GamePad.Index.Two;
       bulletHold2P = bullet;
	}
	
	// Update is called once per frame
    void Update()
    {
        state = GamePad.GetState(player);

        UpdateLife2P(bulletHold2P);

        if ((Input.GetKey(KeyCode.DownArrow) || state.Down) && charaTransform.y >= -3.5f)
        {
            charaTransform.y -= charaSpeedY2P * 0.1f;
        }

        if ((Input.GetKey(KeyCode.UpArrow) || state.Up) && charaTransform.y <= 4f)
        {
            charaTransform.y += charaSpeedY2P * 0.1f;
        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || state.A == true) && blin.nowFlash == false)
        {
            
            if (buttonAFlag == false)
            {
                buttonAFlag = true;
                if (bulletHold2P > 0)
                {
                    bulletHold2P--;
                    Shot();
                }
            }
        }
        if(state.A == false)
        {
            buttonAFlag = false;
        }

        transform.position = charaTransform;
    }

    void Shot()
    {
        GameObject Heart2P = GameObject.Instantiate(bulletHeart2P, character2P.transform.position, Quaternion.identity)as GameObject;
        Heart2P.GetComponent<Rigidbody2D>().velocity = transform.right.normalized * - bulletSpeed2P;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet1P")
        {
            blin.CharaBlinker();
            Destroy(col.gameObject);
            Character1P.bulletHold1P++;
        }
        if(col.tag == "Boy")
        {
            win2P = true;
            SceneManager.LoadScene("Clear");
        }
    }

    public void UpdateLife2P(int life)
    {
        for (int i = 0; i < icons2P.Length;i++)
        {
            if (i < life) icons2P[i].SetActive(true);
            else icons2P[i].SetActive(false);
        }
    }
}
