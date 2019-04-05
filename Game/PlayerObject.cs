using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerObject : MonoBehaviour {

	private Player _player;

	public Player Player {
		get {
			return _player;
		}
	}

	// Use this for initialization
	void Awake () {
		_player = new Player ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
