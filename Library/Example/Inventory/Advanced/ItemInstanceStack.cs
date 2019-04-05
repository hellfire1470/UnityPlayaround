using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstanceStack: ItemInstance{
	public ItemInstanceStack(int itemId, ItemData data): base(itemId, data){
		if (data is ItemInstanceData && RawItem.Data is IItemStackable) {
			if ((data as IItemInstanceStackable).Amount > (RawItem.Data as IItemStackable).StackMax) {
				(data as IItemInstanceStackable).Amount = (RawItem.Data as IItemStackable).StackMax;
			}
		} else {
			(data as ItemInstanceData).Amount = 0;
		}
	}
}