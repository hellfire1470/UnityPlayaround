using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class QuestReward
{

    #region IQuestReward implementation

    public abstract object Reward { get; }

    #endregion

}
