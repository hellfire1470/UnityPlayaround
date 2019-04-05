using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Network.Data;

public class CharacterSelectedArgs: System.EventArgs{
	public long CharacterId { get; set; }
}

public class LoginCharList : MonoBehaviour {


	[SerializeField] private GameObject _charSlotPrefab;
	[SerializeField] private GameObject _contentArea;


	public event System.EventHandler<CharacterSelectedArgs> CharacterSelected;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void RenewCharData(Character[] data){
		_ClearGameObjectChildren (_contentArea);
		if (data == null) {
			throw new System.Exception ("Character data cant be null");
		}
		foreach (Character csd in data) {
			GameObject charSlot = Instantiate (_charSlotPrefab);
			charSlot.transform.SetParent (_contentArea.transform);
			charSlot.transform.localScale = new Vector3 (1f, 1f, 1f);
			Text[] _inputs = charSlot.GetComponentsInChildren<Text> ();
			long characterId = csd.Id;
			_inputs [0].text = csd.Name;
			_inputs [1].text = csd.Race.ToString();
			_inputs [2].text = csd.Class.ToString();
			_inputs [3].text = csd.Level.ToString();
			_inputs [4].text = csd.Location.Name;
			Button _buttonSelectChar = charSlot.GetComponent<Button> ();

			_buttonSelectChar.onClick.AddListener (delegate {
				
				OnCharacterSelected(characterId);
			});
		}
	}

	protected void OnCharacterSelected(long characterId){
		if (CharacterSelected != null) {
			CharacterSelected(this, new CharacterSelectedArgs(){ CharacterId = characterId });
		}
	}

	private void _ClearGameObjectChildren(GameObject gameObject){
		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			if (t != gameObject.transform) {
				Destroy (t.gameObject);
			}
		}
	}
}
