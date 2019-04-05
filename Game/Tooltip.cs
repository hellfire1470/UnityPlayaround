using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

	public struct TooltipContent{
		public string name;
		public string description;
	}

	private GameObject _nameObject;
	private GameObject _descriptionObject;

	// Use this for initialization
	void Start () {
		_nameObject = gameObject.GetComponentsInChildren<Transform> ()[1].gameObject;
		_descriptionObject = gameObject.GetComponentsInChildren<Transform> ()[2].gameObject;
		Manager<Tooltip>.Add ("Item", this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetContent(TooltipContent content){
		_nameObject.GetComponent<Text> ().text = content.name;
		_descriptionObject.GetComponent<Text> ().text = content.description;
	}

	public void Show(Vector2 position){
		gameObject.SetActive (true);
		transform.position = position;
	}

	public void Hide(){
		gameObject.SetActive (false);
	}
}