using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryExtention: Inventory{
	public InventoryExtention (int size, IInventory iInventory = null) : base (size, iInventory){

	}

	public override ItemInstance PutItem (int slotIndex, ItemInstance item)
	{
		if (item == null){
			throw new UnityException("item cant be null. Use RemoveItem function");
		}

		if (!_IsSlotInRange (slotIndex)) {
			throw new UnityException ("selected itemslot is not in range.");
		}


		return base.PutItem (slotIndex, item);
	}


	public new ItemInstance PutItem (ItemInstance item)
	{
		if (item == null){
			throw new UnityException("item cant be null. Use RemoveItem function");
		}
		if (item.RawItem.Data is IItemStackable) {
			int stackMax = (item.RawItem.Data as IItemStackable).StackMax;
			ItemInstance itemstackToAdd = item;
			int amountItemstackToAdd = (itemstackToAdd.InstanceData as IItemInstanceStackable).Amount;

			// Does Inventory already contain these items where stack is not full?
			foreach (int slotIndex in GetIndeces(item)) {
				ItemInstance iInstance = _items [slotIndex];
				int amountItemstack = (iInstance.InstanceData as IItemInstanceStackable).Amount;
				if (amountItemstack < stackMax) {

					// fill these slot
					int amountToAdd = Mathf.Min (amountItemstackToAdd, stackMax - amountItemstack);
					//int amountToAdd = Mathf.Min (amountToAddMax, stackMax - (stackMax - amountItemstackToAdd));
					(itemstackToAdd.InstanceData as IItemInstanceStackable).Amount -= amountToAdd;
					(iInstance.InstanceData as IItemInstanceStackable).Amount += amountToAdd;

					//Interface Call
					OnItemInserted(new ItemInsertedEventArgs(){
						SlotIndex = slotIndex,
						NewItem = iInstance,
						OldItem = null
					});
				}
			}

			// If all stacks are Filled

			while ((itemstackToAdd.InstanceData as IItemInstanceStackable).Amount > 0) {
				if (IsFull ()) {
					return itemstackToAdd;
				}
				amountItemstackToAdd = (itemstackToAdd.InstanceData as IItemInstanceStackable).Amount;
				int amountToAddMax = Mathf.Min (amountItemstackToAdd, stackMax);
				(itemstackToAdd.InstanceData as IItemInstanceStackable).Amount -= amountToAddMax;
				ItemInstance iInstance = new ItemInstance (itemstackToAdd.RawItem.Id, new ItemInstanceData (amountToAddMax));
				base.PutItem (iInstance);
			}
		} else {
			base.PutItem (item);
		}
		return null;
	}

	public ItemInstance RemoveItem(ItemInstance item){
		return null;
	}
}