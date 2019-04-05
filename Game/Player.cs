using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
	private int _level = 1;

	private QuestLog _questLog;
	private InventoryExtention[] _inventories;


	public QuestLog QuestLog {
		get {
			return _questLog;
		}
	}

	public InventoryExtention[] Inventories {
		get {
			return _inventories;
		}
	}

	public int Level {
		get {
			return _level;
		}
		set{ 
			_level = value;
			Debug.Log ("Playerlevel: " + _level);
		}
	}

	public Player(){

		_questLog = new QuestLog (15);
		_inventories = new InventoryExtention[4];
		_inventories [0] = new InventoryExtention (4);
		_inventories [1] = new InventoryExtention (10);
		_inventories [2] = new InventoryExtention (10);
		_inventories [3] = new InventoryExtention (4);
	}

	public void PutItem(ItemInstance item){

		for (int i = 0; i < _inventories.Length; i++) {
			while (item != null && !_inventories [i].IsFull ()) {
				item = _inventories [i].PutItem (item);
			}
		}
	}
}
