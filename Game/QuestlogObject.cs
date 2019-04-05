using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestlogObject : MonoBehaviour, IQuestLogUpdate {

	[SerializeField] private GameObject _questPrefab;
	[SerializeField] private PlayerObject _playerObject;

	#region IQuestLogUpdate implementation
	public void OnQuestAdded (object sender, QuestEventArgs args)
	{
		_ClearQuestLog ();
		_FillQuestLog ();
	}
	public void OnQuestRemoved (object sender, QuestEventArgs args)
	{
		_ClearQuestLog ();
		_FillQuestLog ();
	}
	#endregion


	void OnEnable(){
		_playerObject.Player.QuestLog.QuestAdded += OnQuestAdded;
		_playerObject.Player.QuestLog.QuestRemoved += OnQuestRemoved;
		_FillQuestLog ();
	}

	void OnDisable(){
		_playerObject.Player.QuestLog.QuestAdded -= OnQuestAdded;
		_playerObject.Player.QuestLog.QuestRemoved -= OnQuestRemoved;
		_ClearQuestLog ();
	}


	private void _FillQuestLog(){
		foreach (Quest quest in _playerObject.Player.QuestLog.Quests) {
			GameObject questObject = Instantiate (_questPrefab);
			questObject.transform.SetParent (gameObject.transform);
			questObject.GetComponentInChildren<Text> ().text = ContentParser.ReplaceFromList(quest.Name);
		}
	}

	private void _ClearQuestLog(){
		foreach (Transform t in gameObject.GetComponentsInChildren<Transform>()) {
			if (t != transform) {
				Destroy (t.gameObject);
			}
		}
	}

}
