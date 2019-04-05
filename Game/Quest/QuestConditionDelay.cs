using System.Collections;
using System.Collections.Generic;

public class QuestConditionDelay: QuestCondition {
	private System.DateTime _end;

	public QuestConditionDelay(System.DateTime end){
		_end = end;
	}

	#region implemented abstract members of QuestCondition

	public override bool IsDone ()
	{
		if (System.DateTime.Now > _end) {
			return true;
		}
		return false;
	}

	public override void Finish ()
	{
		
	}

	#endregion


}
