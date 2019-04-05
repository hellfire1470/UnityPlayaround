using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestLog {

	public event EventHandler<QuestEventArgs> QuestAdded;
	public event EventHandler<QuestEventArgs> QuestRemoved;

	private List<Quest> _quests = new List<Quest>();

	private int _maxQuests;

	public List<Quest> Quests {
		get {
			return _quests;
		}
	}

	protected virtual void OnQuestRemoved(Quest quest){
		if (QuestRemoved != null) {
			QuestRemoved (this, new QuestEventArgs () {
				Quest = quest
			});
		}
	}

	protected virtual void OnQuestAdded(Quest quest){
		if (QuestAdded != null) {
			QuestAdded (this, new QuestEventArgs () {
				Quest = quest
			});
		}
	}

	public void Remove(Quest quest){
		if (_quests.Remove (quest)) {
			OnQuestRemoved (quest);
		}
	}


	public QuestLog(int maxQuests){
		_maxQuests = maxQuests;
	}

	public void Add(Quest quest){
		if (!_quests.Contains (quest)) {
			if (!IsFull ()) {
				_quests.Add (quest);
				OnQuestAdded (quest);
			}
		}
	}

	public bool IsFull(){
		return _quests.Count >= _maxQuests;
	}
}
