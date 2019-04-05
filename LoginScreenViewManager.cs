using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Network;
using Network.Data;

public class LoginScreenViewManager : MonoBehaviour {
	
	[SerializeField] Animator _cameraAnimator;
	[SerializeField] Animator _characterFormAnimator;
	[SerializeField] LoginCharList _loginCharList;
	[SerializeField] CharacterSelectionScript _characterSelection;
	[SerializeField] LoginFormObject _loginForm;

	private Authorization _authorization;

	// Use this for initialization
	void Start () {

		_authorization = new Authorization (Manager<UdpClient>.Get ("Client"));
		_authorization.LoggedIn += OnLoggedIn;
		_authorization.LoginFail += OnLoginFailed;
		_authorization.LoggedOut += OnLoggedOut;

		_authorization.UserCharactersReceived += OnUserCharactersReceived;

		_loginCharList.CharacterSelected += OnCharacterSelectedLocal;

		_loginForm.AddLoginButtonOnClickAction (delegate {
			_authorization.Login (_loginForm.Username, _loginForm.Password);
		});

		_characterSelection.AddLogoutButtonAction (delegate {
			_authorization.Logout ();
		});

		_authorization.CharacterSelected += OnCharacterSelected;

	}

	private void OnCharacterSelectedLocal(object sender, CharacterSelectedArgs e){
		_authorization.SelectChar (e.CharacterId);
	}

	private void OnUserCharactersReceived(object sender, UserCharactersArgs e){
		DoOnMainThread.ExecuteOnMainThread.Enqueue(delegate{
			ViewCharData(e.UserCharactersData);
		});
	}

	private void OnLoggedOut(object sender, System.EventArgs e){
		Debug.Log ("OnLoggedOut");
		DoOnMainThread.ExecuteOnMainThread.Enqueue(delegate{
			ViewLoginScreen();
		});
	}

	private void OnCharacterSelected(object sender, CharacterSelectArgs e){
		// todo:: add authorization.joinGame
		DoOnMainThread.ExecuteOnMainThread.Enqueue(delegate{
			JoinGame();
		});
	}

	private void OnLoggedIn(object sender, UserDataArgs e){
		DoOnMainThread.ExecuteOnMainThread.Enqueue(delegate{
			_authorization.RequestUserCharacters();
			ViewCharacterSelection();
			//RequestCharacterData();
		});
	}

	private void OnLoginFailed(object sender, System.EventArgs e){
		DoOnMainThread.ExecuteOnMainThread.Enqueue(delegate{
			_loginForm.Animator.SetTrigger("LoginFail");
		});
	}

	void OnDisable(){
		//_client.DataReceived -= OnDataReceived;
	}

	public void ViewCharacterSelection(){
		_cameraAnimator.SetTrigger("SwitchToCharacter");
		_loginForm.Animator.SetBool("Visible", false);
		_characterFormAnimator.SetBool("Visible", true);
	}

	public void ViewLoginScreen(){
		_cameraAnimator.SetTrigger("SwitchToLogin");
		_loginForm.Animator.SetBool("Visible", true);
		_characterFormAnimator.SetBool("Visible", false);
	}

	public void ViewCharData(Character[] charList){
		_loginCharList.RenewCharData (charList);
	}

	public void JoinGame(){
		Debug.Log ("Join Game");
		SceneManager.LoadScene ("Game");
	}

	// Update is called once per frame
	void Update () {
		
	}

}
