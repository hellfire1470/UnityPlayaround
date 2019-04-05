using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataStack: ItemDataVisual, IItemStackable{
	private int _stackMax;

	#region IItemStackable implementation

	public int StackMax {
		get {
			return _stackMax;
		}
	}

	#endregion


	public ItemDataStack(string name, ItemType type, Stats stats, string description, Sprite sprite, int stackMax): base(name, type, stats, description, sprite){
		_stackMax = stackMax;
	}
}