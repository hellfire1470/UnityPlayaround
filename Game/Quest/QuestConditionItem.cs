using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestConditionEventArgs: EventArgs{
	public Quest Quest{ get; set; }
	public Item RequiredItem { get; set; }
	public int RequiredAmount { get; set; }
	public int AvailableAmount { get; set; }
}

public class QuestConditionItem: QuestCondition {
	private Item _requiredItem;
	private int _requiredAmount;
	private InventoryExtention[] _availableInventories;

	public event EventHandler<QuestConditionEventArgs> QuestProgress;

	public QuestConditionItem(InventoryExtention[] availableInventories, Item requiredItem, int requiredAmount): base(){
		_requiredItem = requiredItem;
		_requiredAmount = requiredAmount;
		foreach (Inventory inventory in availableInventories) {
			inventory.ItemInserted += OnInventoryItemInserted;
		}
		_availableInventories = availableInventories;
	}

	~QuestConditionItem(){
		foreach (InventoryExtention inventory in _availableInventories) {
			inventory.ItemInserted -= OnInventoryItemInserted;
		}
	}

	protected virtual void OnInventoryItemInserted(object sender, ItemInsertedEventArgs e){
		if(e.NewItem.RawItem == _requiredItem){
			OnQuestProgress(new QuestConditionEventArgs(){
				RequiredItem = _requiredItem,
				RequiredAmount = _requiredAmount,
				AvailableAmount = AvailableAmount()
			});
		}
	}

	protected virtual void OnQuestProgress(QuestConditionEventArgs args){

		if (QuestProgress != null)
			QuestProgress (this, args);
	}

	public int AvailableAmount(){
		int amount = 0;
		foreach (InventoryExtention inventory in _availableInventories) {
			foreach (ItemInstance itemInstance in inventory.Items.Values) {
				if (itemInstance == null) {
					continue;
				}
				if (itemInstance.RawItem == _requiredItem) {
					if (itemInstance.InstanceData is IItemInstanceStackable) {
						amount += (itemInstance.InstanceData as IItemInstanceStackable).Amount;
					} else {
						amount++;
					}
				}
			}
		}
		return amount;
	}


	#region implemented abstract members of QuestCondition

	public override bool IsDone ()
	{
		return AvailableAmount() >= _requiredAmount;
	}



	public override void Finish ()
	{
		ItemInstance toRemove = new ItemInstance (_requiredItem.Id, new ItemInstanceData (_requiredAmount));
		//Todo:: write the right one
		foreach (InventoryExtention inventory in _availableInventories) {
			inventory.ItemInserted -= OnInventoryItemInserted;

			toRemove = inventory.RemoveItem (toRemove);

			if (toRemove == null) {
				return;
			} else {
				Debug.Log((toRemove.InstanceData as ItemInstanceData).Amount);
			}
		}

	}

	#endregion

}