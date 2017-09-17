using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerStatus : MonoBehaviour {

	public GamePad.Index player;
	public GamepadState state;
	public Blinker blin;

	public static PlayerDate _playerDate1P;
	public static PlayerDate _playerDate2P;

	public const int BULLET_MAX = 5;

	public class PlayerDate
	{
		public string pname = "";
		public int power = 1;
		public int speed = 1;
		public int bulletSpeed = 1;
		public int bulletType = 1;

		public PlayerDate(string pname, int power, int speed, int bulletSpeed, int bulletType)
		{
			this.pname = pname;
			this.power = power;
			this.speed = speed;
			this.bulletSpeed = bulletSpeed;
			this.bulletType = bulletType;
		}
	}


	public static void SetStatus1P(string pname, int power, int speed, int bulletSpeed, int bulletType)
	{
		//_playerDate1P = new PlayerDate ();
		_playerDate1P = new PlayerDate (pname, power, speed, bulletSpeed, bulletType);
		//Debug.Log (status1P.power);
	}
	public static void SetStatus2P(string pname, int power, int speed, int bulletSpeed, int bulletType)
	{
		//_playerDate2P = new PlayerDate ();
		_playerDate2P = new PlayerDate (pname, power, speed, bulletSpeed, bulletType);
	}

	public PlayerDate GetStatus1P(){
		return _playerDate1P;
	}

	public PlayerDate GetStatus2P(){
		return _playerDate2P;
	}
}
