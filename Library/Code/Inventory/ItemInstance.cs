using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstance{
	private int _itemId;
	private ItemData _instanceData;

	public ItemData InstanceData {
		get {
			return _instanceData;
		}
	}

	public Item RawItem {
		get {
			return ItemManager.Items[_itemId];
		}
	}

	public ItemInstance(int itemId, ItemData instanceData){
		_itemId = itemId;
		_instanceData = instanceData;
	}
}
