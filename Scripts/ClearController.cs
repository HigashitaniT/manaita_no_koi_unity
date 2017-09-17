using UnityEngine;
using System.Collections;

public class ClearController : MonoBehaviour {

    public GameObject UI1P;
    public GameObject UI2P;

	void Start () {
	    if (CharacterManager.IsWin1P == true)
        {
            UI1P.SetActive(true);
        }
        if (CharacterManager.IsWin2P == true)
        {
            UI2P.SetActive(true);
        }

        CharacterManager.IsWin1P = false;
        CharacterManager.IsWin2P = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
