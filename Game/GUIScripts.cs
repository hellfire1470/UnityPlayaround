using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIScripts : MonoBehaviour {

	private GameObject _clipTarget;

	public void SetClipTarget(GameObject clipTarget){
		_clipTarget = clipTarget;
	}

	public void Toggle(GameObject gameObject){
		if (gameObject.activeSelf) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (true);
		}
	}

	public void ClipIfActive(GameObject obj){
		if (_clipTarget != null) {
			if (obj.activeSelf) {
				_clipTarget.GetComponent<Image> ().color = Color.grey;
			} else {
				_clipTarget.GetComponent<Image> ().color = Color.white;
			}
		}
	}

	public void CharacterSelection(){
		SceneManager.LoadScene("LoginScreen");
	}
}
