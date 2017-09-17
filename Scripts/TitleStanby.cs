using UnityEngine;
using System.Collections;
using GamepadInput;
using UnityEngine.SceneManagement;

public class TitleStanby : MonoBehaviour {

    public GameObject RedHart;
    public GameObject BlueHart;

    GamePad.Index player1;
    GamePad.Index player2;
    GamepadState state1;
    GamepadState state2;

    public GameObject howTo;

    bool A1p = false;
    bool A2p = false;

    public bool clearScene = false;

    void Start()
    {
        player1 = GamePad.Index.Two;
        player2 = GamePad.Index.One;
    }

	// Update is called once per frame
	void Update () 
    {
        state1 = GamePad.GetState(player1);
        state2 = GamePad.GetState(player2);

        if (state1.B || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RedHart.SetActive(true);
            A1p = true;
        }
        if (state2.B || Input.GetKeyDown(KeyCode.D))
        {
            BlueHart.SetActive(true);
            A2p = true;
        }

        if(clearScene == false && (state1.A || state2.A || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow)))
        {
            howTo.SetActive(true);
        }
        else if(clearScene == false)
        {
            howTo.SetActive(false);
        }


        if(A1p == true && A2p == true)
        {
            if (clearScene == false)
            {
				SceneManager.LoadScene ("Status");
            }
            else
            {
				SceneManager.LoadScene ("Title");
            }
            
        }
	}
}
