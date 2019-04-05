public class QuestRewardItemInstance : QuestReward
{
    ItemInstance _item;

    public QuestRewardItemInstance(ItemInstance item)
    {
        _item = item;
    }

    #region implemented abstract members of QuestReward
    public override object Reward
    {
        get
        {
            return _item;
        }
    }
    #endregion
}