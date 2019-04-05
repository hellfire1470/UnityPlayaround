using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataVisual: ItemDataBase, I2DImage{
	private Sprite _sprite;

	public ItemDataVisual(string name, ItemType type, Stats stats, string description, Sprite sprite): base(name, type, stats, description){
		_sprite = sprite;
	}

	#region I2DImage implementation

	public Sprite Sprite {
		get {
			return _sprite;
		}
	}

	#endregion
}