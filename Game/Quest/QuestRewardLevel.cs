using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestRewardLevel : QuestReward {
	private int _level = 1;

	public QuestRewardLevel(int level){
		_level = level;
	}

	#region implemented abstract members of QuestReward

	public override object Reward {
		get {
			return _level;
		}
	}

	#endregion


}
