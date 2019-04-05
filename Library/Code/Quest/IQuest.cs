public interface IQuest
{
    void OnQuestFinished(object sender, QuestEventArgs args);
    void OnQuestRewarded(object sender, QuestEventArgs args);
}
