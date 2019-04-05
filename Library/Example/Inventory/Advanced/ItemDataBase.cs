using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase: ItemData, IItemBase, IItemStats {

	private string _name;
	ItemType _type;
	Stats _stats;
	private string _description;

	#region IItemStats implementation

	public Stats Stats {
		get {
			return _stats;
		}
	}

	#endregion

	#region IItemBase implementation

	public string Name {
		get {
			return _name;
		}
	}

	public ItemType Type {
		get {
			return _type;
		}
	}

	#endregion


	#region IItemDescription implementation

	public string Description {
		get {
			return _description;
		}
	}

	#endregion

	public ItemDataBase(string name, ItemType type, Stats stats, string description){
		_name = name;
		_type = type;
		_stats = stats;
		_description = description;
	}

}
