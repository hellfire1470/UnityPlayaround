using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestObjectScript : MonoBehaviour
{

    public Button button;
    public Button button2;
    public Button button3;
    public PlayerObject player;
    public NetworkObject no;

    // Use this for initialization
    void Start()
    {
        button2.onClick.AddListener(delegate
        {
            List<QuestCondition> qcl = new List<QuestCondition>();
            QuestConditionItem qci = new QuestConditionItem(player.Player.Inventories, ItemManager.Items[0], 10);
            qci.QuestProgress += (object sender, QuestConditionEventArgs e) =>
            {
                Debug.Log("Required Item added for Quest: ");
                Debug.Log("Required Amount: " + e.RequiredAmount);
                Debug.Log("Available Amount: " + e.AvailableAmount);
            };
            qcl.Add(qci);
            List<QuestReward> rewards = new List<QuestReward>();
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(0, new ItemInstanceData(1))));
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(0, new ItemInstanceData(5))));
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(1, new ItemInstanceData(1))));
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(1, new ItemInstanceData(1))));
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(1, new ItemInstanceData(1))));
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(1, new ItemInstanceData(1))));
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(1, new ItemInstanceData(1))));
            rewards.Add(new QuestRewardItemInstance(new ItemInstance(1, new ItemInstanceData(1))));
            rewards.Add(new QuestRewardLevel(1));
            Debug.Log("Started new Quest... Get 10 " + ItemManager.Items[0]);
            Quest quest = new Quest("%Class%s story", "Hmmmm... a %class%, \nNope.", qcl, rewards, null);
            quest.QuestFinished += (object sender, QuestEventArgs e) =>
            {
                Debug.Log("Quest Finished");
                foreach (QuestReward reward in quest.ClaimRewards())
                {
                    if (reward.Reward is ItemInstance)
                    {
                        player.Player.PutItem(reward.Reward as ItemInstance);
                    }
                    if (reward is QuestRewardLevel)
                    {
                        player.Player.Level += (int)reward.Reward;
                    }
                }
                player.Player.QuestLog.Remove(quest);
            };
            player.Player.QuestLog.Add(quest);

        });

        button.onClick.AddListener(delegate
        {
            player.Player.Inventories[0].PutItem(new ItemInstance(0, new ItemInstanceData(1)));
            player.Player.Inventories[1].PutItem(new ItemInstance(0, new ItemInstanceData(1)));
            player.Player.Inventories[2].PutItem(new ItemInstance(0, new ItemInstanceData(1)));
            player.Player.Inventories[3].PutItem(new ItemInstance(0, new ItemInstanceData(1)));
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Player.QuestLog.Quests.Count > 0)
        {
            Quest quest = player.Player.QuestLog.Quests[0];
            //Debug.Log (quest.IsDone ());
            if (quest.IsDone())
            {
                quest.Finish();

                //gameObject.SetActive (false);
            }
        }
    }


}
