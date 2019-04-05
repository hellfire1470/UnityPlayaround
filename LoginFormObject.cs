using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginFormObject : MonoBehaviour {

	[SerializeField] private InputField _inputUsername;
	[SerializeField] private InputField _inputPassword;
	[SerializeField] private Button _loginButton; 
	public Animator Animator { get; private set; }

	public string Username{
		get{ 
			return _inputUsername.text;
		}
	}

	public string Password{
		get{
			return _inputPassword.text;
		}
	}

	EventSystem system;

	public void AddLoginButtonOnClickAction(UnityEngine.Events.UnityAction action){
		_loginButton.onClick.AddListener (action);
	}

	// Use this for initialization
	void Start () {
		system = EventSystem.current;
		Animator = GetComponent<Animator> ();
	}

	public void Update()
	{

		if (Input.GetKeyDown(KeyCode.Tab))
		{
			Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

			if (next != null) {

				InputField inputfield = next.GetComponent<InputField> ();
				if (inputfield != null)
					inputfield.OnPointerClick (new PointerEventData (system));  //if it's an input field, also set the text caret

				system.SetSelectedGameObject (next.gameObject, new BaseEventData (system));
			}
		}
	}

}
