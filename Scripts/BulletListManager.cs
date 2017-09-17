using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletListManager : MonoBehaviour {

	public GameObject[] panel1P;
	public GameObject[] panel2P;

	public CharacterManager _CaracterManag1P;
	public CharacterManager _CaracterManag2P;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BulletPlus(CharacterManager chara)
    {
        chara.BulletNum++;
        UpdateLife(chara.BulletNum, chara.playerSelects);
    }
    public void BulletMinus(CharacterManager chara)
    {
        chara.BulletNum--;
        UpdateLife(chara.BulletNum, chara.playerSelects);
    }

    public void UpdateLife(int life, CharacterManager.PlayerSelects Player)
	{
		GameObject[] panel = new GameObject[5];
        if (Player == CharacterManager.PlayerSelects.Player1)
        {
			panel = panel1P;
        }
        else if (Player == CharacterManager.PlayerSelects.Player2)
        {
			panel = panel2P;
		}
		for (int i = 0; i < panel.Length; i++)
		{
			if (i < life) panel[i].SetActive(true);
			else panel[i].SetActive(false);
		}
	}

}
