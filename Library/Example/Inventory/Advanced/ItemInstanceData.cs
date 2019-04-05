using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInstanceData: ItemData, IItemInstanceStackable{
	private int _amount;

	public int Amount {
		get {
			return _amount;
		}
		set {
			_amount = value;
		}
	}

	public ItemInstanceData(int amount){
		_amount = amount;
	}
}
