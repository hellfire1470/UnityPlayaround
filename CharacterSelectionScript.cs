using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CharacterSelectionScript : MonoBehaviour {

	[SerializeField] private Button _logoutButton;

	public void AddLogoutButtonAction(UnityAction action){
		_logoutButton.onClick.AddListener (action);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
