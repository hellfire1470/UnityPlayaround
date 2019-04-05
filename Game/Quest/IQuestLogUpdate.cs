using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestLogUpdate{
	void OnQuestAdded (object sender, QuestEventArgs args);
	void OnQuestRemoved (object sender, QuestEventArgs args);
}
