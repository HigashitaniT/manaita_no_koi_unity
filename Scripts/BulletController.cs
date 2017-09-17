using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	CharacterManager myCharacter;
	CharacterManager enemyCharacter;

    string myWallTag;

    public PlayerStatus.PlayerDate _playerDate;

	// Use this for initialization
	void Start () {

		if (this.tag == "Bullet1P")
		{
            myWallTag = "WallLeft";

			myCharacter = GameObject.FindGameObjectWithTag ("Character1P").
                          GetComponent<CharacterManager>();
			
            enemyCharacter = GameObject.FindGameObjectWithTag ("Character2P").
                             GetComponent<CharacterManager>();

            _playerDate = myCharacter.lead;

            //GetComponent<Rigidbody2D>().velocity = transform.right.normalized * _playerDate.bulletSpeed;
            var moveHashX = new Hashtable();
            moveHashX.Add("x", 10f);
            moveHashX.Add("time", (_playerDate.bulletSpeed - 7) * -2);//到達時間なので短い方が早い
            if(_playerDate.bulletType == 0)
            {
                moveHashX.Add("easetype", "Linear");
            }
            else if (_playerDate.bulletType == 1)
            {
                moveHashX.Add("easetype", "easeInSine");
            }
            else if (_playerDate.bulletType == 2)
            {
                moveHashX.Add("easetype", "easeOutSine");
            }
            else if (_playerDate.bulletType == 3)
            {
                iTween.EaseType randomEase = (iTween.EaseType)Random.Range((float)iTween.EaseType.easeInQuad,(float)iTween.EaseType.punch);
                moveHashX.Add("easetype", randomEase);
            }
            iTween.MoveTo(this.gameObject, moveHashX);
            
        }
		else if (this.tag == "Bullet2P") 
		{
            myWallTag = "WallRight";

            myCharacter = GameObject.FindGameObjectWithTag("Character2P").
                          GetComponent<CharacterManager>();

            enemyCharacter = GameObject.FindGameObjectWithTag("Character1P").
                             GetComponent<CharacterManager>();

            _playerDate = myCharacter.lead;

            //GetComponent<Rigidbody2D>().velocity = -transform.right.normalized * _playerDate.bulletSpeed;
            var moveHashX = new Hashtable();
            moveHashX.Add("x", -10f);
            moveHashX.Add("time", (_playerDate.bulletSpeed - 7) * -2);
            if (_playerDate.bulletType == 0)
            {
                moveHashX.Add("easetype", "Linear");
            }
            else if (_playerDate.bulletType == 1)
            {
                moveHashX.Add("easetype", "easeInSine");
            }
            else if (_playerDate.bulletType == 2)
            {
                moveHashX.Add("easetype", "easeOutSine");
            }
            else if (_playerDate.bulletType == 3)
            {
                iTween.EaseType randomEase = (iTween.EaseType)Random.Range((float)iTween.EaseType.easeInQuad, (float)iTween.EaseType.punch);
                moveHashX.Add("easetype", randomEase);
            }
            iTween.MoveTo(this.gameObject, moveHashX);
        }
    }
	
	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == myCharacter.tag || col.tag == this.tag || col.tag == myWallTag) {
			return;
		} else if (col.tag == "Bullet1P" || col.tag == "Bullet2P") {
			SliderScript.instans.AddMeter();
            GetComponent<Collider2D>().enabled = false;
            iTween.Stop(gameObject);
            var moveHash = new Hashtable();
            moveHash.Add("Position",new Vector3(0,5,0));
            moveHash.Add("time",1f);
            iTween.MoveTo(gameObject,moveHash);
            Invoke("DestroyBullet",1f);
		}else{
            Destroy(gameObject);
        }
        myCharacter.GetComponent<CharacterManager>().BulletNum++;
	}

    void DestroyBullet () {
        Destroy (this.gameObject);
    }

}
