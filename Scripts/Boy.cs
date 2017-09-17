using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {

    Blinker blin;

    private AudioSource blinkSound;
    bool isMoving = false;

    float movY = 0;

    Vector3 boyPosition;

    // Use this for initialization
	void Start () 
    {
        boyPosition = this.transform.position;
        blin = GetComponent<Blinker>();
        blinkSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameController.Instance.gameState == GameController.GameState.Shooting){
            float ranY = Random.Range(0.02f, -0.02f);
            
            if (transform.localPosition.y <= -3.5f)
            {
                movY += 0.03f;
            }
            if (transform.localPosition.y >= 4f)
            {
                movY -= 0.03f;
            }
            else
            {
                movY += ranY * Time.deltaTime * 50;
            }
            boyPosition += new Vector3(0, movY, 0);
            transform.localPosition = boyPosition;

            boyPosition = (new Vector3(
                Mathf.Clamp(boyPosition.x, -9f, 9f),
                Mathf.Clamp(boyPosition.y, -3.5f, 4f),
                0));
        }
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(isMoving == false)
        {
            float p = col.GetComponent<BulletController>()._playerDate.power;
            blin.CharaBlinker();
            blinkSound.Play();
            StartCoroutine(BoyMove(col.tag, p));
        }
    }

    public IEnumerator BoyMove(string tag,float p)
    {
        isMoving = true;
        float timer = 0;
        
        while(timer <= 60)
        {
            if (tag == "Bullet1P")
            {
                boyPosition -= new Vector3(p*Time.deltaTime/3, 0, 0);
            }

            if (tag == "Bullet2P")
            {
                boyPosition += new Vector3(p*Time.deltaTime/3, 0, 0);
            }

            transform.localPosition = boyPosition;
            timer++;
            yield return new WaitForSeconds(0.01f); 
        }
        isMoving = false;
        timer = 0;
        yield break;
    }
}
