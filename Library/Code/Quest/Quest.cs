using System;
using System.Collections.Generic;

public partial class Quest
{

    public event EventHandler<QuestEventArgs> QuestFinished;
    public event EventHandler<QuestEventArgs> QuestRewarded;


    private string _description;
    private string _name;
    private QuestState _questState;

    private List<QuestCondition> _conditions = new List<QuestCondition>();
    private List<QuestReward> _rewards = new List<QuestReward>();
    private List<QuestReward> _rewardSelect = new List<QuestReward>();
    private QuestReward _rewardSelected;

    public string Name
    {
        get
        {
            return _name;
        }
    }

    public string Description
    {
        get
        {
            return _description;
        }
    }

    public Quest(string name, string description, List<QuestCondition> conditionList = null, List<QuestReward> rewards = null, List<QuestReward> rewardsToChoose = null)
    {
        _name = name;
        _description = description;
        _conditions = conditionList;
        if (rewards != null)
        {
            _rewards = rewards;
        }
        if (rewardsToChoose != null)
        {
            _rewardSelect = rewardsToChoose;
        }
    }

    public bool IsDone()
    {
        if (_questState == QuestState.Finished || _questState == QuestState.Completed)
        {
            return true;
        }
        foreach (QuestCondition condition in _conditions)
        {
            if (!condition.IsDone())
            {
                return false;
            }
        }
        return true;
    }

    public void Finish()
    {
        if (IsDone() && _questState == QuestState.Started)
        {
            foreach (QuestCondition condition in _conditions)
            {
                condition.Finish();
            }
            _questState = QuestState.Finished;
            OnQuestFinished();
        }
    }

    public void SelectReward(QuestReward reward)
    {
        if (_questState == QuestState.Finished)
        {
            if (_rewardSelect.Contains(reward))
            {
                _rewardSelected = reward;
            }
            else
            {
                throw new System.Exception("Selected reward is not available");
            }
        }
    }

    public bool RewardSelected()
    {
        if (_rewardSelect.Count > 0 && _rewardSelected != null)
        {
            return true;
        }
        else if (_rewardSelect.Count == 0)
        {
            return true;
        }
        return false;
    }

    public List<QuestReward> ClaimRewards()
    {
        if (!RewardSelected())
        {
            throw new System.Exception("No reward selected");
        }

        List<QuestReward> rewards = new List<QuestReward>();

        if (_questState == QuestState.Finished)
        {
            _questState = QuestState.Completed;
            rewards.AddRange(_rewards);
            if (_rewardSelected != null)
            {
                rewards.Add(_rewardSelected);
            }
            OnQuestRewarded();
        }

        return rewards;
    }



    protected virtual void OnQuestRewarded()
    {
        QuestRewarded?.Invoke(this, new QuestEventArgs() { Quest = this });
    }

    protected virtual void OnQuestFinished()
    {
        QuestFinished?.Invoke(this, new QuestEventArgs() { Quest = this });
    }
}